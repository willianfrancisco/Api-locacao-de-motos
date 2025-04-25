
using Domain.Ports;
using Infra.Logger.serilog;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infra.Logger
{
    public static class ModuloLogger
    {
        public static IServiceCollection AdicionaDependenciaLogger(this IServiceCollection services)
        {
            services.AddSingleton<Serilog.ILogger>(Log.Logger);
            services.AddSingleton<ISerilogLogger, SerilogLogger>();
            return services;
        }
    }
}