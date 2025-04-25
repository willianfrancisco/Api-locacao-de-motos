using Application.DTOs;
using Application.Ports;
using Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Primary.Controllers
{
    [ApiController]
    [Route("api/locacoes")]
    public class LocacaoController(
        ILocacaoUseCase _locacaoUseCase,
        ISerilogLogger _logger
        ) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaLocacaoPorIdAsync([FromRoute] int id)
        {
            try
            {
                _logger.LogInfo($"Chamada ao endpoint locacoes GET locacoes/{id}, recuperar locação por id.");
                var locacao = await _locacaoUseCase.RecuperaLocacaoPorIdAsync(id);
                return Ok(locacao);
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Ocorreu um erro ao chamar o endpoint /GET/{id}, erro: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastraNovaLocacaoAsync([FromBody] CriaNovaLocacaoDto novaLocacaoDto)
        {
            try
            {
                _logger.LogInfo("Chamada ao endpoint locacoes Post /locacoes ,cadastro de nova locação");
                await _locacaoUseCase.CadastraLocacaoAsync(novaLocacaoDto);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Ocorreu um erro ao tentar cadastrar uma nova locação, erro:{ex.Message}");
                throw;
            }
        }

        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> AtualizaDataDevolucaoAsync([FromRoute] int id, [FromBody] AtualizaLocacaoDto atualizaLocacaoDto)
        {
            try
            {
                _logger.LogInfo($"Chamada ao endpoint locacoes Put /locacoes/{id}/devolucao, atualiza data devolucao.");
                await _locacaoUseCase.AtualizaDataDevolucaoAsync(id, atualizaLocacaoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Ocorreu um erro ao tentar atualizar a data de devolução, erro:{ex.Message}");
                throw;
            }
        }
    }
}