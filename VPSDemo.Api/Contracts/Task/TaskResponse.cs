using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VPSDemo.Api.Contracts.Task
{
    public class TaskResponse : BaseResponse
    {
        public int Identifier { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EffortEstimation { get; set; }
        public int AggregatedEffortEstimation { get; set; }
        public string Status { get; set; } = string.Empty;
        public ICollection<TaskResponse>? SubTasks { get; set; }
    }
}
