using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class ProfileViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<ArtWork> Artworks { get; set; }
    }
}