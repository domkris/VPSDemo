using FluentResults;
using Microsoft.AspNetCore.Http;

namespace VPSDemo.Application.Common.Errors
{
    public class IdentifierError : Error
    {
        public IdentifierError(string name) 
        {
            WithMetadata(ConstKeys.StatusCode, StatusCodes.Status400BadRequest);
            WithMetadata(ConstKeys.ErrorMessage, $" {name} must be in range 0 - 99999");
        }
    }
}
