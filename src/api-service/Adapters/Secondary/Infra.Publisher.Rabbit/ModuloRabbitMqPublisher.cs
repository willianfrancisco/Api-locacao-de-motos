using Domain.Ports;
using Infra.Publisher.Rabbit.Moto;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Publisher.Rabbit
{
    public static class RabbitMqPublisherModule
    {
        public static IServiceCollection AdicionaDependenciasInfraRabbitMq(this IServiceCollection services)
        {
            services.AddScoped<IMotoPublishToQueue, MotoPublishToQueue>();
            return services;
        }
    }
}