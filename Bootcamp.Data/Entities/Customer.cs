using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Data.Entities
{
    public class Customer
    {
        #region ID/Foreign Key Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }

        #endregion

        #region Attribute Properties

        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Required]
        public required long PhoneNumber {  get; set; }

        #endregion

        #region Navigation Properties

        public required ICollection<Address> Addresses { get; set; } = new List<Address>();
        public required ICollection<Order> Orders { get; set; } = new List<Order>();

        #endregion
    }
}
