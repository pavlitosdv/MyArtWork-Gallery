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
        public DbSet<Preference> Preferences { get; set; }
        #endregion

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ArtWorkTag Table Configuration
            //modelBuilder.Entity<ArtWork>()
            //            .HasMany<Tag>(s => s.Tags)
            //            .WithMany(c => c.ArtWorks)
            //            .Map(cs =>
            //            {
            //                cs.MapLeftKey("ArtWorkId");
            //                cs.MapRightKey("TagId");
            //                cs.ToTable("ArtWorkTag");
            //            });
            #endregion

            #region Commission Table Configuration
            modelBuilder.Entity<Commission>()
                        .HasRequired(m => m.User)
                        .WithMany(s => s.Commissions)
                        .HasForeignKey(m => m.UserId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<Commission>()
                        .HasRequired(m => m.Artist)
                        .WithMany()
                        .HasForeignKey(m => m.ArtistId)
                        .WillCascadeOnDelete(false);

            #endregion

            #region Preference Table Configuration
            modelBuilder.Entity<Preference>()
                        .HasRequired(m => m.User)
                        .WithMany(s => s.Preferences)
                        .HasForeignKey(m => m.UserId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<Preference>()
                        .HasRequired(m => m.ArtWork)
                        .WithMany()
                        .HasForeignKey(m => m.ArtWorkId)
                        .WillCascadeOnDelete(false);

            #endregion

            #region Message Table 
            modelBuilder.Entity<Message>()
                       .HasRequired(m => m.User1)
                       .WithMany(s => s.Messages)
                       .HasForeignKey(m => m.UserFrom)
                       .WillCascadeOnDelete(true);

            modelBuilder.Entity<Message>()
                        .HasRequired(m => m.User2)
                        .WithMany()
                        .HasForeignKey(m => m.UserTo)
                        .WillCascadeOnDelete(false);
            #endregion

        }

        public System.Data.Entity.DbSet<GroupProject.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}