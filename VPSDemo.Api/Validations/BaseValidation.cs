using FluentResults;
using VPSDemo.Application.Common.Errors;

namespace VPSDemo.Api.Validations
{
    public static class BaseValidation 
    {
        public static Result ValidateIdentifier(int identifier)
        {
            return Result.FailIf(identifier > 99999 || identifier < 0, new IdentifierError($"{nameof(identifier)}"));

        }
    }
}
