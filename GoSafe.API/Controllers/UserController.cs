using GoSafe.API.Common;
using GoSafe.API.Interfaces;
using GoSafe.API.Models;
using GoSafe.Common;
using GoSafe.Dto.User;
using GoSafe.LoggerTool;
using GoSafe.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            try
            {
                var res = await repo.Login(req);
                return StatusCode(res.Result.StatusCode, res);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHandler.GetErrorResponse(ex));
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest req)
        {
            try
            {
                if (req.AccessToken is null || req.RefreshToken is null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ErrorHandler.GetResponse("Check parameters"));
                }
                var res = await repo.RefreshToken(req);

                return StatusCode(res.Result.StatusCode, res);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHandler.GetErrorResponse(ex));
            }
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
