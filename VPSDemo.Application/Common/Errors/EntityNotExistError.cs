using FluentResults;
using Microsoft.AspNetCore.Http;

namespace VPSDemo.Application.Common.Errors
{
    public class EntityNotExistError : Error
    {
        public EntityNotExistError(int id, string entityName)
        {
            WithMetadata(ConstKeys.StatusCode, StatusCodes.Status404NotFound);
            WithMetadata(ConstKeys.ErrorMessage, $"{entityName} with given identifier '{id}' does not exist");
        }

    }
}
