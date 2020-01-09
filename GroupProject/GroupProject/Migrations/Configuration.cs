namespace GroupProject.Migrations
{
    using GroupProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GroupProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GroupProject.Models.ApplicationDbContext context)
        {

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!roleManager.RoleExists("Administrator")) 
            {
                roleManager.Create(new IdentityRole { Name = "Administrator" });
            }

            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            if (!roleManager.RoleExists("Artist"))
            {
                roleManager.Create(new IdentityRole { Name = "Artist" });
            }

            if (!userManager.Users.Any(i => i.UserName == "admin@gallerystore.gr"))
            {                
                var result = userManager.Create(new ApplicationUser
                {
                    UserName = "admin@gallerystore.gr",
                    Email = "admin@gallerystore.gr",
                }, "!Admin123");


                if (result.Succeeded)
                {
                    var u = userManager.FindByName("admin@gallerystore.gr");
                    userManager.AddToRoles(u.Id, "Administrator"); 
                }
            }

            if (!userManager.Users.Any(i => i.UserName == "user@gallerystore.gr"))
            {
                var result = userManager.Create(new ApplicationUser
                {
                    UserName = "user@gallerystore.gr",
                    Email = "user@gallerystore.gr",
                }, "!user123");


                if (result.Succeeded)
                {
                    var u = userManager.FindByName("user@gallerystore.gr");
                    userManager.AddToRoles(u.Id, "User");
                }
            }

        }
    }
}
