using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.CustomMiddlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next , ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var problem = new ProblemDetails()
                    {
                        Title = "Error While Processing Http Request - End Point Not Found",
                        Status = StatusCodes.Status404NotFound,
                        Detail = $"EndPoint {httpContext.Request.Path} Not Found",
                        Instance = httpContext.Request.Path,
                    };
                    await httpContext.Response.WriteAsJsonAsync(problem);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problem = new ProblemDetails()
                {
                    Title = "An Unexpected Error Occured",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                };
                await httpContext.Response.WriteAsJsonAsync(problem);
               
            }
        }
    }
}
