using Bootcamp.Client.Blazor;
using Bootcamp.Client.Blazor.Config;
using Bootcamp.Client.Blazor.Pages;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(Constants.Bootcamp_ApiHttpClientName, client =>
{
    client.BaseAddress = new Uri(Constants.Bootcamp_ApiBaseUrl);
});

builder.Services.AddSingleton<Bootcamp.Client.Blazor.Services.CustomerService>();
builder.Services.AddSingleton<Bootcamp.Client.Blazor.Services.AddressService>();
builder.Services.AddSingleton<Bootcamp.Client.Blazor.Services.OrderService>();


await builder.Build().RunAsync();
