using GoSafe.API.Interfaces;
using GoSafe.API.Models;
using GoSafe.Common;
using GoSafe.Dto.User;
using GoSafe.LoggerTool;
using GoSafe.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserRepo repo;
        private readonly Logger log;

        public UserController(AppDbContext context, IUserRepo repo, Logger log)
        {
            _context = context;
            this.repo = repo;
            this.log = log;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            try
            {
                var res = await repo.Register(req);
                return StatusCode(res.Result.StatusCode, res);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHandler.GetErrorResponse(ex));
            }
        }

    }
}
