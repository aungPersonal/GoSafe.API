using GoSafe.LoggerTool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly Logger log;

        public HealthCheckController(Logger log)
        {
            this.log = log;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is healthy");
        }

        [HttpPost("Log")]
        public async Task<IActionResult> Log()
        {
            log.Info("Log Test: Info");
            return Ok($"Log Test: Info");
        }

        [HttpGet("GetUTCNow")]
        public async Task<IActionResult> GetUTNow()
        {
            return Ok(DateTime.UtcNow);
        }
    }
}
