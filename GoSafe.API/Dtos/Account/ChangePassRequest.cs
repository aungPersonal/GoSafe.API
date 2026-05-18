using System.ComponentModel.DataAnnotations;

namespace GoSafe.Dto.User
{
    public class ChangePassRequest
    {
        [Required]
        public string OldPassword { get; set; } = null!;
        [Required]
        public string NewPassword { get; set; } = null!;
    }
}
