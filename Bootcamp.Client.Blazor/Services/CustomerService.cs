using Bootcamp.Client.Blazor.Config;
using Bootcamp.Client.Blazor.Pages;
using Bootcamp.Data.Entities;
using System.Net.Http.Json;

namespace Bootcamp.Client.Blazor.Services
{
    public class CustomerService(IHttpClientFactory HttpClientFactory)
    {
        private readonly HttpClient _httpClient = HttpClientFactory.CreateClient(Constants.Bootcamp_ApiHttpClientName);

        public async Task<Customer[]> GetCustomersAsync()
        {
            // TODO: Use Constants
            return await _httpClient.GetFromJsonAsync<Customer[]>("api/customers") ?? [];
        }

        public async Task<Customer?> GetCustomerAsync(int id)
        {
            // TODO: Use Constants
            return await _httpClient.GetFromJsonAsync<Customer>($"{Constants.Bootcamp_Customer.Replace("{id}", id.ToString())}");
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync<Customer>("api/customers", customer);
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
            var response = await _httpClient.DeleteAsync($"api/customers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
