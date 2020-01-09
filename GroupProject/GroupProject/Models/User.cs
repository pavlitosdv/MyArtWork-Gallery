using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class User : ApplicationUser
    {
        // An User may make a commission to an Artist
        public virtual ICollection<UserToArtist> UserToArtist { get; set; }

    }
}