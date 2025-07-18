﻿using Bootcamp.Data.Enums;
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

        [Required]
        public required int AddressTypeId { get; set; }

        [Required]
        public required int CustomerId { get; set; }

        #endregion

        #region Attribute Properties

        [Required]
        [MaxLength(50)]
        public required string Street1 { get; set; }

        [MaxLength(50)]
        public string? Street2 { get; set; }

        [Required]
        [MaxLength(50)]
        public required string City { get; set; }

        [Required]
        [MaxLength(50)]
        public required string State { get; set; }

        [Required]
        public required int Zip { get; set; }

        #endregion

        #region Navigation Properties

        public AddressType? AddressType { get; set; }
        public Customer? Customer { get; set; }

        #endregion

    }
}
