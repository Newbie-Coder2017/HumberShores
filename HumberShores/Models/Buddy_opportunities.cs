using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace HumberShores.Models
{
    [MetadataType(typeof(opportunitiesMetadata))]
    public partial class OPPORTUNITY
    {
        class opportunitiesMetadata
        {
            [Key]
            [Display(Name = "Opportunity Id")]
            public int OPPORTUNITY_ID { get; set; }

            [Display(Name = "Opportunity")]
            [Required(ErrorMessage = "The title of this opportunity must be entered.")]
            [StringLength(50, ErrorMessage = "The title's text must be less than 50 characters.")]
            public string OPPORTUNITY_TITLE { get; set; }

            [Display(Name = "Description")]
            [Required(ErrorMessage = "Please give a brief description to this opportunity")]
            public string OPPORTUNITY_DESC { get; set; }

            [Display(Name = "Department")]
            [Required(ErrorMessage = "Pleas select the department where this opportunity is available")]
            public int DEPT_ID { get; set; }

            [Display(Name = "Start Date")]
            [Required(ErrorMessage = "When is the start date for this opportunity")]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime START_DATE { get; set; }

            [Display(Name = "Contact Person's First Name")]
            [Required(ErrorMessage = "Please enter the first name of the contact person for this opportunity")]
            public string CONTACT_FN { get; set; }

            [Display(Name = "Contact Person's Last Name")]
            [Required(ErrorMessage = "Please enter the Last name of the contact person for this opportunity")]
            public string CONTACT_LN { get; set; }

            [Display(Name = "Contact Person's Email")]
            [Required(ErrorMessage = "Please enter the email address of the contact person for this opportunity")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address")]
            public string CONTACT_EMAIL { get; set; }
        }
    }
}