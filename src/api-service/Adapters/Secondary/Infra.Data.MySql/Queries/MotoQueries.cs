namespace Infra.Data.MySql.Queries
{
    public static class MotoQueries
    {
        public static string QuerySelecionatTodasAsMotos = "Select Id, Ano, Modelo, Placa From Motos";
        public static string QuerySelecionarMotoPeloId = "Select Id, Ano, Modelo, Placa From Motos Where Id = @Id";
        public static string QuerySelecionarMotoPelaPlaca = "Select Id, Ano, Modelo, Placa From Motos Where Placa = @Placa";
        public static string QueryAtualizaPlaca = "Update Motos Set Placa = @Placa Where Id = @Id";
        public static string QueryDeletarMoto = "Delete From Motos Where Id = @Id";
    }
}