using Application.DTOs;
using Application.Ports;
using Domain.Ports;
using Newtonsoft.Json;

namespace Application.UseCases
{
    public class MotoUseCase(
        IMotoPublishToQueue _motoPublishToQueue,
        IMotoRepository _motoRepository,
        ISerilogLogger _logger
        ) : IMotoUseCase
    {
        public async Task AtualizaPlacaMotoAsync(int id, AtualizaPlacaMotoDto atualizaPlacaMotoDto)
        {
            try
            {
                var moto = await _motoRepository.RecuperarMotoPorIdAsync(id);

                if (moto != null)
                {
                    await _motoRepository.AtualizaPlacaMotoAsync(id, atualizaPlacaMotoDto.placa);
                    _logger.LogInfo($"Placa atualizada com sucesso. Moto: {id}, Placa nova{atualizaPlacaMotoDto.placa}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar atualizar a placa da moto, erro:{ex.Message}");
                throw;
            }
        }

        public async Task DeletarMotoAsync(int id)
        {
            try
            {
                var moto = await _motoRepository.RecuperarMotoPorIdAsync(id);

                if (moto != null)
                {
                    await _motoRepository.DeletarMotoAsync(id);
                    _logger.LogInfo($"Moto deletada com sucesso, moto:{id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar deleter uma moto {id}, erro:{ex.Message}");
                throw;
            }
        }

        public async Task PublicaMenssagemParaFilaAsync(CriarNovaMotoDto criarNovaMotoDto)
        {
            try
            {
                var mensagem = JsonConvert.SerializeObject(criarNovaMotoDto);
                await _motoPublishToQueue.PublicaMenssagemParaFilaAsync(mensagem);
                _logger.LogInfo("Mensagem publicada na fila do rabbit com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar publicar mensagem na fila, erro:{ex.Message}");
                throw;
            }
        }

        public async Task<LerMotoDto> RecuperarMotoPelaPlacaAsync(string placa)
        {
            try
            {
                _logger.LogInfo($"Realizada consulta para recuperar moto pela placa:{placa}.");
                var moto = await _motoRepository.RecuperarMotoPelaPlacaAsync(placa);
                return moto.ConverterParaMotoDto();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar recuperar a moto pela placa{placa}, erro:{ex.Message}");
                throw;
            }
        }

        public async Task<LerMotoDto> RecuperarMotoPeloIdAsync(int id)
        {
            try
            {
                _logger.LogInfo($"Realizada consulta para recuperar moto por id:{id}");
                var moto = await _motoRepository.RecuperarMotoPorIdAsync(id);
                return moto.ConverterParaMotoDto();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar recuperar moto pelo id:{id}, erro:{ex.Message}");
                throw;
            }
        }

        public async Task<List<LerMotoDto>> RecuperarTodasMotosAsync()
        {
            try
            {
                _logger.LogInfo("Realizada consulta para recuperar todas as motos.");
                var motos = await _motoRepository.ListarTodasMotosCadastradasAsync();
                return motos.ConverterParaListaMotoDto();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao tentar recupera todas as motos. erro:{ex.Message}");
                throw;
            }
        }
    }
}