using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        #region Tables
        public DbSet<ArtWork> ArtWorks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        #endregion

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}