using System.Net;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using MongoDB.Driver;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;

public class AtualizarStatusPedidoInputHandler : AbstractHandler<AtualizarStatusPedidoInput, Response>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IEventProcessor _eventProcessor;

    public AtualizarStatusPedidoInputHandler(IPedidoRepository pedidoRepository, IEventProcessor eventProcessor)
    {
        _pedidoRepository = pedidoRepository;
        _eventProcessor = eventProcessor;
    }

    public override async Task<Response> Handle(AtualizarStatusPedidoInput request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.BuscarPedidoPorIdAsync(request.Id);
        
        pedido.AvancarParaProximoEstado();
        
        try
        {
            await _pedidoRepository.AtualizarPedidoAsync(pedido);
        }
        catch (MongoException ex)
        {
            return new Response {ErrorCode = HttpStatusCode.InternalServerError, ErrorMessages = $"Erro ao criar pedido - {ex.Message}"};
        }
        
        return new Response();
    }
}