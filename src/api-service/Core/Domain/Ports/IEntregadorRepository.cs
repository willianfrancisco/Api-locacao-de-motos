using Domain.Entities;

namespace Domain.Ports
{
    public interface IEntregadorRepository
    {
        Task CadastrarEntregadorAsync(Entregador entregador);
        Task<Entregador> RecuperaEntregadorPeloCNPJ(string cnpj);
        Task<Entregador> RecuperaEntregadorPelaCNH(string cnh);
        Task<Entregador> RecuperaEntregadorPeloId(int id);
        void AtualizaFotoCNHEntregador(string cnpj, string fotoNova);
    }
}