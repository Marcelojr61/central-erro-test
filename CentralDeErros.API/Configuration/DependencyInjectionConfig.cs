using CentralDeErros.Services;
using CentralDeErros.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CentralDeErros.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
        {

            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IEnvironmentService, EnvironmentService>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IMicrosserviceService, MicrosserviceService>();
            services.AddScoped<ITokenGeneratorService,TokenGeneratorService>();


            return services;

        }
    }
}
