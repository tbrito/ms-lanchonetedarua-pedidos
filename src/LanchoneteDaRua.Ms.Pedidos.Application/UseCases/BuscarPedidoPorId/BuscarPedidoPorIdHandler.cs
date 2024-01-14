using System.Net;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidoPorId;

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

        if (pedido is not Pedido)
        {
            return new BuscarPedidoPorIdOutput
            {
                ErrorCode = HttpStatusCode.NotFound,
                ErrorMessages = ""
            };
        }

        var output = BuscarPedidoPorIdOutput.FromEntity(pedido);
        
        return output;
    }
}