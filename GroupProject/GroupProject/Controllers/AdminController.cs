using GroupProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Artworks()
        {
            return View();
        }

        public ActionResult Tags()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult RegisterRole(string id)
        //{

        //    ApplicationUser userList;
        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        userList = db.Users.Find(id);
        //    }
        //    ViewBag.Users = userList;

        //    List<IdentityRole> roles = null;
        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        roles = db.Roles.ToList();
        //    }
        //    ViewBag.Roles = roles;

        //    return View();
        //}

        [HttpGet]
        public ActionResult RegisterRoleToUser(int id)
        {
            //ViewBag.Role = new SelectList(db.Roles.ToList(), "Role", "Role");
            //ViewBag.UserName = new SelectList(db.Users.ToList(), "UserName", "UserName");
            var userId = db.Users.Where(i => i.Id == id.ToString());

            List<ApplicationUser> userList = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                userList = db.Users.ToList();
            }
            ViewBag.Users = userList;

            List<IdentityRole> roles = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                roles = db.Roles.ToList();
            }
            ViewBag.Roles = roles;

            return View(userId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterRoleToUser(RegisterViewModel model, ApplicationUser user)
        {
            var userId = db.Users.Where(i => i.Email == user.Email).Select(s => s.Id);
            //var removeRole = db.Users.Where(i => i.Email == user.Email).Select(s => s.Roles).ToString();

            string updatedId = "";
            foreach (var i in userId)
            {
                updatedId = i.ToString();
            }

            //await this.UserManager.RemoveFromRoleAsync(updatedId, removeRole);
            await this.UserManager.AddToRoleAsync(updatedId, model.Role);
            //await UserManager.UpdateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult RemoveRoleFromUser()
        {
            //ViewBag.Role = new SelectList(db.Roles.ToList(), "Role", "Role");
            //ViewBag.UserName = new SelectList(db.Users.ToList(), "UserName", "UserName");

            List<ApplicationUser> userList = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                userList = db.Users.ToList();
            }
            ViewBag.Users = userList;

            //List<IdentityRole> roles = null;
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    roles = db.Roles.ToList();
            //}
            //ViewBag.Roles = roles;

            return View();
        }
    }
}