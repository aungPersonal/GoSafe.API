using GoSafe.Dto.Common;
using System.ComponentModel.DataAnnotations;

namespace GoSafe.Dto.User
{
    public class RegisterResponse
    {
        public Response Result { get; set; } = new Response();
        /// <summary>
        /// token
        /// </summary>
        [Required]
        public TokenRes Token { get; set; } = null!;
    }
}
