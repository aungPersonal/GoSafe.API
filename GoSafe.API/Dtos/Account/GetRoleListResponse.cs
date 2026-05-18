using GoSafe.API.Dtos.Model;
using GoSafe.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSafe.Dto.User
{
    public class GetRoleListResponse
    {
        public Response Result { get; set; } = new Response();
        public List<RoleType> Items { get; set; } = new();
    }
}
