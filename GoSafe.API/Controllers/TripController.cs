using GoSafe.API.Dtos.Trip;
using GoSafe.API.Interfaces;
using GoSafe.API.Models;
using GoSafe.API.Utility;
using GoSafe.Common;
using GoSafe.LoggerTool;
using GoSafe.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace GoSafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITripRepo repo;
        private readonly Logger log;

        public TripController(AppDbContext context, ITripRepo repo, Logger log)
        {
            _context = context;
            this.repo = repo;
            this.log = log;
        }

        #region Bus
        [Authorize]
        [HttpPost("SaveBus")]
        public async Task<IActionResult> SaveBus(SaveBusRequest req)
        {
            try
            {
                var res = await repo.SaveBus(req, JwtHelper.Id(User));
                return StatusCode(res.Result.StatusCode, res);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ErrorHandler.GetInfoResponse(ex));
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    ErrorHandler.GetErrorResponse(ex));
            }
        }

        [Authorize]
        [HttpDelete("DeleteBus")]
        public async Task<IActionResult> DeleteBus(long Id)
        {
            try
            {
                var res = await repo.DeleteBus(Id, JwtHelper.Id(User));
                return StatusCode(res.Result.StatusCode, res);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ErrorHandler.GetInfoResponse(ex));
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    ErrorHandler.GetErrorResponse(ex));
            }
        }

        [Authorize]
        [HttpGet("GetBusList")]
        public async Task<IActionResult> GetBusList([FromQuery] GetBusListRequest req)
        {
            try
            {
                var res = await repo.GetBusList(req);
                return StatusCode(res.Result.StatusCode, res);
            }
            catch (AppException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ErrorHandler.GetInfoResponse(ex));
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    ErrorHandler.GetErrorResponse(ex));
            }
        }
        #endregion
    }
}
