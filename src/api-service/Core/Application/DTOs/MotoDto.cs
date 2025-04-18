using Domain.Entities;

namespace Application.DTOs
{
    public record CriarNovaMotoDto(int Ano, string Modelo, string Placa);
    public record LerMotoDto(int Id, int Ano, string Modelo, string Placa);
    public record AtualizaPlacaMotoDto(string placa);

    public static class MotoDtoAdapter
    {
        public static LerMotoDto ConverterParaMotoDto(this Moto moto)
        {
            return new LerMotoDto(moto.Id, moto.Ano, moto.Modelo, moto.Placa);
        }

        public static List<LerMotoDto> ConverterParaListaMotoDto(this List<Moto> motos)
        {
            return motos.Select(moto => moto.ConverterParaMotoDto()).ToList();
        }
    }
}