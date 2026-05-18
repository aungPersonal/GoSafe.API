using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace GoSafe.API.Utility
{
    public class JwtTokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtTokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                try
                {
                    var jwtToken = handler.ReadJwtToken(token);
                    var expClaim = jwtToken.Payload.Exp;

                    if (expClaim.HasValue)
                    {
                        DateTime utcExpiryTime = DateTimeOffset.FromUnixTimeSeconds(expClaim.Value).UtcDateTime;

                        if (utcExpiryTime < DateTime.UtcNow)
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            //await context.Response.WriteAsync("{\"error\": \"Token has expired\", \"expiredAtUtc\": \"" + utcExpiryTime.ToString("yyyy-MM-dd HH:mm:ss") + "\"}");
                            var response = new
                            {
                                result = new
                                {
                                    statusCode = 401,
                                    errorMessages = new Dictionary<string, string>
                                    {
                                        { "0", "Token has expired." }
                                    }
                                },
                                id = 0
                            };

                            var json = JsonSerializer.Serialize(response);
                            await context.Response.WriteAsync(json);
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                    context.Response.StatusCode = 401;
                    //await context.Response.WriteAsync("{\"error\": \"Invalid token\"}");
                    var response = new
                    {
                        result = new
                        {
                            statusCode = 401,
                            errorMessages = new Dictionary<string, string>
                                    {
                                        { "0", "Invalid token." }
                                    }
                        },
                        id = 0
                    };

                    var json = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(json);
                    return;
                }
            }

            await _next(context); // Pass request to the next middleware if token is valid
        }
    }
}
