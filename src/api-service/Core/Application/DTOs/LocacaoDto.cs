using Domain.Entities;

namespace Application.DTOs
{
    public record LerLocacaoDto(int Id, int EntregadorId, int MotoId, DateTime DataInicio, int plano, decimal ValorDiaria, decimal ValorTotalLocacao);
    public record CriaNovaLocacaoDto(int EntregadorId, int MotoId, DateTime DataInicio, int plano);
    public record AtualizaLocacaoDto(DateTime NovaDataTermino);

    public static class LocacaoDtoAdapter
    {
        public static LerLocacaoDto ConverterParaLocacaoDto(this Locacao locacao)
        {
            if (locacao == null)
                return null;

            return new LerLocacaoDto(locacao.Id, locacao.EntregadorId, locacao.MotoId, locacao.DataInicio, locacao.Plano, locacao.ValorDiaria, locacao.ValorTotalLocacao);
        }

        public static Locacao ConverterParaLocacaoEntity(this CriaNovaLocacaoDto novaLocacaoDto)
        {
            return new Locacao(novaLocacaoDto.EntregadorId, novaLocacaoDto.MotoId, novaLocacaoDto.DataInicio, novaLocacaoDto.plano);
        }
    }
}