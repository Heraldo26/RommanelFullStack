using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rommanel.Application.Commands;
using Rommanel.Application.Queries;

namespace Rommanel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ISender _mediator;
        public ClientesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("CriarCliente")]
        public async Task<IActionResult> CriarCliente([FromBody] CriarClienteCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet, Route("ObterClientePorId/{idCliente}")]
        public async Task<IActionResult> ObterClientePorId(int idCliente)
        {
            var cliente = await _mediator.Send(new ObterClientePorIdQuery(idCliente));

            if (cliente == null) return NotFound();

            return Ok(cliente);
        }

        [HttpGet, Route("ListarClientes")]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _mediator.Send(new ListarClientesQuery());

            return Ok(clientes);
        }

        [HttpPut, Route("AtualizarCliente")]
        public async Task<IActionResult> AtualizarCliente([FromBody] AtualizarClienteCommand command)
        {
            await _mediator.Send(command);
            
            return Ok();
        }

        [HttpDelete, Route("DeletarCliente/{idCliente}")]
        public async Task<IActionResult> DeletarCliente(int idCliente)
        {
            await _mediator.Send(new ExcluirClienteCommand(idCliente));

            return Ok();
        }
    }
}
