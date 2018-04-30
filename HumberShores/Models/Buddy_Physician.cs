using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HumberShores.Models
{
	[MetadataType(typeof(PhysicianMetadata))]
	public partial class physician
	{
		class PhysicianMetadata
		{

			[Display(Name = "Physician ID#")]
			public int doctor_id { get; set; }

			[Display(Name = "Employee Name")]
			public int emp_id { get; set; }

			[Display(Name = "Department")]
			[Required(ErrorMessage = "Department must be identified")]
			public Nullable<int> department_id { get; set; }

			[Display(Name = "Specialty #1")]
			public Nullable<int> special1 { get; set; }

			[Display(Name = "Specialty #2")]
			public Nullable<int> special2 { get; set; }
			
			[Display(Name = "Phone Number")]
			[MinLength(10, ErrorMessage = "Must provide phone number with area code")]
			[DataType(DataType.PhoneNumber, ErrorMessage = "Must provide a valid phone number")]
			public string phone { get; set; }

			[Display(Name = "Fax Number")]
			[DataType(DataType.PhoneNumber, ErrorMessage = "Must provide a valid fax number")]
			public string fax { get; set; }

			[Display(Name = "E-mail")]
			[DataType(DataType.EmailAddress, ErrorMessage = "Must provide a valid Email")]
			public string email { get; set; }

			[Display(Name = "Office Website")]
			[Url(ErrorMessage = "Not valid URL address")]
			[DataType(DataType.Url, ErrorMessage = "Provide a valid website address")]
			public string website { get; set; }

			[Display(Name = "Street Address 1")]
			[MaxLength(100, ErrorMessage = "Must not be longer than 100 characters")]
			public string street_address1 { get; set; }

			[Display(Name = "Street Address 2")]
			[MaxLength(100, ErrorMessage = "Must not be longer than 100 characters")]
			public string street_address2 { get; set; }

			[Display(Name = "Building Name")]
			[MaxLength(100, ErrorMessage = "Must not be longer than 100 characters")]
			public string building_name { get; set; }

			[Display(Name = "City/Municipality")]
			[MaxLength(50, ErrorMessage = "Must not be longer than 50 characters")]
			public string city { get; set; }

			[Display(Name = "Province")]
			public Nullable<int> province { get; set; }

			[Display(Name = "Postal Code")]
			[MaxLength(7, ErrorMessage = "Must not be longer than 7 characters")]
			[MinLength(6, ErrorMessage = "Must not be less than 6 characters")]
			[RegularExpression("^[A-Za-z][0-9][A-Za-z] ?[0-9][A-Za-z][0-9]$", ErrorMessage = "Must be a valid Canadian Postal Code X3X 3X3")]
			[DataType(DataType.PostalCode, ErrorMessage = "Must enter valid Postal Code")]
			public string postal_code
			{
				get
				{
					return this.postal_code.ToUpper();
				}
				set
				{
					this.postal_code = value.ToUpper();

				}
			}
		}

	}
}