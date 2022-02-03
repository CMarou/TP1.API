using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TP1.API.Exceptions;

namespace TP1.API.Filters
{
    public class HttpExceptionActionFilter : IActionFilter, IOrderedFilter
    {
        private readonly ILogger<HttpExceptionActionFilter> _logger;

        public int Order => int.MaxValue;

        public HttpExceptionActionFilter(ILogger<HttpExceptionActionFilter> logger)
        {
            _logger = logger;
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not null)
            {
                if (context.Exception is HttpException exception)
                {
                    context.Result = new ObjectResult(exception.Errors)
                    {
                        StatusCode = exception.StatusCode
                    };
                    _logger.LogError(exception, "Failed to handle request: {Message}", exception.Errors);
                }
                else
                {
                    var problemDetails = new ProblemDetails
                    {
                        Title = "Internal Server Error",
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = context.Exception.Message
                    };

                    context.Result = new ObjectResult(problemDetails)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    _logger.LogError(context.Exception, "Internal server error: {Message}", context.Exception.Message);
                }

                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
