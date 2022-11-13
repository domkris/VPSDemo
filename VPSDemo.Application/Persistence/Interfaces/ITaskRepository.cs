namespace VPSDemo.Application
{
    public interface ITaskRepository
    {
        public Domain.Entities.Task Create(Domain.Entities.Task entity);
        public void Update(Domain.Entities.Task entity);
        public Domain.Entities.Task? GetById(int id);
        public void GetTaskHierarchy(int id, ref int sum);
        public void Delete(Domain.Entities.Task entity);
        public void AssignSubTask(Domain.Entities.Task subEntity, Domain.Entities.Task parentEntity);
    }
}
