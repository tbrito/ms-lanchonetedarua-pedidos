using System.Net;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using MongoDB.Driver;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarPedido;

public class AtualizarPedidoHandler : AbstractHandler<AtualizarPedidoInput, Response>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IEventProcessor _eventProcessor;

    public AtualizarPedidoHandler(IPedidoRepository pedidoRepository, IEventProcessor eventProcessor)
    {
        _pedidoRepository = pedidoRepository;
        _eventProcessor = eventProcessor;
    }

    public override async Task<Response> Handle(AtualizarPedidoInput request, CancellationToken cancellationToken)
    {
        var pedido = request.ToEntityUpdate();
        
        try
        {
            await _pedidoRepository.AtualizarPedidoAsync(pedido);
        }
        catch (MongoException ex)
        {
            return new Response {ErrorCode = HttpStatusCode.InternalServerError, ErrorMessages = $"Erro ao criar pedido - {ex.Message}"};
        }
        _eventProcessor.Process(pedido.Events);

        return new Response();
    }
}