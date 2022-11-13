using FluentResults;
using Microsoft.AspNetCore.Http;

namespace VPSDemo.Application.Common.Errors
{
    public class EntityHasSubEntitiesError: Error
    {
        public EntityHasSubEntitiesError(int id, string entityName, string subEntityName, int[] subIds)
        {
            WithMetadata(ConstKeys.StatusCode, StatusCodes.Status409Conflict);
            WithMetadata(ConstKeys.ErrorMessage, $"{entityName} with identifier '{id}' has assigned one or more {subEntityName} with identifiers: [{String.Join(", ", subIds)}]");
        }
    }
}
