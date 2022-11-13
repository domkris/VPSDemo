using Microsoft.AspNetCore.Mvc.Infrastructure;
using VPSDemo.Api.Common;
using VPSDemo.Api.Filter;

namespace VPSDemo.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<ProblemDetailsFactory, VPSProblemDetailsFactory>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
