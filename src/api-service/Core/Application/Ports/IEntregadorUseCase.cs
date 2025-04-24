using Application.DTOs;

namespace Application.Ports
{
    public interface IEntregadorUseCase
    {
        Task CadastrarEntregadorAsync(CriarNovoEntregador novoEntregador);
        Task AtualizarFotoCNHentregadorAsync(int id,AtualizaFotoCnh novaFotoCnh);
    }
}