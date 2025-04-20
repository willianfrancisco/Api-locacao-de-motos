using Domain.Enums;

namespace Domain.Entities
{
    public class Entregador
    {
        public Entregador(string? nome, string? cNPJ, DateTime dataNascimento, string? numeroCNH, ETipoCNH tipoCNH)
        {
            Nome = nome;
            CNPJ = cNPJ;
            DataNascimento = dataNascimento;
            NumeroCNH = numeroCNH;
            TipoCNH = tipoCNH;
        }

        public Entregador(string? nome, string? cNPJ, DateTime dataNascimento, string? numeroCNH, ETipoCNH tipoCNH, string? fotoCNH)
        {
            Nome = nome;
            CNPJ = cNPJ;
            DataNascimento = dataNascimento;
            NumeroCNH = numeroCNH;
            TipoCNH = tipoCNH;
            FotoCNH = fotoCNH;
        }


        public Entregador(string? cNPJ, string? fotoCNH)
        {
            CNPJ = cNPJ;
            FotoCNH = fotoCNH;
        }

        public Entregador(int id, string? nome, string? cNPJ, DateTime dataNascimento, string? numeroCNH, ETipoCNH tipoCNH, string? fotoCNH)
        {
            Id = id;
            Nome = nome;
            CNPJ = cNPJ;
            DataNascimento = dataNascimento;
            NumeroCNH = numeroCNH;
            TipoCNH = tipoCNH;
            FotoCNH = fotoCNH;
        }

        public int Id { get; private set; }
        public string? Nome { get; private set; }
        public string? CNPJ { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string? NumeroCNH { get; private set; }
        public ETipoCNH TipoCNH { get; private set; }
        public string? FotoCNH { get; private set; }
    }

    public class DadosEntregador
    {
        public DadosEntregador(string? cNPJ, string? fotoCNH)
        {
            CNPJ = cNPJ;
            FotoCNH = fotoCNH;
        }

        public string? CNPJ { get; private set; }
        public string? FotoCNH { get; set; }
    }
}