using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPSDemo.Application.Common;

namespace VPSDemo.Api.Controllers
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            HttpContext.Items[ConstKeys.ErrorMessage] = new string[] { $"{exception?.Message}" };
            return Problem(title: exception?.Message);
        }
    }
}
