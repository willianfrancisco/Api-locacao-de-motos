namespace worker_consumer_queue_rabbitmq.Queries
{
    public static class MotoQueries
    {
        public static string QueryInserirNovaMoto = "Insert Into Motos (Ano, Modelo, Placa) Values (@Ano, @Modelo, @Placa)";
    }
}