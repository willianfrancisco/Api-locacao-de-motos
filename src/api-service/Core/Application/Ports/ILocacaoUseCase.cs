using Application.DTOs;

namespace Application.Ports
{
    public interface ILocacaoUseCase
    {
        Task CadastraLocacaoAsync(CriaNovaLocacaoDto novaLocacaoDto);
        Task AtualizaDataDevolucaoAsync(int id, AtualizaLocacaoDto locacaoDto);
        Task<LerLocacaoDto> RecuperaLocacaoPorIdAsync(int id);
    }
}