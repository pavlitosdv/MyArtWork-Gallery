using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GroupProject.Models;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;

namespace GroupProject.Controllers
{
    public class CommissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ArtWorksRepository _artWork = new ArtWorksRepository();
        //private CommissionRepository _commissionRepository = new CommissionRepository();


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
            //var total = (double)Session["Total"];

            var artworks = db.ArtWorks.Find(id);

            //if (total == 0)
            //{
            //    total = 0;
            //    Session["Total"] = total;
            //}
            //else
            //{
            //    Session["Total"] = total;
            //}
            //total += artworks.Price;
            //Session["Total"] = total;


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
            //var total = (double)Session["Total"];

            var artworks = db.ArtWorks.Find(id);

            if (donations != null)
            {
                donations.Remove(id);
            }

            //total += artworks.Price;
            //Session["Total"] = total;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Submit()
        {
            var donations = Session["Donations"] as List<int>;

          //---\\
            //var total = (double)Session["Total"];

            IEnumerable<ArtWork> model = null;

            if (donations != null)
            {
                model = _artWork.GetArtWorks(donations);
            }

            return View(model);
        }

        //[HttpPost]
        ////[ActionName("Submit")]
        //public ActionResult SubmitDonation()
        //{
        //    var ids = Session["Donations"] as List<int>;
        //    var total = (double)Session["Total"];

        //    string userId = User.Identity.GetUserId();
        //    //_commissionRepository.AddCommissionToUser(userId, ids, total);

        //    return RedirectToAction("Index", "Home");
        //}
    }
}
