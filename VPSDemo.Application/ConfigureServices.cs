using Microsoft.Extensions.DependencyInjection;
using VPSDemo.Application.Services;

namespace VPSDemo.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}
