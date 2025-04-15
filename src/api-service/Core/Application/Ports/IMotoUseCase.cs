using Application.DTOs;

namespace Application.Ports
{
    public interface IMotoUseCase
    {
        Task PublicaMenssagemParaFila(CriarNovaMotoDto criarNovaMotoDto);
    }
}