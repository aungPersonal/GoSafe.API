using System.Security.Principal;

namespace GoSafe.API.Models
{
    public class TblToken : EntityCommon
    {
        public string Id { get; set; } = null!;

        public long AccountId { get; set; }

        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public DateTime ExpirationTime { get; set; }
    }
}
