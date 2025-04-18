using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infra.Data.MySql.Queries;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infra.Data.MySql.Repositories
{
    public class MotoRepository(
        IConfiguration _configuration
        ) : IMotoRepository
    {
        private readonly string _connectionString = _configuration.GetConnectionString("ApiConnectionString") ?? "";

        public async Task AtualizaPlacaMotoAsync(int id, string placa)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            var query = MotoQueries.QueryAtualizaPlaca;
            await connection.ExecuteAsync(query, new { Id = id, Placa = placa });
        }

        public async Task DeletarMotoAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            var query = MotoQueries.QueryDeletarMoto;
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<List<Moto>> ListarTodasMotosCadastradasAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            var query = MotoQueries.QuerySelecionatTodasAsMotos;
            var motos = await connection.QueryAsync<Moto>(query);
            return motos.ToList();
        }

        public async Task<Moto> RecuperarMotoPelaPlacaAsync(string placa)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            var query = MotoQueries.QuerySelecionarMotoPelaPlaca;
            var moto = await connection.QueryFirstOrDefaultAsync<Moto>(query, new { Placa = placa });
            return moto;
        }

        public async Task<Moto> RecuperarMotoPorIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            var query = MotoQueries.QuerySelecionarMotoPeloId;
            var moto = await connection.QueryFirstOrDefaultAsync<Moto>(query, new { Id = id });
            return moto;
        }
    }
}