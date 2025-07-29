using Bootcamp.Data.DbContexts;
using Bootcamp.Data.Dtos;
using Bootcamp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bootcamp.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly BootcampContext _context;

        public OrdersController(BootcampContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<OrderSearchResult>> GetOrders(
            int skip = 0,
            int take = 5,
            bool descending = false,
            int? id = null,
            int? customerId = null,
            int? billingAddressId = null,
            int? shippingAddressId = null,
            int? orderNumber = null,
            string? dateCreated = null,
            string? orderDescription = null,
            string? orderBy = null)
        {
			//return await _context.Orders.Where(o => o.Customer != null && o.Customer.FirstName.Contains(firstName)).ToListAsync();
			var searchQuery = _context.Orders.AsQueryable();
			if (id != null && id > 0)
			{
				searchQuery = searchQuery.Where(c => c.Id.Equals(id));
			}
			if (customerId != null && customerId > 0)
			{
				searchQuery = searchQuery.Where(c => c.CustomerId.Equals(customerId));
			}
			if (billingAddressId != null && billingAddressId > 0)
			{
				searchQuery = searchQuery.Where(c => c.BillingAddressId.Equals(billingAddressId));
			}
			if (shippingAddressId != null && shippingAddressId > 0)
			{
				searchQuery = searchQuery.Where(c => c.ShippingAddressId.Equals(shippingAddressId));
			}
			if (orderNumber != null && orderNumber > 0)
			{
				searchQuery = searchQuery.Where(c => c.OrderNumber.Equals(orderNumber));
			}
            //TODO: fix dateCreated searching, cant search with characters like "6/"
			if (!string.IsNullOrWhiteSpace(dateCreated))
			{
				searchQuery = searchQuery.Where(c => c.DateCreated != null && (c.DateCreated.ToString() ?? string.Empty).Contains(dateCreated));
			}
			if (!string.IsNullOrWhiteSpace(orderDescription))
			{
				searchQuery = searchQuery.Where(c => c.OrderDescription != null && c.OrderDescription.Contains(orderDescription));
			}
			if (!string.IsNullOrWhiteSpace(orderBy))
			{
				if (orderBy == "Id")
				{
					searchQuery = descending ? searchQuery.OrderByDescending(c => c.Id) : searchQuery.OrderBy(c => c.Id);
				}
				else if (orderBy == "CustomerId")
				{
					searchQuery = descending ? searchQuery.OrderByDescending(c => c.CustomerId) : searchQuery.OrderBy(c => c.CustomerId);
				}
				else if (orderBy == "BillingAddressId")
				{
					searchQuery = descending ? searchQuery.OrderByDescending(c => c.BillingAddressId) : searchQuery.OrderBy(c => c.BillingAddressId);
				}
				else if (orderBy == "ShippingAddressId")
				{
					searchQuery = descending ? searchQuery.OrderByDescending(c => c.ShippingAddressId) : searchQuery.OrderBy(c => c.ShippingAddressId);
				}
				else if (orderBy == "OrderNumber")
				{
					searchQuery = descending ? searchQuery.OrderByDescending(c => c.OrderNumber) : searchQuery.OrderBy(c => c.OrderNumber);
				}
                else if (orderBy == "DateCreated")
                {
                    searchQuery = descending ? searchQuery.OrderByDescending(c => c.DateCreated) : searchQuery.OrderBy(c => c.DateCreated);
                }
                else if (orderBy == "OrderDescription")
				{
					searchQuery = descending ? searchQuery.OrderByDescending(c => c.OrderDescription) : searchQuery.OrderBy(c => c.OrderDescription);
				}
			}

			var orders = await searchQuery.Skip(skip).Take(take).ToListAsync();
			var orderCount = searchQuery.Skip(0).Count();

            return new OrderSearchResult
            {
                Orders = orders,
                Count = orderCount
            };
		}

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            //TODO: generate an order number (random number + character)
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

		// GET: api/Orders/Customer/{customerId}
		[HttpGet("Customer/{customerId}")]
		public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomerId(int customerId)
		{
			var orders = await _context.Orders
                .Include(o => o.BillingAddress)
                .Include(o => o.ShippingAddress)
				.Where(o => o.CustomerId == customerId).ToListAsync();

			return orders;

		}

		private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
