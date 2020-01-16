using GroupProject.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace GroupProject.Repositories
{
    public class _artistSearchRepository
    {
        private ApplicationUserManager _userManager;

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

        public IEnumerable<ApplicationUser> SearchArtist(string searchTerm)
        {
            List<ApplicationUser> artists = new List<ApplicationUser>();
            //IEnumerable<ApplicationUser> artist;


            using (var db = new ApplicationDbContext())
            {
                var RoleId = db.Users.Where(i => i.Roles.Any(r => r.RoleId == db.Roles.SingleOrDefault(z => z.Name == "Artist").Id) && i.UserName == "x").ToList();
                //RoleId[0].Roles.
                var a = db.Roles.ToList();

                //var o= db.Tags.Include("ArtWorks").Where(i=>i.ArtWorks)


                //var removeRole = db.Roles.SingleOrDefault(i => i.Id.Equals(RoleId)).Name;


                //artist = db.Users.Include("UserRoles").Include("Roles").Where(i => i.; Roles.name nam.IsUserInRole(User.Identity.Name, "Artist"));

                artists = db.Users.Include("UserRoles").Include("Roles").Where(i => searchTerm.Contains("artist")).ToList();
            }
            return artists;
        }
    }
}