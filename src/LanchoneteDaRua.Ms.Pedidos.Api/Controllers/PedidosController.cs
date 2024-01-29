using LanchoneteDaRua.Ms.Pedidos.Application.UseCases;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarPedido;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidoPorId;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidosPorStatus;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(CriarPedidoOutput))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CriarPedidoOutput))]
    [SwaggerOperation(Summary = "Cria um pedido")]
    public async Task<IActionResult> CriarPedido(CriarPedidoInput input)
    {
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);
        
        return Accepted(output);
    }

    [HttpGet("{Id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuscarPedidoPorIdInput))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Busca um pedido por id")]
    public async Task<IActionResult> BuscarPorId([FromRoute] BuscarPedidoPorIdInput input)
    {
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);

        return Ok(output);
    }
    
    [HttpGet("status/{Status}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuscarPedidosPorStatusOutput))]
    [SwaggerOperation(Summary = "Busca um pedido por status")]
    public async Task<IActionResult> BuscarPedidosNaFila([FromRoute]BuscarPedidosPorStatusInput input)
    {
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);

        return Ok(output);
    }
    
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AtualizarPedidoOutput))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Atualizar um pedido")]
    public async Task<IActionResult> AtualizarPedido([FromBody] AtualizarPedidoInput input, [FromRoute] Guid id)
    {
        input.Id = id;
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);
        
        return Accepted(output);
    }
    
    [HttpPatch("{Id:Guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Atualizar status de um pedido")]
    public async Task<IActionResult> AtualizarStatusPedido([FromRoute] AtualizarStatusPedidoInput input)
    {
        var output = await _mediator.Send(input);

        if (output.HasError)
            return StatusCode((int)output.ErrorCode, output.ErrorMessages);
                
        return Accepted(output);
    }
}