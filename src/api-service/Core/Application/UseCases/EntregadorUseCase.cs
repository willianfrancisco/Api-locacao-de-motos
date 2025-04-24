using Application.DTOs;
using Application.Ports;
using Domain.Ports;

namespace Application.UseCases
{
    public class EntregadorUseCase
    (
        IEntregadorRepository _entregadorRepository
    ) : IEntregadorUseCase
    {

        public async Task AtualizarFotoCNHentregadorAsync(int id, AtualizaFotoCnh novaFotoCnh)
        {
            var entregador = await _entregadorRepository.RecuperaEntregadorPeloIdAsync(id);

            if (entregador != null && !string.IsNullOrEmpty(entregador.CNPJ))
                _entregadorRepository.AtualizaFotoCNHEntregadorAsync(entregador.CNPJ, novaFotoCnh.novaFoto);

        }

        public async Task CadastrarEntregadorAsync(CriarNovoEntregador novoEntregador)
        {
            var possuiCadastro = await PossuiCadastro(novoEntregador.CNPJ, novoEntregador.NumeroCNH);

            if (!possuiCadastro)
            {
                await _entregadorRepository.CadastrarEntregadorAsync(novoEntregador.ConvertEntregadorDtoToEntity());
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