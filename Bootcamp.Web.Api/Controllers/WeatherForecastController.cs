using Bootcamp.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, Bootcamp1Context dbContext) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // LINQ "Method" Syntax -> C# -- SQL Conversion
            var method_customers = dbContext
                .Customers
                .Where(c => c.FirstName == "Bob")
                .OrderBy(c => c.Id)
                //.Select(c => new { c.Id, c.FirstName, c.Orders }) // <-- Projection
                .Select(c => new { c.Id, c.FirstName }) // <-- Projection
                .ToArray();

            var localCustomers = dbContext.Customers.Local;
            var localOrders = dbContext.Orders.Local;

            // LINQ "Expression" Syntax"
            var expression_customers = 
                (
                    from c in dbContext.Customers
                    //join o in dbContext.Orders on c.Id equals o.CustomerId
                    where c.FirstName == "Bob"
                    orderby c.Id
                    select c
                    //select new { c.Id, c.FirstName } // <-- Projection
                )
                .ToArray();

            var localCustomers1 = dbContext.Customers.Local;
            var localOrders1 = dbContext.Orders.Local;

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
