using System.ComponentModel.DataAnnotations;

namespace VPSDemo.Api.Contracts.Task
{
    public class TaskCreateRequest
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; } = null!;

        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        [Range(0, 999)]
        public int EffortEstimation { get; set; }
    }
}
