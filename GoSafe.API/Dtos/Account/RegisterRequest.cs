using System.ComponentModel.DataAnnotations;

namespace GoSafe.Dto.User
{
    public class RegisterRequest
    {
        [Required]
        public string LoginName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;

    }
}
