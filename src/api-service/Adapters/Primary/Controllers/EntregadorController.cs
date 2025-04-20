using System.Threading.Tasks;
using Application.DTOs;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Primary.Controllers
{
    [ApiController]
    [Route("api/entregadores")]
    public class EntregadorController(
        IEntregadorUseCase _entregadorUseCase
    ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CadastrarNovoEntregador([FromBody]CriarNovoEntregador novoEntregador)
        {
            await _entregadorUseCase.CadastrarEntregadorAsync(novoEntregador);
            return Created();
        }

        [HttpPut("{id}/fotoCnh")]
        public async Task<IActionResult> AtualizarFotoCnhEntregador([FromRoute]int id, [FromBody]AtualizaFotoCnh fotoCnh)
        {
            await _entregadorUseCase.AtualizarFotoCNHentregador(id, fotoCnh);
            return NoContent();
        }
    }
}