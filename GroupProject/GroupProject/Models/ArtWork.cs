using GroupProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class ArtWork
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey]
        //public ArtistId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Dimensions { get; set; }

        [Required]
        public Style style { get; set; }

        [Required]
        public Enums.Type type { get; set; }

        [Required]
        public Media media { get; set; }

        [Required]
        public Surface surface { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePublished { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

       // public virtual ICollection<User/Artist> User/Artist {get; set;}

        //public virtual Artist artist { get; set; }
    }
}