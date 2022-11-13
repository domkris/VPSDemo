using FluentResults;
using VPSDemo.Application.Common.Errors;
using VPSDemo.Domain.Entities;

namespace VPSDemo.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly string EntityName = typeof(Domain.Entities.Task).Name;
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }
        public Result<Domain.Entities.Task> AssignSubTask(int id, int parentId)
        {
            if (_repository.GetById(id) is not Domain.Entities.Task subTask)
                return Result.Fail<Domain.Entities.Task>(
                    new EntityNotExistError(id, EntityName));

            if (_repository.GetById(parentId) is not Domain.Entities.Task parentTask)
                return Result.Fail<Domain.Entities.Task>(
                   new EntityNotExistError(parentId, EntityName));

            _repository.AssignSubTask(subTask, parentTask);
            return parentTask;
        }

        public Result<Domain.Entities.Task> Create(string title, string? description, int effortEstimation)
        {
            var task = new Domain.Entities.Task
            {
                Title = title,
                Description = description,
                EffortEstimation = effortEstimation,
                Status = TaskStatusValues.NewTask
            };
            _repository.Create(task);
            return task;
        }

        public Result Delete(int id)
        {
            if (_repository.GetById(id) is not Domain.Entities.Task task)
                return Result.Fail(new EntityNotExistError(id, EntityName));

            if (task.SubTasks?.Count > 0)
            {
                int[] subTasksIds = task.SubTasks.Select((t) => t.Id).ToArray();
                return Result.Fail(
                   new EntityHasSubEntitiesError(id, EntityName, EntityName, subTasksIds));
            }

            _repository.Delete(task);
            return Result.Ok();
        }

        public Result<Domain.Entities.Task> Edit(int id, string? title, string? description, int? effortEstimation, string? status)
        {
            if (_repository.GetById(id) is not Domain.Entities.Task task)
                return Result.Fail<Domain.Entities.Task>(
                   new EntityNotExistError(id, EntityName));

            task.Title = title ?? task.Title;
            task.Description = description ?? task.Description;
            task.EffortEstimation = (int)(effortEstimation ?? task.EffortEstimation);

            if (status != null && IsChangeStatusAllowed(task, status))
                task.Status = status;

            _repository.Update(task);
            return task;
        }

        public Result<Domain.Entities.Task> Get(int id)
        {
            if (_repository.GetById(id) is not Domain.Entities.Task task)
                return Result.Fail<Domain.Entities.Task>(
                   new EntityNotExistError(id, EntityName));

            int aggregatedEffortEstimation = task.EffortEstimation;
            foreach (var subTask in task.SubTasks)
            {
                aggregatedEffortEstimation += subTask.EffortEstimation;
                _repository.GetTaskHierarchy(subTask.Id, ref aggregatedEffortEstimation);
            }
            task.AggregatedEffortEstimation = aggregatedEffortEstimation;

            return task;
        }

        private bool IsChangeStatusAllowed(Domain.Entities.Task entity, string status)
        {
            if (entity.Status == TaskStatusValues.NewTask
                && status == TaskStatusValues.InProgressTask)
                return true;

            if (entity.Status == TaskStatusValues.InProgressTask
                && status == TaskStatusValues.CompletedTask)
                return true;

            if (entity.Status == TaskStatusValues.InProgressTask
                && status == TaskStatusValues.OnHoldTask)
                return true;

            if (entity.Status == TaskStatusValues.OnHoldTask
                && status == TaskStatusValues.InProgressTask)
                return true;

            throw new Exception($"Not allowed to change Task status from '{entity.Status}' to '{status}'");

        }
    }
}
