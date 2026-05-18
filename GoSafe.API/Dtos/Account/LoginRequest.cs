using System.ComponentModel.DataAnnotations;

namespace GoSafe.Dto.User
{
    public class LoginRequest
    {
        [Required]
        public string LoginName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
