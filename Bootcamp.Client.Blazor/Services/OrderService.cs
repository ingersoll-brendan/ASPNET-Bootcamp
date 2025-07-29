using Bootcamp.Client.Blazor.Config;
using Bootcamp.Client.Blazor.Pages;
using Bootcamp.Data.Dtos;
using Bootcamp.Data.Entities;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace Bootcamp.Client.Blazor.Services
{
	public class OrderService(IHttpClientFactory HttpClientFactory)
	{
		private readonly HttpClient _httpClient = HttpClientFactory.CreateClient(Constants.Bootcamp_ApiHttpClientName);

		public async Task<OrderSearchResult> GetOrdersAsync(
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
			var queryString = Constants.Bootcamp_Orders + $"?skip={skip}&take={take}&descending={descending}";
			if (id != null && id > 0)
			{
				queryString += $"&id={id}";
			}
			if (customerId != null)
			{
				queryString += $"&customerId={customerId}";
			}
			if (billingAddressId != null)
			{
				queryString += $"&billingAddressId={billingAddressId}";
			}
			if (shippingAddressId != null)
			{
				queryString += $"&shippingAddressId={shippingAddressId}";
			}
			if (orderNumber != null)
			{
				queryString += $"&orderNum={orderNumber}";
			}
			if (dateCreated != null)
			{
				queryString += $"&dateCreated={dateCreated}";
			}
			if (!string.IsNullOrWhiteSpace(orderDescription))
			{
				queryString += $"&orderDescription={orderDescription}";
			}
			if (!string.IsNullOrWhiteSpace(orderBy))
			{
				queryString += $"&orderBy={orderBy}";
			}
			return await _httpClient.GetFromJsonAsync<OrderSearchResult>(queryString) ?? new OrderSearchResult
			{
				Orders = [],
				Count = 0
			};
		}

		public async Task<Order[]> GetOrdersByCustomerIdAsync(int customerId)
		{
			return await _httpClient.GetFromJsonAsync<Order[]>($"{Constants.Bootcamp_OrdersByCustomerId.Replace("{id}", customerId.ToString())}") ?? [];
		}

		public async Task<int> GetNumOfOrdersAsync()
		{
			var orders = await _httpClient.GetFromJsonAsync<Order[]>(Constants.Bootcamp_Orders);
			return orders?.Length ?? 0;
		}

		public async Task<Order?> GetOrderAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<Order>($"{Constants.Bootcamp_Order.Replace("{id}", id.ToString())}");
		}

		public async Task<Order> AddOrderAsync(Order order)
		{
			var response = await _httpClient.PostAsJsonAsync<Order>(Constants.Bootcamp_Orders, order);
			response.EnsureSuccessStatusCode();

			var newOrder = await response.Content.ReadFromJsonAsync<Order>();

			return newOrder!;
		}

		public async Task UpdateOrderAsync(Order order)
		{
			var response = await _httpClient.PutAsJsonAsync<Order>($"{Constants.Bootcamp_Order.Replace("{id}", order.Id.ToString())}", order!);
			response.EnsureSuccessStatusCode();
		}

		public async Task DeleteOrderAsync(int id)
		{

			var response = await _httpClient.DeleteAsync($"{Constants.Bootcamp_Order.Replace("{id}", id.ToString())}");
			response.EnsureSuccessStatusCode();
		}
	}
}
