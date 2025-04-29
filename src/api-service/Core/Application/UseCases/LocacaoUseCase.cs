using Application.DTOs;
using Application.Ports;
using Domain.Ports;

namespace Application.UseCases
{
    public class LocacaoUseCase(
        ILocacaoRepository _locacaoRepository,
        IMotoRepository _motoRepository,
        IEntregadorRepository _entregadorRepository,
        ISerilogLogger _logger
        ) : ILocacaoUseCase
    {
        public async Task AtualizaDataDevolucaoAsync(int id, AtualizaLocacaoDto locacaoDto)
        {
            try
            {
                var locacao = await _locacaoRepository.RecuperaLocacaoPorIdAsync(id);

                if (locacao != null)
                {
                    await _locacaoRepository.AtualizaDataDevolucaoAsync(id, locacaoDto.NovaDataTermino);
                    _logger.LogInfo($"Data devolução atualizada com sucesso para a locacao {locacao.Id}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar atualizar a data de devolução, erro:{ex.Message}");
                throw;
            }
        }

        public async Task CadastraLocacaoAsync(CriaNovaLocacaoDto novaLocacaoDto)
        {
            try
            {
                var possuiCadastro = await ValidaSeExisteEntregadorMotoParaLocacao(novaLocacaoDto.EntregadorId, novaLocacaoDto.MotoId);
                var possuiTipoCnhValido = await ValidaSePossuiCnhValidaParaLocacao(novaLocacaoDto.EntregadorId);

                if (possuiCadastro && possuiTipoCnhValido)
                {
                    await _locacaoRepository.CadastrarLocacaoAsync(novaLocacaoDto.ConverterParaLocacaoEntity());
                    _logger.LogInfo("Nova locacao cadastrada com sucesso.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar cadastrar nova locacao, erro:{ex.Message}");
                throw;
            }
        }

        public async Task<LerLocacaoDto> RecuperaLocacaoPorIdAsync(int id)
        {
            try
            {
                _logger.LogInfo($"Realizada consultada para locacao:{id}");
                var locacao = await _locacaoRepository.RecuperaLocacaoPorIdAsync(id);
                return locacao.ConverterParaLocacaoDto();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar recuperar locacao pelo id, erro:{ex.Message}");
                throw;
            }
        }

        public async Task<LerLocacaoDto> RecuperaLocacaoPorMotoIdAsync(int motoId)
        {
            try
            {
                _logger.LogInfo($"Realizada consultada para recuperar locacao pelo id da moto:{motoId}");
                var locacao = await _locacaoRepository.RecuperaLocacaoPorMotoIdAsync(motoId);
                return locacao.ConverterParaLocacaoDto();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar recuperar locacao pelo id da moto, erro:{ex.Message}");
                throw;
            }
        }

        private async Task<bool> ValidaSeExisteEntregadorMotoParaLocacao(int entregadorId, int motoId)
        {
            bool possuiCadastro = false;

            var entregador = await _entregadorRepository.RecuperaEntregadorPeloIdAsync(entregadorId);
            var moto = await _motoRepository.RecuperarMotoPorIdAsync(motoId);

            if (moto != null && entregador != null)
                possuiCadastro = true;

            return possuiCadastro;
        }

        // Somente serão permitidos locação de motos para entregadores que tiverem habilitação do tipo A e A+B
        private async Task<bool> ValidaSePossuiCnhValidaParaLocacao(int entregadorId)
        {
            bool tipoCnhValidaParaLocacao = false;

            var entregador = await _entregadorRepository.RecuperaEntregadorPeloIdAsync(entregadorId);

            if (entregador.TipoCNH == Domain.Enums.ETipoCNH.A || entregador.TipoCNH == Domain.Enums.ETipoCNH.AB)
                tipoCnhValidaParaLocacao = true;

            return tipoCnhValidaParaLocacao;
        }
    }
}