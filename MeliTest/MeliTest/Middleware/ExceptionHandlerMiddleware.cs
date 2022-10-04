using MeliTest.Errors;
using MeliTest.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MeliTest.Middleware
{
    /// <summary>
    /// Middleware for exception handling
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ObjectResult> _executor;
        private static readonly ActionDescriptor _emptyActionDescriptor = new ActionDescriptor();

        public ExceptionHandlerMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor)
        {
            _next = next;
            _executor = executor;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                var routeData = context.GetRouteData() ?? new RouteData();
                var actionContext = new ActionContext(context, routeData, _emptyActionDescriptor);

                var result = CreateResult(ex, context);
                
                await _executor.ExecuteAsync(actionContext, result);
            }
        }

        private ObjectResult CreateResult(Exception exception, HttpContext context)
        {
            var errorCode = exception.Data["ErrorCode"]?.ToString();
            var errorMessage = $"{exception.Data["ErrorMessage"]?.ToString()}";

            var result = new ErrorActionResult(new ErrorResultModel(errorMessage, errorCode));
            switch (exception)
            {
                case DataValidationException _:
                    result.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    break;

                case NotFoundException _:
                    result.StatusCode = StatusCodes.Status404NotFound;
                    break;

                default:
                    result.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            return result;
        }
    }
}
