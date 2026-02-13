using System.Net;

namespace cityshop_api.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }
    }

    public static class ResponseHelper
    {
        // Success response with data
        public static ApiResponse<T> Success<T>(
            T data,
            string message = "Request successful",
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = (int)statusCode
            };
        }

        // Success response without data
        public static ApiResponse<object> Success(
            string message = "Request successful",
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse<object>
            {
                Success = true,
                Message = message,
                Data = null,
                StatusCode = (int)statusCode
            };
        }

        // Failed response
        public static ApiResponse<object> Fail(
            string message,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse<object>
            {
                Success = false,
                Message = message,
                Data = null,
                StatusCode = (int)statusCode
            };
        }
    }
}
