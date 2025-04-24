using Domain.Entities;

namespace Domain.Ports
{
    public interface IEntregadorRepository
    {
        Task CadastrarEntregadorAsync(Entregador entregador);
        Task<Entregador> RecuperaEntregadorPeloCNPJAsync(string cnpj);
        Task<Entregador> RecuperaEntregadorPelaCNHAsync(string cnh);
        Task<Entregador> RecuperaEntregadorPeloIdAsync(int id);
        void AtualizaFotoCNHEntregadorAsync(string cnpj, string fotoNova);
    }
}