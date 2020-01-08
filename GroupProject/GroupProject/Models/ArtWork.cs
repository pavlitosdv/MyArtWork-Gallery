using GroupProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class ArtWork
    {
        [Key]
        public int Id { get; set; }
        #region data
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
        #endregion

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<ApplicationUser> MyFans { get; set; }
        //public string ArtistId { get; set; }

        //[Required]
        //[ForeignKey("ArtistId")]
        //public ApplicationUser Artist { get; set; }



    }
}