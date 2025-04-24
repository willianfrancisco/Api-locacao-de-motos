namespace Infra.Data.MySql.Queries
{
    public static class LocacaoQueries
    {
        public static string QueryInserirLocacao = "Insert Into Locacoes(EntregadorId, MotoId, DataInicio, DataTermino, DataDevolucao, Plano, ValorDiaria) Values (@EntregadorId, @MotoId, @DataInicio, @DataTermino, @DataDevolucao, @Plano, @ValorDiaria)";

        public static string QuerySelectLocacaoPeloId = "Select Id, EntregadorId, MotoId, DataInicio, DataTermino, DataDevolucao, Plano From Locacoes Where Id = @Id";

        public static string QueryAtualizaDataTerminoLocacao = "Update Locacoes Set DataDevolucao = @DataDevolucao Where Id = @Id";
    }
}