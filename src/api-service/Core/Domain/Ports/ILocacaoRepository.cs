using Domain.Entities;

namespace Domain.Ports
{
    public interface ILocacaoRepository
    {
        Task<Locacao> RecuperaLocacaoPorIdAsync(int id);
        Task CadastrarLocacaoAsync(Locacao locacao);
        Task AtualizaDataDevolucaoAsync(int id,DateTime novaDataDevolucao);
        Task<Locacao> RecuperaLocacaoPorMotoIdAsync(int motoId);
    }
}