using Application.DTOs;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Primary.Controllers
{
    [ApiController]
    [Route("api/locacoes")]
    public class LocacaoController(
        ILocacaoUseCase _locacaoUseCase
        ) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaLocacaoPorIdAsync([FromRoute]int id)
        {
            var locacao = await _locacaoUseCase.RecuperaLocacaoPorIdAsync(id);
            return Ok(locacao);
        }

        [HttpPost]
        public async Task<IActionResult> CadastraNovaLocacaoAsync([FromBody]CriaNovaLocacaoDto novaLocacaoDto)
        {
            await _locacaoUseCase.CadastraLocacaoAsync(novaLocacaoDto);
            return Created();
        }

        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> AtualizaDataDevolucaoAsync([FromRoute] int id, [FromBody]AtualizaLocacaoDto atualizaLocacaoDto)
        {
            await _locacaoUseCase.AtualizaDataDevolucaoAsync(id, atualizaLocacaoDto);
            return NoContent();
        }
    }
}