using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SSEExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SseController : ControllerBase
    {
        [HttpGet("stream")]
        public async Task Stream(CancellationToken cancellationToken)
        {
            Response.Headers.TryAdd("Content-Type", "text/event-stream");
            Response.Headers.TryAdd("Cache-Control", "no-cache");
            Response.Headers.TryAdd("Connection", "keep-alive");

            while (!cancellationToken.IsCancellationRequested)
            {
                var data = new { Time = DateTime.UtcNow };
                var jsonData = JsonSerializer.Serialize(data);
                await Response.WriteAsync($"data: {jsonData}\n\n");
                await Response.Body.FlushAsync();
                await Task.Delay(1000, cancellationToken); // Send updates every second
            }
        }
    }
}
