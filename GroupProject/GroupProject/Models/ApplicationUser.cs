using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GroupProject.Models
{
   
    public class ApplicationUser : IdentityUser
    {
        #region Navigation Properties
        public virtual ICollection<ArtWork> MyArtWorks { get; set; }

        public virtual ICollection<Commission> Commissions { get; set; }

        public virtual ICollection<Preference> Preferences { get; set; }
        #endregion

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    
}