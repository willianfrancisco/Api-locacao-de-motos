using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infra.Data.MySql.Queries;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infra.Data.MySql.Repositories
{
    public class LocacaoRepository(
        IConfiguration _configuration,
        ISerilogLogger _logger
        ) : ILocacaoRepository
    {
        private readonly string _connectionString = _configuration.GetConnectionString("ApiConnectionString") ?? "";

        public async Task AtualizaDataDevolucaoAsync(int id, DateTime dataDevolucao)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                var query = LocacaoQueries.QueryAtualizaDataTerminoLocacao;
                await connection.ExecuteAsync(query, new { Id = id, DataDevolucao = dataDevolucao });
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar atualizar a data de devolução, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar atualizar a data de devolução, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar atualizar a data de devolução, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar atualizar a data de devolução, erro{ex.Message}");
                throw;
            }
        }

        public async Task CadastrarLocacaoAsync(Locacao locacao)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                var query = LocacaoQueries.QueryInserirLocacao;
                var diasLocacao = locacao.DataInicio.AddDays(locacao.Plano);

                var novaLocacao = new
                {
                    EntregadorId = locacao.EntregadorId,
                    MotoId = locacao.MotoId,
                    DataInicio = locacao.DataInicio,
                    DataTermino = diasLocacao,
                    DataDevolucao = diasLocacao,
                    Plano = locacao.Plano
                };

                await connection.ExecuteAsync(query, novaLocacao);
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar cadastrar nova locação, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar cadastrar nova locação, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar cadastrar nova locação, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar cadastrar nova locação, erro{ex.Message}");
                throw;
            }
        }

        public async Task<Locacao> RecuperaLocacaoPorIdAsync(int id)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                var query = LocacaoQueries.QuerySelectLocacaoPeloId;
                var locacao = await connection.QueryFirstOrDefaultAsync<Locacao>(query, new { Id = id });
                return locacao ?? new Locacao();
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar locação por id, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar locação por id, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar locação por id, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar locação por id, erro{ex.Message}");
                throw;
            }
        }
    }
}