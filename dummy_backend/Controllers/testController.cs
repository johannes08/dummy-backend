using System;
using System.Net.Http;
using System.Threading.Tasks;
using dummy_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dummy_backend.Controllers
{
    [Route("[Controller]")]
    public class TestController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;
        private readonly ITestRepository _testRepository;

        public TestController(IHttpClientFactory clientFactory, ILogger<ControlController> logger, ITestRepository testRepository)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _testRepository = testRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Test([FromQuery] int status)
        {
            return Ok("Hello World!");
        }

        [HttpPost]
        public async Task<IActionResult> TestPost()
        {
            await _testRepository.test();
            return Ok();
        }
    }
}