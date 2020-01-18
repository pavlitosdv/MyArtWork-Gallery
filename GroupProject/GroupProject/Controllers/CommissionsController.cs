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

            if (donations == null)
            {
                donations = new List<int>();
                Session["Attendances"] = donations;
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

            if (donations != null)
            {
                donations.Remove(id);
            }

            return RedirectToAction("Index", "Gigs");
        }

        public ActionResult Submit()
        {
            var donations = Session["Donations"] as List<int>;
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

            string userId = User.Identity.GetUserId();
            _commissionRepository.AddCommissionToUser(userId, ids);

            return RedirectToAction("Index", "Home");
        }

        // GET: Commissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commissions.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // GET: Commissions/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Users, "Id", "Email");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Commissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,DateOfCommission,Deadline,UserId,ArtistId")] Commission commission)
        {
            if (ModelState.IsValid)
            {
                db.Commissions.Add(commission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Users, "Id", "Email", commission.ArtistId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", commission.UserId);
            return View(commission);
        }

        // GET: Commissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commissions.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Users, "Id", "Email", commission.ArtistId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", commission.UserId);
            return View(commission);
        }

        // POST: Commissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,DateOfCommission,Deadline,UserId,ArtistId")] Commission commission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Users, "Id", "Email", commission.ArtistId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", commission.UserId);
            return View(commission);
        }

        // GET: Commissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commission commission = db.Commissions.Find(id);
            if (commission == null)
            {
                return HttpNotFound();
            }
            return View(commission);
        }

        // POST: Commissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commission commission = db.Commissions.Find(id);
            db.Commissions.Remove(commission);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
