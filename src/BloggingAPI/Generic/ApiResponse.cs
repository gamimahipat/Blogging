namespace BloggingAPI
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Success = false;
            Message = null;
            Data = null;
        }

        public ApiResponse(bool success, string? message = null, object? data = null, long totalRecords = 0)
        {
            Success = success;
            Message = message;
            Data = data;
            TotalRecords = totalRecords;
        }

        public bool Success { get; set; } = false;
        public string? Message { get; set; } = null;
        public object? Data { get; set; } = null;
        public long TotalRecords { get; set; } = 0;


        // Static methods for creating responses
        public static ApiResponse SuccessResponse(object data, string message = "Success", long totalRecords = 0) =>
            new ApiResponse(true, message, data, totalRecords);

        public static ApiResponse ErrorResponse(string message = "Error") =>
            new ApiResponse(false, message, null, 0);
    }
}
