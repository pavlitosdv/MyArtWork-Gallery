using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ArtWork> ArtWorks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        

        public ApplicationDbContext()
            : base("GalleryConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
         

            base.OnModelCreating(modelBuilder);
        }
    }
}