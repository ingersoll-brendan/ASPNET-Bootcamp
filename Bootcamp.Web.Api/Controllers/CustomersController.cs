using Bootcamp.Data.DbContexts;
using Bootcamp.Data.Dtos;
using Bootcamp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bootcamp.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BootcampContext _context;

        public CustomersController(BootcampContext context)
        {
            _context = context;
        }

		// GET: api/Customers
		[HttpGet]
        public async Task<ActionResult<CustomerSearchResult>> GetCustomers(
            int skip = 0, 
            int take = 5, 
            bool descending = false, 
            int? id = null, 
            string? firstName = null, 
            string? lastName = null, 
            string? email = null, 
            string? phoneNumber = null, 
            string? orderBy = null) 
        {
			//NOTE: Alternative way to load customers with their included address objects for frontend if desired
			//return await _context.Customers.Include(c => c.Addresses).ToListAsync();
			//return await _context.Customers.ToListAsync();
		    var searchQuery = _context.Customers.AsQueryable();
			if (id != null && id > 0)
			{
				searchQuery = searchQuery.Where(c => c.Id.Equals(id));
			}
			if (!string.IsNullOrWhiteSpace(firstName))
            {
                searchQuery = searchQuery.Where(c => c.FirstName != null && c.FirstName.Contains(firstName));
            }
			if (!string.IsNullOrWhiteSpace(lastName))
			{
				searchQuery = searchQuery.Where(c => c.LastName != null && c.LastName.Contains(lastName));
			}
			if (!string.IsNullOrWhiteSpace(email))
			{
				searchQuery = searchQuery.Where(c => c.Email != null && c.Email.Contains(email));
			}
			if (!string.IsNullOrWhiteSpace(phoneNumber))
			{
				searchQuery = searchQuery.Where(c => c.PhoneNumber != null && c.PhoneNumber.Contains(phoneNumber));
			}
            if(!string.IsNullOrWhiteSpace(orderBy))
            {
                if(orderBy == "Id")
                {
					searchQuery = descending ? searchQuery.OrderByDescending(c => c.Id) : searchQuery.OrderBy(c => c.Id);
                }
                else if(orderBy == "FirstName")
				{
					searchQuery = descending ? searchQuery.OrderByDescending(c => c.FirstName) : searchQuery.OrderBy(c => c.FirstName);
				}
                else if(orderBy == "LastName")
                {
                    searchQuery = descending ? searchQuery.OrderByDescending(c => c.LastName) : searchQuery.OrderBy(c => c.LastName);
                }
                else if(orderBy == "Email")
                {
                    searchQuery = descending ? searchQuery.OrderByDescending(c => c.Email) : searchQuery.OrderBy(c => c.Email);
                }
                else if(orderBy == "PhoneNumber")
                {
                    searchQuery = descending ? searchQuery.OrderByDescending(c => c.PhoneNumber) : searchQuery.OrderBy(c => c.PhoneNumber);
				}
			}

			var customers = await searchQuery.Skip(skip).Take(take).ToListAsync();
            var customerCount = searchQuery.Skip(0).Count();

			return new CustomerSearchResult
            {
                Customers = customers,
                Count = customerCount
            };
		}

		// GET: api/Customers/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
			//NOTE: Alternative way to load customers with their included address objects for frontend if desired
            //var customer = await _context.Customers.Include(c => c.Addresses).SingleOrDefaultAsync((c => c.Id == id));
			var customer = await _context.Customers.FindAsync(id);


            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
			customer.PhoneNumber = Regex.Replace(customer.PhoneNumber ?? "", @"[^\d]", "");
			_context.Entry(customer).State = EntityState.Modified;

            foreach(var address in customer.Addresses)
            {
                if(address.Id == 0)
                {
                    _context.Entry(address).State = EntityState.Added;
                }
                else
                {
                    _context.Entry(address).State = EntityState.Modified;
				}
			}

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 201
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            customer.PhoneNumber = Regex.Replace(customer.PhoneNumber ?? "", @"[^\d]", "");
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
