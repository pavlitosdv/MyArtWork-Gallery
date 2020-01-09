using GroupProject.Repositories;
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
            return View(users);
        }



    }
}