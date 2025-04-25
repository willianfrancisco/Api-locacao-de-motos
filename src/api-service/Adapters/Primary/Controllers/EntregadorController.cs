using Application.DTOs;
using Application.Ports;
using Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Primary.Controllers
{
    [ApiController]
    [Route("api/entregadores")]
    public class EntregadorController(
        IEntregadorUseCase _entregadorUseCase,
        ISerilogLogger _logger
    ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CadastrarNovoEntregadorAsync([FromBody] CriarNovoEntregador novoEntregador)
        {
            try
            {
                _logger.LogInfo($"Chamada ao endpoint POST /entregadores, cadastrando entregador:{novoEntregador.Nome}.");
                await _entregadorUseCase.CadastrarEntregadorAsync(novoEntregador);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao chamar endpoint POST /entregadores, erro:{ex.Message}");
                throw;
            }
        }

        [HttpPut("{id}/fotoCnh")]
        public async Task<IActionResult> AtualizarFotoCnhEntregadorAsync([FromRoute] int id, [FromBody] AtualizaFotoCnh fotoCnh)
        {
            try
            {
                _logger.LogInfo($"Chamada ao endpoint PUT /entregadores/{id}/fotoCnh.");
                await _entregadorUseCase.AtualizarFotoCNHentregadorAsync(id, fotoCnh);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro ao chamar endpoint PUT /entregadores{id}/fotoCnh, erro:{ex.Message}");
                throw;
            }
        }
    }
}