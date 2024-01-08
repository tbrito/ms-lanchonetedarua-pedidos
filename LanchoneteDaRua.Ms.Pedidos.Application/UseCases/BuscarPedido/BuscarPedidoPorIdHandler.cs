using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedido;

public class BuscarPedidoPorIdHandler : AbstractHandler<BuscarPedidoPorIdInput, BuscarPedidoPorIdOutput>
{
    private readonly IPedidoRepository _pedidoRepository;

    public BuscarPedidoPorIdHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public override async Task<BuscarPedidoPorIdOutput> Handle(BuscarPedidoPorIdInput input, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.BuscarPedidoPorIdAsync(input.Id);

        var output = BuscarPedidoPorIdOutput.FromEntity(pedido);
        
        return output;
    }
}