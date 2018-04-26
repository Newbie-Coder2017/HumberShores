using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HumberShores.Models
{
    [MetadataType(typeof(employeeMetadata))]
    public partial class employee
    {
        class employeeMetadata
        {
            [Key]
            [Display(Name = "Employee ID")]
            public int emp_id { get; set; }

            [Display(Name = "Employee Position")]
            [Required(ErrorMessage = "The employee's position title must be entered.")]
            [StringLength(15, ErrorMessage = "Employee position title must be less than 15 characters.")]
            [RegularExpression("^[A-Za-z]{4,20}$", ErrorMessage = "Employee Position must only contain Letters")]
            public string emp_position { get; set; }

            [Display(Name = "Employee Department")]
            [Required(ErrorMessage = "The employee's department must be entered.")]
            public int dept_id { get; set; }

        }
    }
}