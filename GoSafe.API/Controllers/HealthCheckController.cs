using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is healthy");
        }
    }
}
