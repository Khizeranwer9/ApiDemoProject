using System.Linq.Expressions;
using System.Net;
using System.Text.Json;
using ApiDemo.Controllers;
using ApiDemo.Exceptions;
namespace ApiDemo.Middleware
{
    public class ExceptionMiddleware
    {
       private readonly RequestDelegate _next;
       private readonly ILogger<UserController> _logger;

       public ExceptionMiddleware(RequestDelegate next, ILogger<UserController> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context) 
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync (HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string StatusMessage = "Something went wrong";

            if(ex is AppException appEx)
            {
                statusCode=appEx.StatusCode;
                StatusMessage = appEx.Message;
            }
            else if(ex is BadRequestException badRequestEx)
            {
                statusCode=badRequestEx.StatusCode;
            }

            context.Response.StatusCode = statusCode;

            var response = new
            {
                success = false,
                message = StatusMessage,
                statusCode = statusCode
            };

            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);

        }

       


    }
}
