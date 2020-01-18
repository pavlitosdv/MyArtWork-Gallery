using GroupProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // GET: Profile
        public ActionResult Index()
        {
            //string userId = User.Identity.GetUserId();

            //ApplicationUser LogedInUser = null;
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    LogedInUser = db.Users.SingleOrDefault(i => i.Id == userId);
            //}
            
            //return View(LogedInUser);
            return View();
        }

        public ActionResult Edit()
        {

            string userId = User.Identity.GetUserId();
            
            ApplicationUser applicationUser = db.Users.Find(userId);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(applicationUser);
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
               
            }
            return RedirectToAction("Index");
            //return View(applicationUser);
        }


    }
}