using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSafe.Dto.User
{
    public class SaveUserRequest
    {
        public long Id { get; set; }

        public string LoginName { get; set; } = null!;

        public string? Password { get; set; }

        public string FullName { get; set; } = null!;

        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }

        public bool? IsDeative { get; set; }
        public bool Adult { get; set; }
    }
}
