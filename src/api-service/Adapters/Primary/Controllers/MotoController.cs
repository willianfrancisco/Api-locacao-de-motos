using Application.DTOs;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Primary.Controllers
{
    [ApiController]
    [Route("api/motos")]
    public class MotoController(
        IMotoUseCase _motoUseCase
        ) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> RecuperarTodasAsMotosAsync()
        {
            var motos = await _motoUseCase.RecuperarTodasMotosAsync();
            return Ok(motos);
        }

        [HttpGet("{placa}")]
        public async Task<IActionResult> RecupearMotoPelaPlacaAsync([FromRoute] string placa)
        {
            var moto = await _motoUseCase.RecuperarMotoPelaPlacaAsync(placa);

            if (moto == null)
                return NotFound();

            return Ok(moto);
        }

        [HttpPost]
        public async Task<IActionResult> CriarNovaMotoAsync([FromBody] CriarNovaMotoDto criarNovaMotoDto)
        {
            await _motoUseCase.PublicaMenssagemParaFilaAsync(criarNovaMotoDto);
            return Ok();
        }

        [HttpPut("{id}/placa")]
        public async Task<IActionResult> AtualizarPlacaMotoAsync([FromRoute] int id, [FromBody] AtualizaPlacaMotoDto atualizaPlacaMotoDto)
        {
            await _motoUseCase.AtualizaPlacaMotoAsync(id, atualizaPlacaMotoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarMotoAsync([FromRoute] int id)
        {
            await _motoUseCase.DeletarMotoAsync(id);
            return NoContent();
        }
    }
}