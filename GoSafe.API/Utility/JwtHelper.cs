using GoSafe.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GoSafe.Utility
{
    public static class JwtHelper
    {
        public static long Id(IEnumerable<Claim> claims)
        {
            var userIdClaim = claims.FirstOrDefault(c => c.Type == "NameIdentifier");
            return Convert.ToInt64(userIdClaim);
        }

        public static long Id(ClaimsPrincipal user)
        {
            return Convert.ToInt64(user.Identity!.Name);
        }

        public static int RoleId(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            return Convert.ToInt32(userIdClaim!.Value);
        }

        public static bool Adult(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.StreetAddress);
            return Convert.ToBoolean(userIdClaim!.Value);
        }

        public static bool ValidToSeeVideo(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Dsa);
            return Convert.ToBoolean(userIdClaim!.Value);
        }
       
    }
}
