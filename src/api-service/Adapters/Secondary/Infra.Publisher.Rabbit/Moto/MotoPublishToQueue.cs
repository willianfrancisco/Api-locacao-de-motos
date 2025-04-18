using System.Text;
using Domain.Ports;
using RabbitMQ.Client;

namespace Infra.Publisher.Rabbit.Moto
{
    public class MotoPublishToQueue : IMotoPublishToQueue
    {
        public async Task PublicaMenssagemParaFilaAsync(string mensagem)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue:"moto",
                durable:false,
                exclusive:false,
                autoDelete:false,
                arguments:null);

            var body = Encoding.UTF8.GetBytes(mensagem);

            await channel.BasicPublishAsync(exchange:"",routingKey:"moto",body:body);
        }
    }
}