using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;

public class AtualizarStatusPedidoInput : IRequest<Response>
{
    public Guid Id { get; set; }
}