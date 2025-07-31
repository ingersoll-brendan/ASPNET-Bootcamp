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
	// TODO: Use a "Compiler" Statement (i.e. #if DEV, #if INT, #if PROD, etc.
	// Or does Blazor have AppSettings.json that can be used simaliar to Web Projects
	// and an run time, we can load the correct ones?
	client.BaseAddress = new Uri(Constants.Bootcamp_ApiBaseUrl);
});

builder.Services.AddSingleton<Bootcamp.Client.Blazor.Services.CustomerService>();
builder.Services.AddSingleton<Bootcamp.Client.Blazor.Services.AddressService>();
builder.Services.AddSingleton<Bootcamp.Client.Blazor.Services.OrderService>();


await builder.Build().RunAsync();
