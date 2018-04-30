using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace HumberShores.Models
{
    [MetadataType(typeof(passtypeMetadata))]
    public partial class PASS_TYPE
    {
        class passtypeMetadata
        {
            [Display(Name = "Pass Type Id")]
            public int TYPE_ID { get; set; }

            [Display(Name = "Pass Type")]
            [Required(ErrorMessage = "The name of this type of pass is required")]
            public string PASS_TYPE1 { get; set; }

            [Display(Name = "Price")]
            [Required(ErrorMessage = "The price of the pass is required")]
            public int PRICE { get; set; }
        }
    }
    
}