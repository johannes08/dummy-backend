using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dummy_backend.Controllers
{
    [Route("[Controller]")]
    public class TestController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;

        public TestController(IHttpClientFactory clientFactory, ILogger<ControlController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Test([FromQuery] int status)
        {
            return Ok("Hello World!");
        }
    }
}