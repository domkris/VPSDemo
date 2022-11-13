using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VPSDomain.Domain.Common;

namespace VPSDemo.Domain.Entities
{
    public class Task : BaseEntity
    {
        [Range(0, 99999)]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Title { get; set; } = null!;

        [MaxLength(100)]
        public string? Description { get; set; }

        [Range(0, 999)]
        public int EffortEstimation { get; set; }

        [NotMapped]
        [Range(0, 999)]
        public int? AggregatedEffortEstimation { get; set; }

        private String status = null!;

        [MaxLength(11)]
        public string Status {
            get 
            { 
                return status; 
            }
            set 
            {
                if (!TaskStatusValues.ValuesDict.Any(x => x.Value == value))
                    throw new ArgumentException("Not valid value");
                status = value;
            }
        }
        public int? ParentTaskId { get; set; }

        [JsonIgnore]
        public virtual Task? ParentTask { get; set; }
        public ICollection<Task>? SubTasks { get; set; }       
    }

    /// <summary>
    ///  This class is used to define constants specific for Task Entity, used for this Demo purpose.
    ///  In real world scenario would be better to define TaskStatus entity in DB
    /// </summary>
    public static class TaskStatusValues
    {
        public static readonly string InProgressTask = "In Progress";
        public static readonly string NewTask = "New";
        public static readonly string OnHoldTask = "On hold";
        public static readonly string CompletedTask = "Completed";

        /// <summary>
        /// Static Dictionary for Task Status allowed values
        /// </summary>
        public static readonly Dictionary<int, string> ValuesDict = new()
            {
                {1, TaskStatusValues.InProgressTask},
                {2, TaskStatusValues.NewTask},
                {3, TaskStatusValues.OnHoldTask},
                {4, TaskStatusValues.CompletedTask}
            };
    }
}
