namespace SLY.Portal.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }

        public ApiResponse(T data, string? SucessMessage = null)
        {
            Success = true;
            Data = data;
            Message = SucessMessage;
            Error = null;
        }

        public ApiResponse(string errorCode, string errorMessage)
        {
            Success = false;
            Data = default;
            Message = null;
            Error = new ApiError
            {
                Code = errorCode,
                Message = errorMessage
            };
        }

        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public ApiError? Error { get; set; }
    }

    public class ApiError
    {
        public string Code { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
