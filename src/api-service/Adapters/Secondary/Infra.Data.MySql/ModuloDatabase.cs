using Domain.Ports;
using Infra.Data.MySql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Data.MySql
{
    public static class ModuloDatabase
    {
        public static IServiceCollection AdicionaDependenciaDatabases(this IServiceCollection services)
        {
            services.AddScoped<IMotoRepository, MotoRepository>();
            services.AddScoped<IEntregadorRepository,EntregadorRepository>();
            services.AddScoped<ILocacaoRepository, LocacaoRepository>();
            return services;
        }
    }
}