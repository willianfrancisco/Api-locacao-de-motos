using System.Text;
using Dapper;
using MySqlConnector;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using worker_consumer_queue_rabbitmq.Entities;
using worker_consumer_queue_rabbitmq.Queries;

namespace worker_consumer_queue_rabbitmq;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private IConnection _connection;
    private bool _conectado = false;
    private readonly string _connectionString;

    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connectionString = configuration.GetConnectionString("WorkerConnectionString") ?? "";
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(15000);
            _logger.LogInformation("Executando worker.");
            await ConsomeMensagensFila();
        }
    }

    async Task ConsomeMensagensFila()
    {

        if (!_conectado)
            await ConectarRabbitMq();

        await using var channel = await _connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "moto",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = Encoding.UTF8.GetString(ea.Body.ToArray());
            var mensagem = JsonConvert.DeserializeObject<Moto>(body);
            if (mensagem != null)
                await GravarNovaMotoNoBanco(mensagem);
        };

        await channel.BasicConsumeAsync("moto", autoAck: true, consumer: consumer);
    }

    async Task GravarNovaMotoNoBanco(Moto moto)
    {
        try
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            var query = MotoQueries.QueryInserirNovaMoto;
            await connection.ExecuteAsync(query, moto);
        }
        catch (MySqlException ex)
        {
            _logger.LogError($"Erro ao tentar conectar no mysql:{ex.Message}");
        }
        catch(InvalidOperationException ex)
        {
            _logger.LogError($"Erro ao executar query:{ex.Message}");
        }
        catch(Exception ex)
        {
            _logger.LogError($"Ocorreu um erro:{ex.Message}");
        }

    }

    async Task ConectarRabbitMq()
    {
        const int maximoDeTentativas = 10;
        var tentativa = 0;

        while (tentativa <= maximoDeTentativas && !_conectado)
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
                _connection = await factory.CreateConnectionAsync();
                _conectado = true;
                _logger.LogInformation("Conexão com rabbit iniciada com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Não foi possivel conectar no rabbit:{ex.Message}");
                tentativa++;
                await Task.Delay(20000);
            }

        }
    }
}
