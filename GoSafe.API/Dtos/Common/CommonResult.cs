using GoSafe.Dto.Common;

namespace GoSafe.Dto.Common
{
    public class CommonResult
    {
        public Response Result { get; set; } = new Response();
        public long Id { get; set; }
    }
}
