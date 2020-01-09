using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Models
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt;

            bool isValid = DateTime.TryParse(Convert.ToString(value), out dt);

            return (isValid && dt >= DateTime.Now);
        }
    }
}