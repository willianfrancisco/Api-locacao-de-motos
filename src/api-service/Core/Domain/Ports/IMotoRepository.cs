using Domain.Entities;

namespace Domain.Ports
{
    public interface IMotoRepository
    {
        Task<List<Moto>> ListarTodasMotosCadastradasAsync();
        Task<Moto> RecuperarMotoPelaPlacaAsync(string placa);
        Task<Moto> RecuperarMotoPorIdAsync(int id);
        Task AtualizaPlacaMotoAsync(int id,string placa);
        Task DeletarMotoAsync(int id);
    }
}