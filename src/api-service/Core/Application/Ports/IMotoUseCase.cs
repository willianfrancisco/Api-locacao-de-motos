using Application.DTOs;

namespace Application.Ports
{
    public interface IMotoUseCase
    {
        Task PublicaMenssagemParaFilaAsync(CriarNovaMotoDto criarNovaMotoDto);
        Task<List<LerMotoDto>> RecuperarTodasMotosAsync();
        Task<LerMotoDto> RecuperarMotoPeloIdAsync(int id);
        Task<LerMotoDto> RecuperarMotoPelaPlacaAsync(string placa);
        Task AtualizaPlacaMotoAsync(int id,AtualizaPlacaMotoDto atualizaPlacaMotoDto);
        Task DeletarMotoAsync(int id);
    }
}