using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumberShores.Models
{
	[MetadataType(typeof(DepartmentMetadata))]
	public partial class department
	{

		class DepartmentMetadata
		{

			[Display(Name = "Department Name")]
			[Required(ErrorMessage = "Department Name must be identified")]
			[RegularExpression("[-A-Za-z &]{1,}", ErrorMessage = "Must be letters only, may include - and & symbols")]
			public string dept_name { get; set; }

			[Display(Name = "Department Head")]
			[RegularExpression("[^0][1-9]*[0-9]?", ErrorMessage = "Must Select an employee as Department Head#")]
			[Required(ErrorMessage = "Must Select an employee as Department Head")]
			//[ForeignKey("emp_id")]
			public int dept_head { get; set; }

			[Display(Name = "Description")]
			public string dept_desc { get; set; }

			[Display(Name = "Department Extension")]
			[Required(ErrorMessage = "Must provide extension")]
			[RegularExpression("[0-9]{3,5}", ErrorMessage = "Must be between 3 and 5 digits in length, and can only be between 0 and 9")]
			public string dept_phone { get; set; }

			[Display(Name = "Section Location")]
			public Nullable<int> section { get; set; }
		}
	}
}