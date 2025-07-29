using Bootcamp.Client.Blazor.Config;
using Bootcamp.Client.Blazor.Pages;
using Bootcamp.Data.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace Bootcamp.Client.Blazor.Services
{
	public class AddressService(IHttpClientFactory HttpClientFactory)
	{
		private readonly HttpClient _httpClient = HttpClientFactory.CreateClient(Constants.Bootcamp_ApiHttpClientName);

		public async Task<Address[]> GetAddressesAsync()
		{
			return await _httpClient.GetFromJsonAsync<Address[]>(Constants.Bootcamp_Addresses) ?? [];
		}

		public async Task<Address[]> GetAddressesByCustomerIdAsync(int customerId)
		{
			return await _httpClient.GetFromJsonAsync<Address[]>($"{Constants.Bootcamp_AddressesByCustomerId.Replace("{id}", customerId.ToString())}") ?? [];
		}

		public async Task<Address?> GetAddressAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<Address>($"{Constants.Bootcamp_Address.Replace("{id}", id.ToString())}");
		}

		public async Task<Address> AddAddressAsync(Address address)
		{
			var response = await _httpClient.PostAsJsonAsync<Address>(Constants.Bootcamp_Addresses, address);
			response.EnsureSuccessStatusCode();

			var newAddress = await response.Content.ReadFromJsonAsync<Address>();

			return newAddress!;
		}

		public async Task UpdateAddressAsync(Address address)
		{
			var response = await _httpClient.PutAsJsonAsync<Address>($"{Constants.Bootcamp_Address.Replace("{id}", address.Id.ToString())}", address!);
			response.EnsureSuccessStatusCode();
		}

		public async Task DeleteAddressAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"{Constants.Bootcamp_Address.Replace("{id}", id.ToString())}");
			response.EnsureSuccessStatusCode();
		}
	}
}
