using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace HumberShores.Models
{
    [MetadataType(typeof(VolunteersMetadata))]
    public partial class VOLUNTEER
    {
        class VolunteersMetadata
        {
            [Key]
            [Display(Name = "Volunteer Id")]
            public int VOLUNTEER_ID { get; set; }

            [Display(Name = "First Name")]
            [Required(ErrorMessage = "Please enter your first name")]
            [StringLength(15, ErrorMessage = "First name must be less than 15 characters.")]
            [RegularExpression("^[A-Za-z]{2,15}$", ErrorMessage = "First Name must only contain Letters.")]
            public string FIRST_NAME { get; set; }

            [Display(Name = "Middle Name")]
            [StringLength(15, ErrorMessage = "First name must be less than 15 characters.")]
            [RegularExpression("^[A-Za-z]{2,15}$", ErrorMessage = "First Name must only contain Letters.")]
            public string MIDDLE_NAME { get; set; }

            [Display(Name = "Last Name")]
            [Required(ErrorMessage = "Please enter your last name")]
            [StringLength(15, ErrorMessage = "First name must be less than 15 characters.")]
            [RegularExpression("^[A-Za-z]{2,15}$", ErrorMessage = "First Name must only contain Letters.")]
            public string LAST_NAME { get; set; }

            [Display(Name = "Occupation")]
            [Required(ErrorMessage = "Please enter your occupation")]
            public string OCCUPATION { get; set; }

            [Display(Name = "Opportunity")]
            [Required(ErrorMessage = "Please select an opportunity")]
            public int OPPORTUNITY_ID { get; set; }

            [Display(Name = "Phone Number")]
            [Required(ErrorMessage = "Please Enter a valid phone number")]
            [StringLength(15, ErrorMessage = "Employee phone number must be less than 15 numbers.")]
            [Phone(ErrorMessage = "Must be a valid phone number.")]
            public string PHONE_NUMBER { get; set; }

            [Display(Name = "Street Address")]
            [Required(ErrorMessage = "Street Address must be entered.")]
            [RegularExpression("^[A-Za-z0-9 ]{2,30}$", ErrorMessage = "Street Address must only contain Letters and Numbers.")]
            public string STREET_ADDRESS { get; set; }

            [Display(Name = "City")]
            [Required(ErrorMessage = "City must be entered.")]
            [RegularExpression("^[A-Za-z]{2,15}$", ErrorMessage = "City must only contain Letters")]
            public string CITY { get; set; }

            [Display(Name = "Province")]
            [Required(ErrorMessage = "Province must be entered.")]
            public string PROVINCE { get; set; }

            [Display(Name = "Postal Code")]
            [Required(ErrorMessage = "Postal Code must be entered.")]
            [StringLength(6, ErrorMessage = "Postal Code must be 6 characters.")]
            [RegularExpression("^[A-Z0-9]{6}$", ErrorMessage = "Must be a valid Postal Code")]
            public string POSTAL_CODE { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "Email must be entered.")]
            [StringLength(30, ErrorMessage = "Email must be less than 25 characters.")]
            [EmailAddress(ErrorMessage = "Must be a valid email address.")]
            public string EMAIL { get; set; }

            [Display(Name = "Date of Birth")]
            [Required(ErrorMessage = "Date of birth must be entered.")]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
            public System.DateTime DATE_OF_BIRTH { get; set; }

            [Display(Name = "Gender")]
            [Required(ErrorMessage = "Gender must be entered.")]
            public string GENDER { get; set; }

            [Display(Name = "Drivers Licence")]
            [Required(ErrorMessage = "Please select yes or no")]
            public string LICENCE { get; set; }

            [Display(Name = "Available to start")]
            [Required(ErrorMessage = "The day you're available to start is required")]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
            public System.DateTime START_DATE { get; set; }
        }
    }
}