using GroupProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroupProject.Repositories.ApiRepository
{
    public class AdminApiRepository
    {
        private ApplicationUserManager _userManager;
        //public ApplicationDbContext db;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            IEnumerable<ApplicationUser> users;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //users = db.Users.Include("Roles").Include("Claims").Include("Logins").ToList();
                users = db.Users.Include(u => u.Roles).Include(u => u.Claims).Include(u => u.Logins).ToList();
                //users = db.Users.ToList();
            }

            return users;
        }

        public ApplicationUser FindUserById(int id)
        {
            ApplicationUser user;

            using (var db = new ApplicationDbContext())
            {
                user = db.Users.SingleOrDefault(i => i.Id == id.ToString());
            }

            return user;
        }

        public bool DeleteRoleFromUser(UserRoleViewModel userRole)
        {
            ApplicationUser userId;
            using (var db = new ApplicationDbContext())
            {
                userId = db.Users.SingleOrDefault(i => i.Id == userRole.ApplicationUser.Id);
            }
            if (userId == null)
            {
                return false;
            }

            UserManager.RemoveFromRole(userId.Id, userRole.RegisterViewModel.Role);
            return true;

        }

        public bool ChangeRoleFromUser(UserRoleViewModel userRole)
        {
            var removeRole = UserManager.GetRoles(userRole.ApplicationUser.Id);
            ApplicationUser userId;
            using (var db = new ApplicationDbContext())
            {
                userId = db.Users.SingleOrDefault(i => i.Id == userRole.ApplicationUser.Id);
            }

            UserManager.RemoveFromRoleAsync(userId.Id, removeRole.ToString());
            UserManager.AddToRoleAsync(userId.Id, userRole.RegisterViewModel.Role);

            return true;

        }
    }
}