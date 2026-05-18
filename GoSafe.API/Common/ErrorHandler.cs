using Microsoft.AspNetCore.Mvc;
using GoSafe.Dto.Common;
using GoSafe.API.Common;

namespace GoSafe.Common
{
    public static class ErrorHandler
    {
        public static object? GetResponse(string message)
        {
            var res = new CommonResult()
            {
                Result = new Response()
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            };
            res.Result.AddErrorMessage(message);
            return res;
        }

        public static object? GetErrorResponse(Exception ex)
        {
            var res = new CommonResult()
            {
                Result = new Response()
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            };
            if (CommonConstants.EnableDevMode)
            {
                res.Result.AddErrorMessage(ex.Message);
            }
            else
            {
                res.Result.AddErrorMessage(CommonConstants.ProductionErrorMessage);
            }
            return res;
        }

        public static object? GetInfoResponse(Exception ex)
        {
            var res = new CommonResult()
            {
                Result = new Response()
                {
                    StatusCode = StatusCodes.Status400BadRequest
                }
            };
            res.Result.AddErrorMessage(ex.Message);
            return res;
        }
    }
}
