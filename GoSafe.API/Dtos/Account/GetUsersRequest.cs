using GoSafe.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSafe.Dto.User
{
    public class GetUsersRequest
    {
        public string? Filter { get; set; }
        public bool? FilterStatus { get; set; }
        public int FilterRole { get; set; }
        public OurTableState? TableState { get; set; }
        public bool IsForDropdown { get; set; }
    }
}
