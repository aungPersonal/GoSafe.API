using GoSafe.Dto.Common;
using System.ComponentModel.DataAnnotations;

namespace GoSafe.Dto.User
{
    public class LoginResponse
    {
        public Response Result { get; set; } = new Response();
        /// <summary>
        /// token
        /// </summary>
        [Required]
        public TokenRes Token { get; set; } = null!;
    }
    public class TokenRes
    {
        /// <summary>
        /// access token
        /// </summary>
        [Required]
        public string AccessToken { get; set; } = null!;
        /// <summary>
        /// refresh token
        /// </summary>
        [Required]
        public string RefreshToken { get; set; } = null!;
        [Required]
        public DateTime ExpirationDateTime { get; set; }
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public long Id { get; set; }
    }
}
