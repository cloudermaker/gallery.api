using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json.Serialization;

namespace gallery.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"

    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        private void testDapper()
        {
            var sql = "select * from \"user\"";

            var connectionString = _configuration.GetConnectionString("gallery");
            using var connection2 = new NpgsqlConnection(connectionString);

            var products = connection2.Query<Product>(sql).ToList();
            var str = String.Join(',', products.Select(a => a.Name));
            _logger.LogInformation(str);

            /*
            using (var connection = new SqlConnection(connString2))
            {
                connection.Open();
                var products = connection.Query<Product>(sql).ToList();
                var str = String.Join(',', products.Select(a => a.Name));
                _logger.LogInformation(str);
            }*/
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("coucou pierre");

            testDapper();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private class Product
        {
            public string Id { get; set; }

            //[JsonPropertyName("name")]
            public string Name { get; set; }
        }
    }
}