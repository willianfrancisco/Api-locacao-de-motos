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
        IConfiguration _configuration,
        ISerilogLogger _logger
        ) : IEntregadorRepository
    {
        private readonly string _connectionString = _configuration.GetConnectionString("ApiConnectionString") ?? "";

        public void AtualizaFotoCNHEntregadorAsync(string cnpj, string fotoNova)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar atualizar a foto da cnh do entregador, erro{ex.Message}");
                throw;
            }

        }

        public async Task CadastrarEntregadorAsync(Entregador entregador)
        {
            try
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
                SalvarFotoCnh(entregador.CNPJ ?? "", entregador.FotoCNH ?? "");
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar cadastrar um novo entregador, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar cadastrar um novo entregador, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar cadastrar um novo entregador, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar cadastrar um novo entregador, erro{ex.Message}");
                throw;
            }
        }

        public async Task<Entregador> RecuperaEntregadorPelaCNHAsync(string cnh)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                var query = EntregadorQueries.QuerySelectEntregadorPelaCNH;

                var entregador = await connection.QueryFirstOrDefaultAsync<Entregador>(query, new { NumeroCNH = cnh });

                return entregador ?? new Entregador();
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pela cnh, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pela cnh, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pela cnh, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pela cnh, erro{ex.Message}");
                throw;
            }
        }

        public async Task<Entregador> RecuperaEntregadorPeloCNPJAsync(string cnpj)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                var query = EntregadorQueries.QuerySelectEntregadorPeloCnpj;

                var entregador = await connection.QueryFirstOrDefaultAsync<Entregador>(query, new { CNPJ = cnpj });

                return entregador ?? new Entregador();
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pelo cnpj, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pelo cnpj, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pelo cnpj, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pelo cnpj, erro{ex.Message}");
                throw;
            }

        }

        public async Task<Entregador> RecuperaEntregadorPeloIdAsync(int id)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                var query = EntregadorQueries.QuerySelectEntregadorPeloId;

                var entregador = await connection.QueryFirstOrDefaultAsync<Entregador>(query, new { Id = id });

                return entregador ?? new Entregador();
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pelo id, erro{ex.Message}");
                throw;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pelo id, erro{ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pelo id, erro{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar recuperar um entregador pelo id, erro{ex.Message}");
                throw;
            }
        }

        private void SalvarFotoCnh(string cnpj, string fotoCnh)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro no repositorio ao tentar salvar a foto cnh localmente, erro:{ex.Message}");
                throw;
            }
        }
    }
}