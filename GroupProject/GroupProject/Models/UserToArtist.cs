using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class UserToArtist : Commission
    {
        [Key, Column(Order = 0)]
        public int UserID { get; set; }
        [Key, Column(Order = 1)]
        public int ArtistID { get; set; }

        public virtual User User { get; set; }
        public virtual Artist Artist { get; set; }

    }
}