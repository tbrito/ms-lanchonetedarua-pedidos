using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarFilaDePedidos;

public class BuscarFilaDePedidosHandler : AbstractHandler<BuscarFilaDePedidosInput, BuscarFilaDePedidosOutPut>
{
    private readonly IPedidoRepository _pedidoRepository;

    public BuscarFilaDePedidosHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public override async Task<BuscarFilaDePedidosOutPut> Handle(BuscarFilaDePedidosInput request, CancellationToken cancellationToken)
    {
        var pedidos = await _pedidoRepository.BuscarPedidoPorStatusAsync(PedidoStatus.Empreparacao);

        var filaDePedidos = BuscarFilaDePedidosOutPut.FromModelList(pedidos);
        
        return filaDePedidos;
    }
}