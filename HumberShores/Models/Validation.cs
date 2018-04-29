using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HumberShores.Models
{
    public class Validation
    {
        public class FutureDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value != null && (DateTime)value > DateTime.Now;
            }
        }
    }
}