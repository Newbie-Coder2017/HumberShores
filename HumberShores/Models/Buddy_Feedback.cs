using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberShores.Models
{

    [MetadataType(typeof(LoginMetadata))] //buddy class
    public partial class HOSPITAL_FEEDBACK //parent partial class
    {

        class LoginMetadata //buddy class
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Display(Name = "Feedback ID")]
            public int id_feedback { get; set; }

            [Display(Name = "First Name")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "First Name required")]//no empty strings
            public string f_name { get; set; }

            [Display(Name = "Last Name")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "First Name required")]//no empty strings
            public string l_name { get; set; }

            [Display(Name = "Contact Email")]
            [Required(ErrorMessage = "Contact email required")]
            public string contact_email { get; set; }

            [Display(Name = "Type of Feedback")]
            [Required(ErrorMessage = "Type of feedback required")]
            public string type_feedback { get; set; }

            [Display(Name = "Department for Feedback")]
            [Required(ErrorMessage = "Departement required")]
            public string depart_feedback { get; set; }

            [Display(Name = "Comment")]
            [Required(ErrorMessage = "Comment required")]
            [MaxLength(500, ErrorMessage = "Comment must not exceed 500 characters")]
            public string comment_feedback { get; set; }

            [Display(Name = "Date of Feedback")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Feedback date required")]
            public DateTime date_feedback { get; set; }

            [Display(Name = "Published Status")]
            [Required(ErrorMessage = "Status required")]
            public int is_publish { get; set; }

        }
    }
}