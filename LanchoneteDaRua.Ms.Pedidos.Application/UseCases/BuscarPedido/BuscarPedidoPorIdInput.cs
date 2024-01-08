using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedido;

public class BuscarPedidoPorIdInput : IRequest<BuscarPedidoPorIdOutput>
{
    public Guid Id { get; set; }
}