using System.Net;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using MongoDB.Driver;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarPedido;

public class AtualizarPedidoHandler : AbstractHandler<AtualizarPedidoInput, AtualizarPedidoOutput>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IEventProcessor _eventProcessor;

    public AtualizarPedidoHandler(IPedidoRepository pedidoRepository, IEventProcessor eventProcessor)
    {
        _pedidoRepository = pedidoRepository;
        _eventProcessor = eventProcessor;
    }

    public override async Task<AtualizarPedidoOutput> Handle(AtualizarPedidoInput request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.BuscarPedidoPorIdAsync(request.Id);
        
        pedido.AtualizarPedido(
            request.Cliente.ToEntity(),
            request.InformacaoDePagamento.ToEntity(),
            request.PedidoItems.Select(p => p.ToEntity()).ToList(),
            true
            );
        
        try
        {
            await _pedidoRepository.AtualizarPedidoAsync(pedido);
        }
        catch (MongoException ex)
        {
            return new AtualizarPedidoOutput {ErrorCode = HttpStatusCode.InternalServerError, ErrorMessages = $"Erro ao atualizar pedido - {ex.Message}"};
        }
        
        _eventProcessor.Process(pedido.Events);

        return new AtualizarPedidoOutput{Id = pedido.Id};
    }
}