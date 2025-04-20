using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs
{
    public record CriarNovoEntregador(string Nome, string CNPJ, DateTime DataNascimento, string NumeroCNH, ETipoCNHDto TipoCNH, string FotoCNH);

    public record AtualizaFotoCnh(string novaFoto);

    public enum ETipoCNHDto
    {
        A,
        B,
        AB
    }

    public static class EntregadorDtoAdapter
    {
        public static Entregador ConvertEntregadorDtoToEntity(this CriarNovoEntregador novoEntregador)
        {
            return new Entregador(
                novoEntregador.Nome,
                novoEntregador.CNPJ,
                novoEntregador.DataNascimento,
                novoEntregador.NumeroCNH,
                MapTipoCNH(novoEntregador.TipoCNH),
                novoEntregador.FotoCNH);
        }

        private static ETipoCNH MapTipoCNH(ETipoCNHDto tipoDto)
        {
            return tipoDto switch
            {
                ETipoCNHDto.A => ETipoCNH.A,
                ETipoCNHDto.B => ETipoCNH.B,
                ETipoCNHDto.AB => ETipoCNH.AB,
                _ => throw new ArgumentOutOfRangeException(nameof(tipoDto), $"Valor inesperado: {tipoDto}")
            };
        }
    }
}