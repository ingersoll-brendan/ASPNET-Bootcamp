using Bootcamp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Data.DbContexts
{
    public class BootcampContext : DbContext
    {
        public BootcampContext(DbContextOptions<BootcampContext> options): base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<AddressType> AddressTypes { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: 
            //modelBuilder.Entity<Order>()
            //    .HasMany(p => p.BillingAddress)
            //    .WithOne(c => c.)
            //    .OnDelete(DeleteBehavior.Cascade); // or Restrict, SetNull, etc.
        }
    }
}
