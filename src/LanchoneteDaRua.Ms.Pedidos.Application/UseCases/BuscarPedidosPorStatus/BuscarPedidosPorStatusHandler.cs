using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidosPorStatus;

public class BuscarPedidosPorStatusHandler : AbstractHandler<BuscarPedidosPorStatusInput, BuscarPedidosPorStatusOutput>
{
    private readonly IPedidoRepository _pedidoRepository;

    public BuscarPedidosPorStatusHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public override async Task<BuscarPedidosPorStatusOutput> Handle(BuscarPedidosPorStatusInput request, CancellationToken cancellationToken)
    {
        var pedidos = await _pedidoRepository.BuscarPedidoPorStatusAsync(request.Status);

        var filaDePedidos = BuscarPedidosPorStatusOutput.FromModelList(pedidos);
        
        return filaDePedidos;
    }
}