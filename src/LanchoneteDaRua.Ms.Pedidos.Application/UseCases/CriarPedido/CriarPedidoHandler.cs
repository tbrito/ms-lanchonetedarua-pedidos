using System.Net;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using MongoDB.Driver;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;

public class CriarPedidoHandler : AbstractHandler<CriarPedidoInput, CriarPedidoOutput>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IEventProcessor _eventProcessor;

    public CriarPedidoHandler(IPedidoRepository pedidoRepository, IEventProcessor eventProcessor)
    {
        _pedidoRepository = pedidoRepository;
        _eventProcessor = eventProcessor;
    }

    public override async Task<CriarPedidoOutput> Handle(CriarPedidoInput request, CancellationToken cancellationToken)
    {
        var order = request.ToEntity();

        try
        {
            await _pedidoRepository.AdicionarPedidoAsync(order);
        }
        catch (MongoException ex)
        {
            return new CriarPedidoOutput {ErrorCode = HttpStatusCode.InternalServerError, ErrorMessages = $"Erro ao criar pedido - {ex.Message}"};
        }
        
        _eventProcessor.Process(order.Events);
        return new CriarPedidoOutput(order.Id);
    }
}