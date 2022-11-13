using Microsoft.EntityFrameworkCore;
using VPSDemo.Application;

namespace VPSDemo.Infrastructure.Persistance.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly VPSDemoDbContext _dbContext;
        public TaskRepository(VPSDemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AssignSubTask(Domain.Entities.Task subTask, Domain.Entities.Task parentTask)
        {
            subTask.ParentTaskId = parentTask.Id;
            _dbContext.SaveChanges();
        }

        public Domain.Entities.Task Create(Domain.Entities.Task task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
            return task;
        }

        public void Delete(Domain.Entities.Task task)
        {
            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
        }

        public void Update(Domain.Entities.Task task)
        {
            _dbContext.Tasks.Update(task);
            _dbContext.SaveChanges();
        }


        public Domain.Entities.Task? GetById(int id)
        {
            return _dbContext.Tasks.Where(entity => entity.Id == id).Include(_ => _.SubTasks).FirstOrDefault();
        }

        public void GetTaskHierarchy(int id, ref int sum)
        {
            var childredEntities = _dbContext.Tasks.Where(entity => entity.ParentTaskId == id).ToList();

            foreach (var entity in childredEntities)
            {
                sum += entity.EffortEstimation;
                GetTaskHierarchy(entity.Id, ref sum);
            }
        }
    }
}
