using System.Text.Json;
using Dapper;
using Domain.Entities;
using Domain.Ports;
using Infra.Data.MySql.Queries;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infra.Data.MySql.Repositories
{
    public class EntregadorRepository(
        IConfiguration _configuration
        ) : IEntregadorRepository
    {
        private readonly string _connectionString = _configuration.GetConnectionString("ApiConnectionString") ?? "";

        public void AtualizaFotoCNHEntregador(string cnpj, string fotoNova)
        {
            string pastaDeDestino = Path.Combine("..", "Secondary", "Infra.Data.MySql", "Dados");
            string caminhoArquivo = Path.Combine(pastaDeDestino, "empregados.json");

            if (!File.Exists(caminhoArquivo))
                return;

            var json = File.ReadAllText(caminhoArquivo);
            var entregadores = JsonSerializer.Deserialize<List<DadosEntregador>>(json) ?? new List<DadosEntregador>();

            var dadosEntregador = entregadores.FirstOrDefault(e => e.CNPJ == cnpj);

            if (dadosEntregador != null)
            {
                dadosEntregador.FotoCNH = fotoNova;

                var novoJson = JsonSerializer.Serialize(entregadores, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(caminhoArquivo, novoJson);
            }


        }

        public async Task CadastrarEntregadorAsync(Entregador entregador)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = EntregadorQueries.QueryInserirNovoEntregado;

            var novoEntregador = new
            {
                Nome = entregador.Nome,
                CNPJ = entregador.CNPJ,
                DataNascimento = entregador.DataNascimento,
                NumeroCNH = entregador.NumeroCNH,
                TipoCNH = entregador.TipoCNH
            };

            await connection.ExecuteAsync(query, novoEntregador);
            SalvarFotoCnh(entregador.CNPJ, entregador.FotoCNH);
        }

        public async Task<Entregador> RecuperaEntregadorPelaCNH(string cnh)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = EntregadorQueries.QuerySelectEntregadorPelaCNH;

            var entregador = await connection.QueryFirstOrDefaultAsync<Entregador>(query, new { NumeroCNH = cnh });

            return entregador;
        }

        public async Task<Entregador> RecuperaEntregadorPeloCNPJ(string cnpj)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = EntregadorQueries.QuerySelectEntregadorPeloCnpj;

            var entregador = await connection.QueryFirstOrDefaultAsync<Entregador>(query, new { CNPJ = cnpj });

            return entregador;
        }

        public async Task<Entregador> RecuperaEntregadorPeloId(int id)
        {

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = EntregadorQueries.QuerySelectEntregadorPeloId;

            var entregador = await connection.QueryFirstOrDefaultAsync<Entregador>(query, new { Id = id });

            return entregador;

        }

        private void SalvarFotoCnh(string cnpj, string fotoCnh)
        {
            List<DadosEntregador> entregadores;

            var entregador = new DadosEntregador(cnpj, fotoCnh);

            string pastaDeDestino = Path.Combine("..", "Secondary", "Infra.Data.MySql", "Dados");

            if (!Directory.Exists(pastaDeDestino))
                Directory.CreateDirectory(pastaDeDestino);

            string caminhoArquivo = Path.Combine(pastaDeDestino, "empregados.json");

            if (File.Exists(caminhoArquivo))
            {
                var entregadoresExistentes = File.ReadAllText(caminhoArquivo);
                entregadores = JsonSerializer.Deserialize<List<DadosEntregador>>(entregadoresExistentes) ?? new List<DadosEntregador>();
            }
            else
            {
                entregadores = new List<DadosEntregador>();
            }

            entregadores.Add(entregador);

            var json = JsonSerializer.Serialize(entregadores, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(caminhoArquivo, json);
        }
    }
}