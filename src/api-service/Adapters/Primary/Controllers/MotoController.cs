using Application.DTOs;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Primary.Controllers
{
    [ApiController]
    [Route("api/Motos")]
    public class MotoController(
        IMotoUseCase _motoUseCase
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CriarNovaMoto([FromBody]CriarNovaMotoDto criarNovaMotoDto)
        {
            await _motoUseCase.PublicaMenssagemParaFila(criarNovaMotoDto);
            return Ok();
        }
    }
}