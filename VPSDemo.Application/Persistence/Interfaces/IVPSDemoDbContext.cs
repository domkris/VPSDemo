using Microsoft.EntityFrameworkCore;

namespace VPSDemo.Application.Persistence.Interfaces
{
    public interface IVPSDemoDbContext
    {
        public DbSet<Domain.Entities.Task> Tasks { get; }
    }
}
