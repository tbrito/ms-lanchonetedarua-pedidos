using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarPedido;

public class AtualizarPedidoInput : PedidoInput, IRequest<AtualizarPedidoOutput>
{
    public Guid Id { get; set; }
}