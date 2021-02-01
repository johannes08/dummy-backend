using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dummy_backend.Controllers
{
    [Route("[Controller]")]
    public class ControlController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;

        public ControlController(IHttpClientFactory clientFactory, ILogger<ControlController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SwitchLight([FromQuery] int status)
        {
            var client = _clientFactory.CreateClient();

            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://192.168.188.195/?{status}")
            };

            try
            {
                await client.SendAsync(message);
            }
            catch (Exception e)
            {
            }
            
            _logger.LogInformation("Request to send light Status sent", new
            {
                status
            });

            return Ok();
        }
    }
}