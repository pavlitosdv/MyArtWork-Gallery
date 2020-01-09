using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        #region Navigation Properties

        #region A Tag may characterize zero, one or many ArtWorks
        public virtual ICollection<ArtWork> ArtWorks { get; set; }
        #endregion

        #endregion

    }
}