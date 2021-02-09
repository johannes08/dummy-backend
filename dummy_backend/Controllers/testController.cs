using System;
using System.Net.Http;
using System.Threading.Tasks;
using dummy_backend.Interfaces;
using dummy_backend.Models;
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
            return Ok(await _testRepository.Get());
        }

        [HttpPost]
        public async Task<IActionResult> TestPost([FromBody] EntryDto entryDto)
        {
            return Ok(await _testRepository.Post(entryDto));
        }
        
        [HttpPatch]
        public async Task<IActionResult> TestPost([FromBody] Entry entry)
        {
            return Ok(await _testRepository.Patch(entry));
        }
        
        [HttpDelete]
        public async Task<IActionResult> TestPost([FromQuery] int id)
        {
            await _testRepository.Delete(id);
            return Ok();
        }
    }
}