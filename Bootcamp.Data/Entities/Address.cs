using Bootcamp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Data.Entities
{
    public class Address
    {
        #region ID/Foreign Key Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Address Type Was Not Assigned")]
        public int AddressTypeId { get; set; }

        public int? CustomerId { get; set; }

        #endregion

        #region Attribute Properties

        [Required(ErrorMessage = "Street 1 is Required")]
        [MaxLength(50, ErrorMessage = "Street 1 cannot be longer than 50 characters")]
        public string? Street1 { get; set; }

        [MaxLength(50, ErrorMessage = "Street 2 cannot be longer than 50 characters")]
        public string? Street2 { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [MaxLength(50, ErrorMessage = "City cannot be longer than 50 characters")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State is Required")]
        [MaxLength(50, ErrorMessage = "State cannot be longer than 50 characters")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Zip is Required")]
        public int? Zip { get; set; }

        #endregion

        #region Navigation Properties

        public AddressType? AddressType { get; set; }
        //public Customer? Customer { get; set; }

        #endregion

    }
}
