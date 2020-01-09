using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Artist : ApplicationUser
    {
        // An Artist must have at least one ArtWork published
        public virtual ICollection<ArtWork> MyArtWorks { get; set; }

        // An Artist may make a commission to an Artist
        public virtual ICollection<ArtistToArtist> ArtistToArtist { get; set; }

        // An Artist may be commissioned by a User
        public virtual ICollection<UserToArtist> UserToArtist { get; set; }
    }
}