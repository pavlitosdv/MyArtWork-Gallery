using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class UserRoleViewModel
    {
        public string SelectedRole { get; set; }
        //public List<ApplicationUser> ApplicationUser { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        //public List<RegisterViewModel> RegisterViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
        //public List<IdentityRole> Roles { get; set; }
        //public List<IdentityUser> users { get; set; }

        //public IdentityDbContext<ApplicationUser> identity {get;set;}



    }
}