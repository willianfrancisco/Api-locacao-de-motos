using Application.DTOs;
using Application.Ports;
using Domain.Ports;
using Newtonsoft.Json;

namespace Application.UseCases
{
    public class MotoUseCase(
        IMotoPublishToQueue _motoPublishToQueue
        ) : IMotoUseCase
    {
        public async Task PublicaMenssagemParaFila(CriarNovaMotoDto criarNovaMotoDto)
        {
            var mensagem = JsonConvert.SerializeObject(criarNovaMotoDto);
            await _motoPublishToQueue.PublicaMenssagemParaFila(mensagem);
        }
    }
}