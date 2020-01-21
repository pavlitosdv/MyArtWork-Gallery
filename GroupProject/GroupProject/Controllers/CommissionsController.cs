using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GroupProject.Models;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using PayPal.Api;

namespace GroupProject.Controllers
{
    public class CommissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ArtWorksRepository _artWork = new ArtWorksRepository();
        private CommissionRepository _commissionRepository = new CommissionRepository();


        // GET: Commissions
        public ActionResult Index()
        {
            //var commissions = db.Commissions.Include(c => c.Artist).Include(c => c.User);
            //return View(commissions.ToList());
            return View();
        }

        public ActionResult Donation(int id)
        {
            var donations = Session["Donations"] as List<int>; 
            var total = (double)Session["Total"];

            var artworks = db.ArtWorks.Find(id);

            if (total == 0)
            {
                total = 0;
                Session["Total"] = total;
            }
            else
            {
                Session["Total"] = total;
            }
            total += artworks.Price;
            Session["Total"] = total;


            if (donations == null)
            {
                donations = new List<int>();
                Session["Donations"] = donations;
            }

            if (!donations.Contains(id))
            {
                donations.Add(id);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveDonation(int id)
        {
            var donations = Session["Donations"] as List<int>;
            var total = (double)Session["Total"];

            var artworks = db.ArtWorks.Find(id);

            if (donations != null)
            {
                donations.Remove(id);
            }

            total -= artworks.Price;
            Session["Total"] = total;

            return RedirectToAction("Index", "Gigs");
        }

        public ActionResult Submit()
        {
            var donations = Session["Donations"] as List<int>;

          //---\\
            var total = (double)Session["Total"];

            IEnumerable<ArtWork> model = null;

            if (donations != null)
            {
                model = _artWork.GetArtWorks(donations);
            }

            return View(model);
        }

        [HttpPost]
        //[ActionName("Submit")]
        public ActionResult SubmitDonation()
        {
            var ids = Session["Donations"] as List<int>;
            var total = (double)Session["Total"];

            string userId = User.Identity.GetUserId();
            //_commissionRepository.AddCommissionToUser(userId, ids, total);

            return RedirectToAction("Index", "Home");
        }

        //--------------------------------------------

        private Payment payment;

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            var donations = Session["Donations"] as List<int>;

            ArtWork artworksItem = null;
            List<ArtWork> artworks = null;

            foreach (var item in donations)
            {
                artworksItem = db.ArtWorks.Find(item);
                artworks.Add(artworksItem);
            }


            foreach (var cart in artworks)
            {
                itemList.items.Add(new Item()
                {
                    name = cart.Name,
                    currency = "EUR",
                    price = cart.Price.ToString(),
                    quantity = "1",
                    sku = "sku"
                });
            }
          

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "1.00",
                shipping = "1.00",
                subtotal = artworks.Sum(x => x.Price).ToString()
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDecimal(details.tax, new CultureInfo("en-US")) + Convert.ToDecimal(details.shipping, 
                new CultureInfo("en-US")) + Convert.ToDecimal(details.subtotal, new CultureInfo("en-US"))).ToString("0.00", new CultureInfo("en-US")),//tax+shipping+subtotal            
                //total = "7", // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Afdemp group project transaction",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }


        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        public ActionResult PaymentWithPaypal()
        {
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Paypal/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.log("Error" + ex.Message);
                Session.Clear();
                return View("FailureView");
            }

            var ids = Session["Donations"] as List<int>;
            var total = (double)Session["Total"];

            string userId = User.Identity.GetUserId();
            _commissionRepository.AddCommissionToUser(userId, ids, total);
            Session.Clear();
            return View("SuccessView");
        }
    }
}
