using GroupProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class ArtWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        #region Fields Data

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public long Length { get; set; }

        [Required]
        public long Width { get; set; }

        [Required]
        public Style style { get; set; }

        [Required]
        public Enums.Type type { get; set; }

        [Required]
        public Media media { get; set; }

        [Required]
        public Surface surface { get; set; }

        // added Price 1-01-2020
        [Required]
        public double Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePublished { get; set; }

        public string Thumbnail { get; set; }

        [DisplayName("Upload File")]
        [NotMapped] 
        public HttpPostedFileBase ImageFile { get; set; }

        #endregion

        #region Navigation Properties

        #region An ArtWork must have an Artist

        public ApplicationUser Artist { get; set; }
        #endregion

        #region An ArtWork may have many Tags that charactetize it
        public ICollection<Tag> Tags { get; set; }
        #endregion  

        #endregion
    }
}