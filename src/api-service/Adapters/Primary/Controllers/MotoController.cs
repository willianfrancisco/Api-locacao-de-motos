using Application.DTOs;
using Application.Ports;
using Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Primary.Controllers
{
    [ApiController]
    [Route("api/motos")]
    public class MotoController(
        IMotoUseCase _motoUseCase,
        ISerilogLogger _logger
        ) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> RecuperarTodasAsMotosAsync()
        {
            try
            {
                _logger.LogInfo("Chamada ao endpoint motos /GET recupera todas as motos.");
                var motos = await _motoUseCase.RecuperarTodasMotosAsync();
                return Ok(motos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao chamar o endpoint GET /motos, recuperar todas as motos, erro:{ex.Message}");
                throw;
            }
        }

        [HttpGet("{placa}")]
        public async Task<IActionResult> RecupearMotoPelaPlacaAsync([FromRoute] string placa)
        {
            try
            {
                _logger.LogInfo($"Chamada ao endpoint GET /motos/{placa}.");
                var moto = await _motoUseCase.RecuperarMotoPelaPlacaAsync(placa);

                if (moto == null)
                    return NotFound();

                return Ok(moto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao chamar endpoint GET /motos/{placa}, erro:{ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarNovaMotoAsync([FromBody] CriarNovaMotoDto criarNovaMotoDto)
        {
            try
            {
                _logger.LogInfo("Chamada ao endpoint POST /motos.");
                await _motoUseCase.PublicaMenssagemParaFilaAsync(criarNovaMotoDto);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao chamar o endpoint POST /motos, erros:{ex.Message}");
                throw;
            }
        }

        [HttpPut("{id}/placa")]
        public async Task<IActionResult> AtualizarPlacaMotoAsync([FromRoute] int id, [FromBody] AtualizaPlacaMotoDto atualizaPlacaMotoDto)
        {
            try
            {
                _logger.LogInfo($"Chamada ao endpoint PUT /motos/{id}/placa.");
                await _motoUseCase.AtualizaPlacaMotoAsync(id, atualizaPlacaMotoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao chamar o endpoint PUT /motos{id}/placa, erro:{ex.Message}");
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarMotoAsync([FromRoute] int id)
        {
            try
            {
                _logger.LogInfo($"Chamada ao endpoint DELETE /motos/{id}.");
                await _motoUseCase.DeletarMotoAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro ao chamar o endpoint DELETE /motos/{id}, erro: {ex.Message}");
                throw;
            }
        }
    }
}