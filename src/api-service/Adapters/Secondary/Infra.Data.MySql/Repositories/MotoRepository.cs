using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infra.Data.MySql.Queries;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infra.Data.MySql.Repositories
{
    public class MotoRepository(
        IConfiguration _configuration,
        ISerilogLogger _logger
        ) : IMotoRepository
    {
        private readonly string _connectionString = _configuration.GetConnectionString("ApiConnectionString") ?? "";

        public async Task AtualizaPlacaMotoAsync(int id, string placa)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();
                var query = MotoQueries.QueryAtualizaPlaca;
                await connection.ExecuteAsync(query, new { Id = id, Placa = placa });
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar atualizar a placa da moto, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar atualizar a placa da moto, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar atualizar a placa da moto, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar atualizar a placa da moto, erro{ex.Message}");
                throw;
            }
        }

        public async Task DeletarMotoAsync(int id)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();
                var query = MotoQueries.QueryDeletarMoto;
                await connection.ExecuteAsync(query, new { Id = id });
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar deletar uma moto, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar deletar uma moto, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar deletar uma moto, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar deletar uma moto, erro{ex.Message}");
                throw;
            }
        }

        public async Task<List<Moto>> ListarTodasMotosCadastradasAsync()
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();
                var query = MotoQueries.QuerySelecionatTodasAsMotos;
                var motos = await connection.QueryAsync<Moto>(query);
                return motos.ToList();
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar todas as motos, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar todas as motos, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar todas as motos, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar todas as motos, erro{ex.Message}");
                throw;
            }
        }

        public async Task<Moto> RecuperarMotoPelaPlacaAsync(string placa)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();
                var query = MotoQueries.QuerySelecionarMotoPelaPlaca;
                var moto = await connection.QueryFirstOrDefaultAsync<Moto>(query, new { Placa = placa });
                return moto ?? new Moto();
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar uma moto pela placa, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar uma moto pela placa, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar uma moto pela placa, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar uma moto pela placa, erro{ex.Message}");
                throw;
            }
        }

        public async Task<Moto> RecuperarMotoPorIdAsync(int id)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();
                var query = MotoQueries.QuerySelecionarMotoPeloId;
                var moto = await connection.QueryFirstOrDefaultAsync<Moto>(query, new { Id = id });
                return moto ?? new Moto();
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar moto pelo id, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar moto pelo id, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar moto pelo id, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar moto pelo id, erro{ex.Message}");
                throw;
            }
        }
    }
}