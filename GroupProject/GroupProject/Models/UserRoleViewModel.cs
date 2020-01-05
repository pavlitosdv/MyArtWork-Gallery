using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class UserRoleViewModel
    {

        public List<ApplicationUser> applicationUser{get;set;}

        public List<RegisterViewModel> registerViewModel { get; set; }

    }
}