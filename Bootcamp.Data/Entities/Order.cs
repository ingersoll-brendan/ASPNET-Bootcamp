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
        public required int Id { get; set; }

        [Required]
        public required int CustomerId { get; set; }

        [Required]
        public required int BillingAddressId { get; set; }

        [Required]
        public required int ShippingAddressId { get; set; }

        #endregion

        #region Attribute Properties

        [Required]
        public int OrderNumber { get; set; }

        #endregion

        #region Navigation Properties

        public required Customer Customer { get; set; }
        public required Address BillingAddress { get; set; }
        public required Address ShippingAddress { get; set; }

        #endregion
    }
}
