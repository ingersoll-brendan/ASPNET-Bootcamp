using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Data.Entities
{
    public class Order
    {
        #region ID/Foreign Key Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "CustomerId is Required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Billing Address Id is Required")]
        public int BillingAddressId { get; set; }

        [Required(ErrorMessage = "Shipping Address Id is Required")]
        public int ShippingAddressId { get; set; }

        #endregion

        #region Attribute Properties

        [Required(ErrorMessage = "Order Number is Required")]
        public string OrderNumber { get; set; }

        [Required(ErrorMessage = "Date Created is Required")]
        public DateTimeOffset? DateCreated { get; set; }

        [Required(ErrorMessage = "Order Description is Required")]
        [MaxLength(200, ErrorMessage = "Order Description cannot be longer than 200 characters")]
		public string? OrderDescription { get; set; }



		#endregion

		#region Navigation Properties

		public Customer? Customer { get; set; }
        public Address? BillingAddress { get; set; }
        public Address? ShippingAddress { get; set; }

		#endregion
	}
}
