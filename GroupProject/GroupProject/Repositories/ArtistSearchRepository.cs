using GroupProject.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace GroupProject.Repositories
{
    public class ArtistSearchRepository
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

        public IEnumerable<ApplicationUser> SearchArtist(string searchTerm, string category)
        {
            IEnumerable<ApplicationUser> artists;
            //List<ApplicationUser> artists = new List<ApplicationUser>();
            //IEnumerable<ApplicationUser> artist;

            if (category != null && searchTerm !=null)
            {
                using (var db = new ApplicationDbContext())
                {
                    artists = db.Users.Where(i => i.Roles.Any(r => r.RoleId == db.Roles.FirstOrDefault(z => z.Name == "Artist").Id) && i.UserName == searchTerm).ToList();
                }                
            }
            else
            {
                using (var db = new ApplicationDbContext())
                {
                    artists = db.Users.Where(i => i.Roles.Any(r => r.RoleId == db.Roles.FirstOrDefault(z => z.Name == "Artist").Id)).ToList();
                }
            }           
            return artists;
        }
    }
}