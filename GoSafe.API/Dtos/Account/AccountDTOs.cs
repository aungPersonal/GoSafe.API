using GoSafe.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSafe.Dto.User
{
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
}
