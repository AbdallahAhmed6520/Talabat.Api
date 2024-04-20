
namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;

            Message = message ?? GetDafultMessageForStatusCode(statusCode);
        }

        private string? GetDafultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You Are Nor Authorized",
                404 => "Response Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
