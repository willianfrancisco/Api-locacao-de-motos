namespace Domain.Entities
{
    public class Locacao
    {
        public Locacao()
        {
            
        }
        public Locacao(int entregadorId, int motoId, DateTime dataInicio, int plano)
        {
            EntregadorId = entregadorId;
            MotoId = motoId;
            DataInicio = dataInicio;
            Plano = plano;
        }

        public Locacao(int id, int entregadorId, int motoId, DateTime dataInicio, int plano)
        {
            Id = id;
            EntregadorId = entregadorId;
            MotoId = motoId;
            DataInicio = dataInicio;
            Plano = plano;
        }

        public Locacao(int id, int entregadorId, int motoId, DateTime dataInicio, DateTime dataTermino, DateTime dataDevolucao, int plano)
        {
            Id = id;
            EntregadorId = entregadorId;
            MotoId = motoId;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
            DataDevolucao = dataDevolucao;
            Plano = plano;
        }

        public int Id { get; private set; }
        public int EntregadorId { get; private set; }
        public int MotoId { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataTermino { get; private set; }
        public DateTime DataDevolucao { get; private set; }
        public int Plano { get; private set; }
        public decimal ValorDiaria => MapValorDiaria(Plano);
        public decimal ValorTotalLocacao => CalculaValorTotalLocacao();

        private decimal MapValorDiaria(int plano)
        {
            return plano switch
            {
                7 => 30.00m,
                15 => 28.00m,
                30 => 22.00m,
                45 => 20.00m,
                50 => 18.00m,
                _ => throw new ArgumentException(nameof(plano), $"Plano desconhecido: {plano}")
            };
        }

        private decimal CalculaValorTotalLocacao()
        {
            decimal valorTotal = 0m;

            if (DataDevolucao < DataTermino)
            {
                var percentualMulta = Plano == 7 ? 0.20m : Plano == 15 ? 0.40m : 1;
                var valorMulta = ValorDiaria * Plano * percentualMulta;
                valorTotal = (ValorDiaria * Plano) + valorMulta;
            }
            else if (DataDevolucao > DataTermino)
            {
                var diasEmAtraso = (DataDevolucao - DataTermino).Days;
                var valorMulta = diasEmAtraso * 50;
                valorTotal = (ValorDiaria * Plano) + valorMulta;
            }
            else
            {
                valorTotal = ValorDiaria * Plano;
            }

            return valorTotal;
        }
    }
}