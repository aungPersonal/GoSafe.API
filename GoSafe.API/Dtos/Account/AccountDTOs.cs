using GoSafe.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSafe.Dto.User
{

    #region Sign Up
    public class SignUpResponse
    {
        public Response Result { get; set; } = new Response();
        public long Id { get; set; }
    }

    public class GenerateOTPRequest
    {
        public string EmailOrPhone { get; set; } = null!;
    }

    public class SignUpRequest
    {
        public string EmailOrPhone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }

    public class VerifySignUpRequest
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
    }

    public class ReSendOtpSignUpRequest
    {
        public long Id { get; set; }
        public string EmailOrPhoneNumber { get; set; } = null!;
    }
    #endregion


    #region User CRUD
    public class SaveUserRequest
    {
        public long Id { get; set; }

        public string LoginName { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? Phone { get; set; }

        public int? RoleId { get; set; }
        public string? Password { get; set; }
    }

    public class GetUserListRequest : PaginationRequest
    {
        public string? LoginName { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public int? RoleId { get; set; }
    }

    public class UserModel
    {
        public long Id { get; set; }

        public string LoginName { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? Phone { get; set; }

        public int? RoleId { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class GetUserListResponse
    {
        public Response Result { get; set; } = new Response();

        public List<UserModel> Items { get; set; } = new();

        public int TotalItem { get; set; }
    }
    #endregion
}
