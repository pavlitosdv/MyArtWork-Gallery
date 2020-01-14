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
using GroupProject.Repositories;

namespace GroupProject.Controllers
{
    public class AdminController : Controller
    {
        public AdminRepository _adminRepository = new AdminRepository();
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
            //var users = _adminRepository.GetUsers();

            //List<IdentityRole> roles = null;
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    roles = db.Roles.ToList();
            //}
            //ViewBag.Roles = roles;

            //return View(users);
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
        
        
        [HttpGet]
        public ActionResult RegisterRoleToUser(string id)
        {
            UserRoleViewModel u = new UserRoleViewModel();
            u.ApplicationUser= db.Users.SingleOrDefault(i => i.Id == id);
            
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
            var removeRole = UserManager.GetRoles(userRole.ApplicationUser.Id);

            ApplicationUser userId;
            using (var db = new ApplicationDbContext())
            {
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

    }
}