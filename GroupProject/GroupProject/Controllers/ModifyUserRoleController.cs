using GroupProject.Models;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupProject.Controllers
{
    public class ModifyUserRoleController : Controller
    {
        modifyUserRoleRepository _modifyUserRoleRepository = new modifyUserRoleRepository();

        // GET: ModifyUserRole
        public ActionResult Index()
        {
            var users = _modifyUserRoleRepository.GetUsers();
            List<IdentityRole> roles = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                roles = db.Roles.ToList();
            }
            ViewBag.Roles = roles;

            return View(users);
        }

        //public ActionResult Index()
        //{
        //    UserRoleViewModel m = new UserRoleViewModel();

        //    m.ApplicationUser = _modifyUserRoleRepository.GetUsers().ToList();

        //    List<IdentityRole> roles = null;
        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        roles = db.Roles.ToList();
        //    }
        //    ViewBag.Roles = roles;

        //    return View(users);
        //}

    }
}