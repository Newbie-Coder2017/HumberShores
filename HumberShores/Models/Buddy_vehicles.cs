using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace HumberShores.Models
{
    [MetadataType(typeof(vehiclesMetadata))]
    public partial class VEHICLE
    {
        class vehiclesMetadata
        {
            [Display(Name = "Plate Number")]
            [Required(ErrorMessage = "Please enter your vehicle licence plate number")]
            public string LICENSE_NUMBER { get; set; }

            [Display(Name = "User Id")]
            public int USER_ID { get; set; }

            [Display(Name = "Make")]
            public string MAKE { get; set; }

            [Display(Name = "Model")]
            public string MODEL { get; set; }

            [Display(Name = "Year")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> YEAR { get; set; }
        }
    }
   
}