using FluentResults;
using Microsoft.AspNetCore.Mvc;
using VPSDemo.Api.Filter;
using VPSDemo.Application.Common;

namespace VPSDemo.Api.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class BaseController : ControllerBase
    {
        protected IActionResult ProblemHandler(List<IError> errors)
        {
            var firstError = errors[0];
            var statusCode = firstError.Metadata.GetValueOrDefault(ConstKeys.StatusCode);
            var errorMessage = firstError.Metadata.GetValueOrDefault(ConstKeys.ErrorMessage);
            HttpContext.Items[ConstKeys.ErrorMessage] = new string[] { $"{errorMessage}" };
            return Problem(statusCode: (int)statusCode);
        }

    }
}
