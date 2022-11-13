using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace VPSDemo.Api.Filter
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var traceId = Activity.Current?.Id ?? context.HttpContext?.TraceIdentifier;
                context.Result = new BadRequestObjectResult(new 
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "Validation Error",
                    status = StatusCodes.Status400BadRequest,
                    traceId = traceId,
                    errorMessage = context.ModelState.Values.SelectMany(
                            o => o.Errors.Select(
                                e => e.ErrorMessage))
                });
            }
        }
    }
}
