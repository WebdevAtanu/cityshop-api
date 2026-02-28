using cityshop_api.Helpers;
using System.Net;
using System.Text.Json;

namespace cityshop_api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // This method is called for each HTTP request. It tries to execute the next middleware in the pipeline and catches any exceptions that occur.
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        // This method handles exceptions by creating a standardized error response using the ResponseHelper and writing it to the HTTP response.
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = ResponseHelper.ResponseFailed(
                ex.Message,
                HttpStatusCode.InternalServerError
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    }
}
