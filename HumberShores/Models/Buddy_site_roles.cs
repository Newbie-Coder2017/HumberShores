using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HumberShores.Models
{
    [MetadataType(typeof(site_rolesMetadata))]
    public partial class site_roles
    {
        class site_rolesMetadata
        {
            [Key]
            [Display(Name = "Role")]
            public string role_code { get; set; }

            [Display(Name = "Role Name")]
            public string role_name { get; set; }

            [Display(Name = "Role Description")]
            public string role_description { get; set; }

        }
    }
}


