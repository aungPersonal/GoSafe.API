using System.ComponentModel.DataAnnotations;

namespace GoSafe.Dto.User
{
    public class RefreshTokenRequest
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
