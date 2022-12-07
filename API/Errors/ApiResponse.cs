namespace API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }        
        public string Message {get; set; }    

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        private string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made.",
                401 => "Not Authorized, you are",
                404 => "Resource Not Found." ,
                500 => "Errors are path to darkside. Errors lead to anger. Anger leads to hate. Hate leads to career change.",
                _ => "Invalid status Code"
            };
        }
    }
}