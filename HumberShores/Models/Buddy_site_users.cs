using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HumberShores.Models
{
    [MetadataType(typeof(site_usersMetadata))]
    public partial class site_users
    {
        class site_usersMetadata
        {
            [Key]
            [Display(Name = "User Id")]
            public int user_id { get; set; }

            [Display(Name = "Employee Id")]
            public Nullable<int> emp_id { get; set; }

            [Display(Name = "User Role")]
            [Required(ErrorMessage = "User role must be defined.")]
            [StringLength(3, ErrorMessage = "Role code must be 3 Letters.")]
            public string role_code { get; set; }

            [Display(Name = "First Name")]
            [Required(ErrorMessage = "First name must be entered.")]
            [StringLength(15, ErrorMessage = "First name must be less than 15 characters.")]
            [RegularExpression("^[A-Za-z]{2,15}$", ErrorMessage = "First Name must only contain Letters.")]
            public string user_first_name { get; set; }

            [Display(Name = "Last Name")]
            [Required(ErrorMessage = "Last name must be entered.")]
            [StringLength(20, ErrorMessage = "Last name must be less than 20 characters.")]
            [RegularExpression("^[A-Za-z]{2,20}$", ErrorMessage = "Last Name must only contain Letters.")]
            public string user_last_name { get; set; }

            [Display(Name = "Date of Birth")]
            [Required(ErrorMessage = "Date of birth must be entered.")]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
            public System.DateTime user_dob { get; set; }

            [Display(Name = "Gender")]
            [Required(ErrorMessage = "Gender must be entered.")]
            public string user_gender { get; set; }

            [Display(Name = "Street Address")]
            [Required(ErrorMessage = "Street Address must be entered.")]
            [RegularExpression("^[A-Za-z0-9 ]{2,30}$", ErrorMessage = "Street Address must only contain Letters and Numbers.")]
            public string user_address { get; set; }

            [Display(Name = "City")]
            [Required(ErrorMessage = "City must be entered.")]
            [RegularExpression("^[A-Za-z]{2,15}$", ErrorMessage = "City must only contain Letters")]
            public string user_city { get; set; }

            [Display(Name = "Province")]
            [Required(ErrorMessage = "Province must be entered.")]
            public string user_province { get; set; }

            [Display(Name = "Postal Code")]
            [Required(ErrorMessage = "Postal Code must be entered.")]
            [StringLength(6, ErrorMessage = "Postal Code must be 6 characters.")]
            [RegularExpression("^[A-Z0-9]{6}$", ErrorMessage = "Must be a valid Postal Code")]
            public string user_postal_code { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "Email must be entered.")]
            [StringLength(30, ErrorMessage = "Email must be less than 25 characters.")]
            [EmailAddress(ErrorMessage = "Must be a valid email address.")]
            public string user_email { get; set; }

            [Display(Name = "Username")]
            [Required(ErrorMessage = "Username must be entered.")]
            [StringLength(20, ErrorMessage = "Username must be less than 20 characters.")]
            [RegularExpression("^[A-Za-z0-9]{6,20}$", ErrorMessage = "Username must only be Letters and Numbers.")]
            public string user_username { get; set; }

            [Display(Name = "Password")]
            [Required(ErrorMessage = "Password must be entered.")]
            [StringLength(20, ErrorMessage = "Employee password must be less than 20 characters.")]
            public string user_password { get; set; }

            [Display(Name = "Phone Number")]
            [StringLength(15, ErrorMessage = "Employee phone number must be less than 15 numbers.")]
            [Phone(ErrorMessage = "Must be a valid phone number.")]
            public string user_phone { get; set; }

            private DateTime _user_date_joined = DateTime.Now;
            [Display(Name = "Date Joined")]
            [Required(ErrorMessage = "The date a user registered must be entered.")]
            [DataType(DataType.DateTime)]
            public DateTime user_date_joined
            {
                get
                {
                    return (_user_date_joined == DateTime.Now) ? DateTime.Now : _user_date_joined;
                }
                set
                {
                    _user_date_joined = value;
                }
            }
			
			//Full Name property to be used in the Maps feature, also inputed in the actual model - Paul Ooi
			public string full_name
			{
				get
				{
					return this.user_first_name + " " + this.user_last_name;
				}
			}
		}
    }
}