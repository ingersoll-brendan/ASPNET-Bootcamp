using Bootcamp.Client.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Bootcamp.Client.Blazor.Config;
using Bootcamp.Client.Blazor.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(Constants.Bootcamp_ApiHttpClientName, client =>
{
    client.BaseAddress = new Uri(Constants.Bootcamp_ApiBaseUrl);
});

builder.Services.AddSingleton<Bootcamp.Client.Blazor.Services.CustomerService>();


await builder.Build().RunAsync();
