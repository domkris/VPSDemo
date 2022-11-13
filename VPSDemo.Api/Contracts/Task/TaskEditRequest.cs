using System.ComponentModel.DataAnnotations;
using VPSDemo.Api.Contracts.Task.DataAnnotations;
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


        [ValidStatus]
        [MaxLength(11)]
        public string Status { get; set; } = null!; 
    }
}
