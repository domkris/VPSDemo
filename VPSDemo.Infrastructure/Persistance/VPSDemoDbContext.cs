using Microsoft.EntityFrameworkCore;
using VPSDemo.Application.Persistence.Interfaces;
using VPSDomain.Domain.Common;
using Task = VPSDemo.Domain.Entities.Task;

namespace VPSDemo.Infrastructure.Persistance
{
    public  class VPSDemoDbContext : DbContext, IVPSDemoDbContext
    {
        public VPSDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Task> Tasks => Set<Task>();

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreationDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
