using Bootcamp.Client.Blazor.Config;
using Bootcamp.Client.Blazor.Pages;
using Bootcamp.Data.Dtos;
using Bootcamp.Data.Entities;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace Bootcamp.Client.Blazor.Services
{
    public class CustomerService(IHttpClientFactory HttpClientFactory)
    {
        private readonly HttpClient _httpClient = HttpClientFactory.CreateClient(Constants.Bootcamp_ApiHttpClientName);

        public async Task<CustomerSearchResult> GetCustomersAsync(
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
            var queryString = Constants.Bootcamp_Customers + $"?skip={skip}&take={take}&descending={descending}";
			if (id != null && id > 0)
			{
				queryString += $"&id={id}";
			}
			if (!string.IsNullOrWhiteSpace(firstName))
            {
                queryString += $"&firstName={firstName}";
            }
			if (!string.IsNullOrWhiteSpace(lastName))
			{
				queryString += $"&lastName={lastName}";
			}
			if (!string.IsNullOrWhiteSpace(email))
			{
				queryString += $"&email={email}";
			}
			if (!string.IsNullOrWhiteSpace(phoneNumber))
			{
				queryString += $"&phoneNumber={phoneNumber}";
			}
			if (!string.IsNullOrWhiteSpace(orderBy))
			{
				queryString += $"&orderBy={orderBy}";
			}
			return await _httpClient.GetFromJsonAsync<CustomerSearchResult>(queryString) ?? new CustomerSearchResult
            {
                Customers = [],
                Count = 0
            };
        }

		public async Task<int> GetNumOfCustomersAsync()
		{
			var customers = await _httpClient.GetFromJsonAsync<Customer[]>(Constants.Bootcamp_Customers);
			return customers?.Length ?? 0;
		}

		public async Task<Customer?> GetCustomerAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Customer>($"{Constants.Bootcamp_Customer.Replace("{id}", id.ToString())}");
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync<Customer>(Constants.Bootcamp_Customers, customer);
            response.EnsureSuccessStatusCode();

            var newCustomer = await response.Content.ReadFromJsonAsync<Customer>();

            return newCustomer!;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PutAsJsonAsync<Customer>($"{Constants.Bootcamp_Customer.Replace("{id}", customer.Id.ToString())}", customer!);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCustomerAsync(int id)
        {

            var response = await _httpClient.DeleteAsync($"{Constants.Bootcamp_Customer.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();
        }
    }
}
