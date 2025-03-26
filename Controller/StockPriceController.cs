using Microsoft.AspNetCore.Mvc;
using StockPriceAPI.Model;
using System.Text.Json;

namespace StockPriceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockPriceController : ControllerBase
    {

        private static readonly Dictionary<string, double> stocks = new Dictionary<string, double>()
        {
            { "Tata Power", 150.00 },
            { "Reliance", 2800.00 },
            { "Adani Power", 3400.00 },
            { "Suzlon", 299.00 }
        };

        private static readonly Random random = new Random();
        [HttpGet("stream")]
        public async Task Get()
        {
            Response.ContentType = "text/event-stream";

            while (true)
            {
                // Declare the dictionary to store stock updates
                var updatedStocks = new Dictionary<string, StockInfo>();

                // Simulate stock price updates
                foreach (var stock in stocks.Keys.ToList())
                {
                    var oldPrice = stocks[stock];

                    // Simulate price fluctuation
                    var newPrice = oldPrice + random.NextDouble() * 10 - 5;
                    stocks[stock] = Math.Round(newPrice, 2);
                    var isUp = newPrice > oldPrice;

                    // Store updated stock data using the StockInfo class
                    updatedStocks[stock] = new StockInfo { newPrice = stocks[stock], isUp = isUp };
                }

                var json = JsonSerializer.Serialize(updatedStocks);
                await Response.WriteAsync($"data: {json}\n\n");
                await Response.Body.FlushAsync();

                await Task.Delay(2000); // Delay to simulate real-time updates (every 2 seconds)
            }
        }
    }
}