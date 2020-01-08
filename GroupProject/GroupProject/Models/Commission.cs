using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class Commission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserFromId { get; set; }

        [Required]
        public string UserToId { get; set; }

        [Required]
        public DateTime DateOfCommission {get; set;}

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public double Price { get; set; }
    }
}