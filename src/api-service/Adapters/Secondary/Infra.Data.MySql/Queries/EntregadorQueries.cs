namespace Infra.Data.MySql.Queries
{
    public static class EntregadorQueries
    {
        public static string QueryInserirNovoEntregado = "Insert Into Entregadores(Nome, CNPJ, DataNascimento, NumeroCNH, TipoCNH) Values(@Nome, @CNPJ, @DataNascimento, @NumeroCNH, @TipoCNH)";
        public static string QuerySelectEntregadorPeloCnpj = "Select Nome, CNPJ, DataNascimento, NumeroCNH, TipoCNH From Entregadores Where CNPJ = @CNPJ";
        public static string QuerySelectEntregadorPelaCNH = "Select Nome, CNPJ, DataNascimento, NumeroCNH, TipoCNH From Entregadores Where NumeroCNH = @NumeroCNH";
        public static string QuerySelectEntregadorPeloId = "Select Nome, CNPJ, DataNascimento, NumeroCNH, TipoCNH From Entregadores Where Id = @Id";
    }
}