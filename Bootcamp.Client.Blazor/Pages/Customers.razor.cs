using Bootcamp.Data.Entities;
using Bootcamp.Client.Blazor.Config;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace Bootcamp.Client.Blazor.Pages
{
    public partial class Customers : ComponentBase
    {
        private Customer[] customers = [];

        // TODO: Fix to replace with adding CustomerService
        private HttpClient bootcampApiClient = default!;

        [Inject]
        public IHttpClientFactory HttpClientFactory { 
            set 
            {
                bootcampApiClient = value.CreateClient(Constants.Bootcamp_ApiHttpClientName);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            this.customers = await bootcampApiClient.GetFromJsonAsync<Customer[]>(Constants.Bootcamp_Customers);
        }
    }
}
