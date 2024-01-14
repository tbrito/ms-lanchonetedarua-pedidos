using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarPedido;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarFilaDePedidos;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidoPorId;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteDaRua.Ms.Pedidos.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PedidosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido(CriarPedidoInput input)
    {
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);
        
        return Accepted(output);
    }

    [HttpGet("{Id:Guid}")]
    public async Task<IActionResult> BuscarPorId([FromRoute] BuscarPedidoPorIdInput input)
    {
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);

        return Ok(output);
    }
    
    [HttpGet("fila")]
    public async Task<IActionResult> BuscarPedidosNaFila(BuscarFilaDePedidosInput input)
    {
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);

        return Ok(output);
    }
    
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> AtualizarPedido([FromBody] AtualizarPedidoInput input, [FromRoute] Guid id)
    {
        input.Id = id;
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);
        
        return Accepted(output);
    }
    
    [HttpPatch("{Id:Guid}")]
    public async Task<IActionResult> AtualizarStatusPedido([FromRoute] AtualizarStatusPedidoInput input)
    {
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);
                
        return Accepted(output);
    }
}