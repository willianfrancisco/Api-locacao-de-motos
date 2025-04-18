using Application.DTOs;
using Application.Ports;
using Domain.Ports;
using Newtonsoft.Json;

namespace Application.UseCases
{
    public class MotoUseCase(
        IMotoPublishToQueue _motoPublishToQueue,
        IMotoRepository _motoRepository
        ) : IMotoUseCase
    {
        public async Task AtualizaPlacaMotoAsync(int id,AtualizaPlacaMotoDto atualizaPlacaMotoDto)
        {
            var moto = await _motoRepository.RecuperarMotoPorIdAsync(id);

            if(moto != null)
                await _motoRepository.AtualizaPlacaMotoAsync(id,atualizaPlacaMotoDto.placa);
        }

        public async Task DeletarMotoAsync(int id)
        {
            var moto = await _motoRepository.RecuperarMotoPorIdAsync(id);

            if(moto != null)
                await _motoRepository.DeletarMotoAsync(id);
        }

        public async Task PublicaMenssagemParaFilaAsync(CriarNovaMotoDto criarNovaMotoDto)
        {
            var mensagem = JsonConvert.SerializeObject(criarNovaMotoDto);
            await _motoPublishToQueue.PublicaMenssagemParaFilaAsync(mensagem);
        }

        public async Task<LerMotoDto> RecuperarMotoPelaPlacaAsync(string placa)
        {
            var moto = await _motoRepository.RecuperarMotoPelaPlacaAsync(placa);
            return moto.ConverterParaMotoDto();
        }

        public async Task<LerMotoDto> RecuperarMotoPeloIdAsync(int id)
        {
            var moto = await _motoRepository.RecuperarMotoPorIdAsync(id);
            return moto.ConverterParaMotoDto();
        }

        public async Task<List<LerMotoDto>> RecuperarTodasMotosAsync()
        {
            var motos = await _motoRepository.ListarTodasMotosCadastradasAsync();
            return motos.ConverterParaListaMotoDto();
        }
    }
}