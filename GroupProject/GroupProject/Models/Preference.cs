using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Preference
    {

        //[Key]
        //public int Id { get; set; }

        [Key]
        [Required]
        [Column(Order = 1)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Key]
        [Required]
        [Column(Order = 2)]
        public int ArtWorkId { get; set; }
        [ForeignKey("ArtWorkId")]
        public ArtWork Artwork { get; set; }
        [Required]
        public bool IsLiked { get; set; }
    }
}