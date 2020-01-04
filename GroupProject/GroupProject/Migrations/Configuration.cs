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


            // ** Proxira Ektos** \\
            //var userStore = new UserStore<ApplicationUser>(context); 
            //var userManager = new UserManager<ApplicationUser>(userStore); 

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

        }
    }
}
