using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class ArtistToArtist : Commission
    {
        [Key, Column(Order = 0)]
        public int ArtistID_Assigner { get; set; }
        [Key, Column(Order = 1)]
        public int ArtistID_Receiver { get; set; }

        public virtual Artist Artist_Assigner { get; set; }
        public virtual Artist Artist_Receiver { get; set; }
    }
}