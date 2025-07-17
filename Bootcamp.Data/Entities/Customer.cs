using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Bootcamp.Data.Entities
{
	public class Customer
	{
		#region ID/Foreign Key Properties
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		#endregion

		#region Attribute Properties

		[Required(ErrorMessage = "First Name is Required")]
		[MaxLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
		public string? FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is Required")]
		[MaxLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
		public string? LastName { get; set; }

		[Required(ErrorMessage = "Email is Required")]
		[MaxLength(255, ErrorMessage = "Email cannot be longer than 255 characters")]
		[RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")]
		public string? Email { get; set; }


		// TODO: Change PhoneNumber to string and add validation for phone number format using Regex
		[Required(ErrorMessage = "Phone Number is Required")]
		[MaxLength(10, ErrorMessage = "Phone Number cannot be longer than 50 characters")]
		public string? PhoneNumber { get; set; }

		#endregion

		#region Navigation Properties

		public ICollection<Address> Addresses { get; set; } = new List<Address>();
		public ICollection<Order> Orders { get; set; } = new List<Order>();

		#endregion
	}
}
