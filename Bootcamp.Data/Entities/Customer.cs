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
	public class Customer : IValidatableObject
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
		[RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Email must be in the proper format")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Phone Number is Required")]
		[MaxLength(15, ErrorMessage = "Phone Number cannot be longer than 15 characters")]
		[RegularExpression(@"^\(\d{3}\)\s?\d{3}-\d{4}$", ErrorMessage = "Phone Number must be in the proper format, ex: (123)456-7890")]
		public string? PhoneNumber { get; set; }

		#endregion

		#region Navigation Properties

		public ICollection<Address> Addresses { get; set; } = new List<Address>();
		public ICollection<Order> Orders { get; set; } = new List<Order>();

		#endregion

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			foreach (var address in Addresses)
			{
				var results = new List<ValidationResult>();
				var context = new ValidationContext(address);
				Validator.TryValidateObject(address, context, results, true);

				foreach (var result in results)
				{
					// Prefix the error with something to indicate which address
					yield return new ValidationResult($"Address: {result.ErrorMessage}", result.MemberNames);
				}
			}
		}
	}
}
