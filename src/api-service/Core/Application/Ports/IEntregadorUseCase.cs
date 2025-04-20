using Application.DTOs;

namespace Application.Ports
{
    public interface IEntregadorUseCase
    {
        Task CadastrarEntregadorAsync(CriarNovoEntregador novoEntregador);
        Task AtualizarFotoCNHentregador(int id,AtualizaFotoCnh novaFotoCnh);
    }
}