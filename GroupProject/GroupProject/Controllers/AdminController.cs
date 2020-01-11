using GroupProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace GroupProject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationDbContext db;
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
        public AdminController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Admin
        public ActionResult Index()
        {
            List<DataPointBar> dataPointsBar = new List<DataPointBar>();
            List<DataPointPie> dataPointsPie = new List<DataPointPie>();

            dataPointsBar.Add(new DataPointBar("Economics", 1));
            dataPointsBar.Add(new DataPointBar("Physics", 2));
            dataPointsBar.Add(new DataPointBar("Literature", 4));
            dataPointsBar.Add(new DataPointBar("Chemistry", 4));
            dataPointsBar.Add(new DataPointBar("Literature", 9));
            dataPointsBar.Add(new DataPointBar("Physiology or Medicine", 11));
            dataPointsBar.Add(new DataPointBar("Peace", 13));

            dataPointsPie.Add(new DataPointPie("Simple Users", 26));
            dataPointsPie.Add(new DataPointPie("Artists", 20));
            dataPointsPie.Add(new DataPointPie("Tags", 5));
            dataPointsPie.Add(new DataPointPie("Comments", 3));
            dataPointsPie.Add(new DataPointPie("Favourites", 7));
            dataPointsPie.Add(new DataPointPie("Others", 17));

            ViewBag.DataPointsPie = JsonConvert.SerializeObject(dataPointsPie);
            ViewBag.DataPointsBar = JsonConvert.SerializeObject(dataPointsBar);

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
        public ActionResult RegisterRoleToUser(string id)
        {
            //ViewBag.Role = new SelectList(db.Roles.ToList(), "Role", "Role");
            //ViewBag.UserName = new SelectList(db.Users.ToList(), "UserName", "UserName");
            
            UserRoleViewModel u = new UserRoleViewModel();
            u.ApplicationUser= db.Users.SingleOrDefault(i => i.Id == id);
            //u.ApplicationUser = (ApplicationUser)db.Users.Where(i => i.Id == id);

            //List<ApplicationUser> userList = null;
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    userList = db.Users.ToList();
            //}
            //ViewBag.Users = userList;

            List<IdentityRole> roles = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                roles = db.Roles.ToList();
            }
            ViewBag.Roles = roles;

            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterRoleToUser(UserRoleViewModel userRole)
        {
            //var removeRole = db.Users.Where(i => i.Id == userRole.ApplicationUser.Id).Select(i => i.Id == userRole.RegisterViewModel.Role).ToString();
            //var RoleId = db.Users.SingleOrDefault(i => i.Id == userRole.ApplicationUser.Id).Roles.ElementAt(0).RoleId;
            //var removeRole = db.Roles.SingleOrDefault(i => i.Id.Equals(RoleId)).Name;

            //var RoleId = db.Users.Include("Roles").SingleOrDefault(i => i.Id == userRole.ApplicationUser.Id); /*.Roles.ElementAt(0).RoleId;*/

            var removeRole = UserManager.GetRoles(userRole.ApplicationUser.Id);

            //var rolesArray = db.Roles.GetRolesForUser();
            //await this.UserManager.RemoveFromRoleAsync(userRole.ApplicationUser.Id, removeRole);

            ApplicationUser userId;
            using (var db = new ApplicationDbContext())
            {
                //userId = db.Users.Where(i => i.Email == user.Email).Select(s => s.Id);
                userId = db.Users.SingleOrDefault(i => i.Id == userRole.ApplicationUser.Id);
            }

            await this.UserManager.RemoveFromRoleAsync(userId.Id, removeRole.ToString());

            await this.UserManager.AddToRoleAsync(userId.Id, userRole.RegisterViewModel.Role);
            //await UserManager.UpdateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult RemoveRoleFromUser(string id)
        {
            UserRoleViewModel u = new UserRoleViewModel();
            u.ApplicationUser = db.Users.SingleOrDefault(i => i.Id == id);

            List<IdentityRole> roles = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                roles = db.Roles.ToList();
            }
            ViewBag.Roles = roles;

            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveRoleFromUser(UserRoleViewModel userRole)
        {
            ApplicationUser userId;
            using (var db = new ApplicationDbContext())
            {
                userId = db.Users.SingleOrDefault(i => i.Id == userRole.ApplicationUser.Id);
            }

            await this.UserManager.RemoveFromRoleAsync(userId.Id, userRole.RegisterViewModel.Role);
            //await UserManager.UpdateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        #region Arxiko RegisterRoleToUser
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RegisterRoleToUser(RegisterViewModel model, ApplicationUser user)
        //{
        //    ApplicationUser userId;
        //    using (var db = new ApplicationDbContext())
        //    {
        //        //userId = db.Users.Where(i => i.Email == user.Email).Select(s => s.Id);
        //        userId = db.Users.SingleOrDefault(i => i.Id == user.Id);
        //    }

        //    //var removeRole = db.Users.Where(i => i.Email == user.Email).Select(s => s.Roles).ToString();

        //    //string updatedId = "";
        //    //foreach (var i in userId)
        //    //{
        //    //    updatedId = i.ToString();
        //    //}

        //    //await this.UserManager.RemoveFromRoleAsync(updatedId, removeRole);
        //    await this.UserManager.AddToRoleAsync(userId.Id.ToString(), model.Role);
        //    //await UserManager.UpdateAsync(user);
        //    return RedirectToAction("Index", "Home");
        //}

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

        #endregion
    }
}