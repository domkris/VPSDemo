using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VPSDemo.Application;
using VPSDemo.Application.Persistence.Interfaces;
using VPSDemo.Infrastructure.Persistance;
using VPSDemo.Infrastructure.Persistance.Repositories;

namespace VPSDemo.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<VPSDemoDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                   builder => builder.MigrationsAssembly(typeof(VPSDemoDbContext).Assembly.FullName)));

            services.AddScoped(
                provider => (IVPSDemoDbContext)provider.GetRequiredService<VPSDemoDbContext>()
                );

            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}
