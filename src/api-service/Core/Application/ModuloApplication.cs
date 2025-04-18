using Application.Ports;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ModuloApplication
    {
        public static IServiceCollection AdicionaDependenciaApplication(this IServiceCollection services)
        {
            services.AddScoped<IMotoUseCase,MotoUseCase>();
            return services;
        }
    }
}