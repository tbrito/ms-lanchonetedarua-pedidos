using System.Net;
using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using MongoDB.Driver;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;

public class AtualizarStatusPedidoHandler : AbstractHandler<AtualizarStatusPedidoInput, Response>
{
    private readonly IPedidoRepository _pedidoRepository;

    public AtualizarStatusPedidoHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public override async Task<Response> Handle(AtualizarStatusPedidoInput request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.BuscarPedidoPorIdAsync(request.PedidoId);

        if (pedido is null)
        {
            return new Response
            {
                ErrorCode = HttpStatusCode.InternalServerError,
                ErrorMessages = $"Erro ao criar pedido - não encontrou pedido " + request.PedidoId
            };
        }

        if (request.StatusPagamento != "NEGADO")
        {
            pedido.AvancarParaProximoEstado();
        }
        else
        {
            pedido.PagamentoRejeitado();
        }

        try
        {
            await _pedidoRepository.AtualizarPedidoAsync(pedido);
        }
        catch (MongoException ex)
        {
            return new Response
            {
                ErrorCode = HttpStatusCode.InternalServerError, ErrorMessages = $"Erro ao criar pedido - {ex.Message}"
            };
        }

        return new Response();
    }
}