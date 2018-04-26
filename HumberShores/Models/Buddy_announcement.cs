using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HumberShores.Models
{
    [MetadataType(typeof(announcementMetadata))]
    public partial class announcement
    {
        class announcementMetadata
        {
            [Key]
            [Display(Name = "Announcement ID")]
            public int ann_id { get; set; }

            [Display(Name = "Announcement Text")]
            [Required(ErrorMessage = "The announcement's text must be entered.")]
            [StringLength(200, ErrorMessage = "The announcement's text must be less than 200 characters.")]
            public string ann_text { get; set; }

            private DateTime _ann_date = DateTime.Now;
            [Display(Name = "Date Posted/Last Update")]
            [Required(ErrorMessage = "The announcement's date must be entered.")]
            [DataType(DataType.DateTime)]
            public DateTime ann_date
            {
                get
                {
                    return (_ann_date == DateTime.Now) ? DateTime.Now : _ann_date;
                }
                set
                {
                    _ann_date = value;
                }
            }

            [Display(Name = "Severity of Announcement")]
            [Required(ErrorMessage = "The severity of the announcement must be declared.")]
            public string ann_severity { get; set; }

            [Display(Name = "Visible in Announcement Bar")]
            public bool ann_visible { get; set; } = true;

            [Display(Name = "Authorized by Employee")]
            public int emp_id { get; set; }

        }
    }
}