using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HumberShores.Models
{
    [MetadataType(typeof(appointmentMetaData))]
    public partial class appointment
    {
        class appointmentMetaData
        {
            [Key]
            [Display(Name = "Doctor")]
            public int emp_id { get; set; }

            [Display(Name = "Day of Appointment")]
            [Required(ErrorMessage = "Appointment Day must be selected")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
            //[FutureDate]
            public System.DateTime app_date { get; set; }

            [Display(Name = "Time of Appointment")]
            [Required(ErrorMessage = "Appointment Time must be selected")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
            public System.DateTime app_time { get; set; }

            [Display(Name = "Reason for Appointment")]
            [Required(ErrorMessage = "Please enter your reason for booking an appointment")]
            public string app_reason { get; set; }

            [Display(Name = "Extra Comments/Concerns")]
            [DisplayFormat(NullDisplayText = "N/A")]
            public string app_comment { get; set; }

            [Display(Name = "Is the appointment for a child or dependent?")]
            public bool app_child { get; set; }

            [Display(Name = "Child's First Name")]
            [DisplayFormat(NullDisplayText = "Not Applicable")]
            public string app_child_first { get; set; }

            [Display(Name = "Child's Last Name")]
            [DisplayFormat(NullDisplayText = "Not Applicable")]
            public string app_child_last { get; set; }

            [Display(Name = "Child's Date of Birth")]
            [DisplayFormat(NullDisplayText = "Not Applicable", ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
            public Nullable<System.DateTime> app_child_dob { get; set; }

            [Display(Name = "Child's Gender")]
            [DisplayFormat(NullDisplayText = "Not Applicable")]
            public string app_child_gender { get; set; }
        }
    }
}