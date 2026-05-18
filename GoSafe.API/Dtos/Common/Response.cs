using Microsoft.AspNetCore.Http;

namespace GoSafe.Dto.Common
{
    public class Response
    {
        /// <summary>
        /// Status code
        /// 200,400
        /// </summary>
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        /// <summary>
        /// Error messages
        /// </summary>
        //public Dictionary<string, string>? ErrorMessages { get { return errorMessages; }}

        public string ErrorMessage { get; set; }

        public void AddErrorMessage(string message)
        {
            ErrorMessage = message;

        }
    }
}
