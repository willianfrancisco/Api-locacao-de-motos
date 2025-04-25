using Application.DTOs;
using Application.Ports;
using Domain.Ports;

namespace Application.UseCases
{
    public class EntregadorUseCase
    (
        IEntregadorRepository _entregadorRepository,
        ISerilogLogger _logger
    ) : IEntregadorUseCase
    {

        public async Task AtualizarFotoCNHentregadorAsync(int id, AtualizaFotoCnh novaFotoCnh)
        {
            try
            {
                var entregador = await _entregadorRepository.RecuperaEntregadorPeloIdAsync(id);

                if (entregador != null && !string.IsNullOrEmpty(entregador.CNPJ))
                {
                    _entregadorRepository.AtualizaFotoCNHEntregadorAsync(entregador.CNPJ, novaFotoCnh.novaFoto);
                    _logger.LogInfo($"Foto cnh atualizada com sucesso, entregador: {entregador.Nome}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar atualizar a foto da cnh do entregador, erro {ex.Message}.");
                throw;
            }

        }

        public async Task CadastrarEntregadorAsync(CriarNovoEntregador novoEntregador)
        {
            try
            {
                var possuiCadastro = await PossuiCadastro(novoEntregador.CNPJ, novoEntregador.NumeroCNH);

                if (!possuiCadastro)
                {
                    await _entregadorRepository.CadastrarEntregadorAsync(novoEntregador.ConvertEntregadorDtoToEntity());
                    _logger.LogInfo($"Entregador cadastrado com sucesso,{novoEntregador.Nome}");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar cadastrar o entregador, {ex.Message}");
                throw;
            }

        }

        private async Task<bool> PossuiCadastro(string cnpj, string cnh)
        {
            bool possuiCnhCadastrada = false;
            bool possuiCnpjCadastrado = false;
            bool possuiCadastro = false;

            if (!string.IsNullOrEmpty(cnh))
                possuiCnhCadastrada = await _entregadorRepository.RecuperaEntregadorPelaCNHAsync(cnh) != null ? true : false;

            if (!string.IsNullOrEmpty(cnpj))
                possuiCnpjCadastrado = await _entregadorRepository.RecuperaEntregadorPeloCNPJAsync(cnpj) != null ? true : false;

            if (possuiCnhCadastrada || possuiCnpjCadastrado)
                possuiCadastro = true;

            return possuiCadastro;
        }
    }
}