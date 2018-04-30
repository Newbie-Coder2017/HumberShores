using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace HumberShores.Models
{
    [MetadataType(typeof(passMetadata))]
    public partial class PASS
    {
        class passMetadata
        {
            public int PASS_ID { get; set; }
            public int USER_ID { get; set; }

            [Display(Name = "Type of Pass")]
            public int PASS_TYPE { get; set; }

            [Display(Name = "Purchase Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime PURCHASE_DATE { get; set; }

            [Display(Name = "Expiry Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime EXPIRY_DATE { get; set; }

            [Display(Name = "Success")]
            public string PURCHASE_SUCCESS { get; set; }
            
        }
    }   
}