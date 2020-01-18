using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        //[MaxLength(255)]
        public string Messages { get; set; }

        #region Fields Data

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime DateOfCommission { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]

        #endregion

        #region Navigation Properties

        #region A User that receive's a message
        public string UserTo { get; set; }

        public ApplicationUser User1 { get; set; }
        #endregion

        #region A user who sent a message
        public string UserFrom { get; set; }

        public ApplicationUser User2 { get; set; }
        #endregion

        #endregion
    }
}