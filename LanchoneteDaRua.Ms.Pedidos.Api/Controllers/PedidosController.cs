using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarPedido;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedido;
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
    public async Task<IActionResult> TrazerPorId([FromRoute] BuscarPedidoPorIdInput input)
    {
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);

        return Ok(output);
    }
    
    [HttpPut]
    public async Task<IActionResult> AtualizarPedido(AtualizarPedidoInput input)
    {
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