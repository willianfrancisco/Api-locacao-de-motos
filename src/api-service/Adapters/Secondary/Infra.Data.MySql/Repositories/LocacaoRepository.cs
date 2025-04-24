using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infra.Data.MySql.Queries;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infra.Data.MySql.Repositories
{
    public class LocacaoRepository(IConfiguration _configuration) : ILocacaoRepository
    {
        private readonly string _connectionString = _configuration.GetConnectionString("ApiConnectionString") ?? "";

        public async Task AtualizaDataDevolucaoAsync(int id, DateTime dataDevolucao)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = LocacaoQueries.QueryAtualizaDataTerminoLocacao;
            await connection.ExecuteAsync(query, new { Id = id, DataDevolucao = dataDevolucao });
        }

        public async Task CadastrarLocacaoAsync(Locacao locacao)
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

        public async Task<Locacao> RecuperaLocacaoPorIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = LocacaoQueries.QuerySelectLocacaoPeloId;
            var locacao = await connection.QueryFirstOrDefaultAsync<Locacao>(query, new { Id = id });
            return locacao;
        }
    }
}