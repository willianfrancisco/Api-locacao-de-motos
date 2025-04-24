using Application.DTOs;
using Application.Ports;
using Domain.Ports;

namespace Application.UseCases
{
    public class LocacaoUseCase(
        ILocacaoRepository _locacaoRepository,
        IMotoRepository _motoRepository,
        IEntregadorRepository _entregadorRepository
        ) : ILocacaoUseCase
    {
        public async Task AtualizaDataDevolucaoAsync(int id, AtualizaLocacaoDto locacaoDto)
        {
            var locacao = await _locacaoRepository.RecuperaLocacaoPorIdAsync(id);

            if (locacao != null)
                await _locacaoRepository.AtualizaDataDevolucaoAsync(id, locacaoDto.NovaDataTermino);
        }

        public async Task CadastraLocacaoAsync(CriaNovaLocacaoDto novaLocacaoDto)
        {
            var possuiCadastro = await ValidaSeExisteEntregadorMotoParaLocacao(novaLocacaoDto.EntregadorId, novaLocacaoDto.MotoId);
            var possuiTipoCnhValido = await ValidaSePossuiCnhValidaParaLocacao(novaLocacaoDto.EntregadorId);

            if (possuiCadastro && possuiTipoCnhValido)
            {
                await _locacaoRepository.CadastrarLocacaoAsync(novaLocacaoDto.ConverterParaLocacaoEntity());
            }
        }

        public async Task<LerLocacaoDto> RecuperaLocacaoPorIdAsync(int id)
        {
            var locacao = await _locacaoRepository.RecuperaLocacaoPorIdAsync(id);
            return locacao.ConverterParaLocacaoDto();
        }

        private async Task<bool> ValidaSeExisteEntregadorMotoParaLocacao(int entregadorId, int motoId)
        {
            bool possuiCadastro = false;

            var entregador = await _entregadorRepository.RecuperaEntregadorPeloId(entregadorId);
            var moto = await _motoRepository.RecuperarMotoPorIdAsync(motoId);

            if (moto != null && entregador != null)
                possuiCadastro = true;

            return possuiCadastro;
        }

        // Somente serão permitidos locação de motos para entregadores que tiverem habilitação do tipo A e A+B
        private async Task<bool> ValidaSePossuiCnhValidaParaLocacao(int entregadorId)
        {
            bool tipoCnhValidaParaLocacao = false;

            var entregador = await _entregadorRepository.RecuperaEntregadorPeloId(entregadorId);

            if (entregador.TipoCNH == Domain.Enums.ETipoCNH.A || entregador.TipoCNH == Domain.Enums.ETipoCNH.AB)
                tipoCnhValidaParaLocacao = true;

            return tipoCnhValidaParaLocacao;
        }
    }
}