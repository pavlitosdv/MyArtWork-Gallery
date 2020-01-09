using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Commission
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfCommission { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        [FutureDate]
        public DateTime Deadline { get; set; }

       



    }
}