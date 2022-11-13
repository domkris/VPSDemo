using System.ComponentModel.DataAnnotations;
using VPSDemo.Domain.Entities;

namespace VPSDemo.Api.Contracts.Task
{
    public class TaskEditRequest
    {
        [MaxLength(30)]
        public string? Title { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        [Range(0, 999)]
        public int? EffortEstimation { get; set; }

        private String? status { get; set; }

        [MaxLength(11)]
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (!TaskStatusValues.ValuesDict.Any(x => x.Value == value))
                    throw new ArgumentException($" Task status value '{value}' is not a valid value");
                status = value;
            }
        }
    }
}
