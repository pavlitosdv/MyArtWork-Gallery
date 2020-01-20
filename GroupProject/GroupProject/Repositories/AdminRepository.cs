using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroupProject.Repositories
{
    public class AdminRepository
    {
        //public IEnumerable<ApplicationUser> GetUsers()
        //{
        //    IEnumerable<ApplicationUser> users;

        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        users = db.Users.Include("Roles").ToList();
        //        //users = db.Users.ToList();
        //    }

        //    return users;
        //}

        public ApplicationUser FindById(int id)
        {
            ApplicationUser user;

            using (var db = new ApplicationDbContext())
            {
                user = db.Users.SingleOrDefault(i => i.Id == id.ToString());
            }

            return user;
        }

        public void DeleteUser(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }


    }
}