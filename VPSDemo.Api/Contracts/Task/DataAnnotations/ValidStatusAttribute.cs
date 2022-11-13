using System.ComponentModel.DataAnnotations;
using VPSDemo.Domain.Entities;

namespace VPSDemo.Api.Contracts.Task.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidStatusAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (!TaskStatusValues.ValuesDict.Any(x => x.Value == value))
                return false;

            return true;
        }
    }
}
