using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberShores.Models
{

    [MetadataType(typeof(VacancyMetadata))] //buddy class
    public partial class HOSPITAL_VACANCY //parent partial class
    {

        class VacancyMetadata //buddy class

        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Display(Name = "Vacancy ID")]
            public int vacancy_id { get; set; }

            [Display(Name = "Job Title")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Job title required")]//no empty strings
            public string job_title { get; set; }

            [Display(Name = "Application Due Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Submission due date required")]
            public DateTime apply_due_date { get; set; }

            [Display(Name = "Description")]
            [Required(ErrorMessage = "Job description required")]
            [MaxLength(500, ErrorMessage = "Job Description must not exceed 500 characters")]
            public string description { get; set; }

            [Display(Name = "Department Id")]
            [Required(ErrorMessage = "Departement id required")]
            public int description_id { get; set; }

            [Display(Name = "Job Type")]
            [Required(ErrorMessage = "Job type required")]
            [MaxLength(ErrorMessage = "Job Type cant be more than 2 characters")]
            [MinLength(2, ErrorMessage = "Job Type needs to be 2 characters")]
            public string job_type { get; set; }

            [Display(Name = "Number of Vacancies")] //optional feild
            public int no_of_vacancy { get; set; }

            [Display(Name = "Contact Email")]
            [Required(ErrorMessage = "Contact email required")]
            public string contact_email { get; set; }

        }
    }
}