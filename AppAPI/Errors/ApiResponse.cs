namespace AppAPI.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            // if message is null then execute right handside
            Message = message ?? GetMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        
        private string GetMessageForStatusCode(int statusCode)
        {
            return statusCode switch 
            {
                400 => "You have made a bade request",
                401 => "Authorizaton Error",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}