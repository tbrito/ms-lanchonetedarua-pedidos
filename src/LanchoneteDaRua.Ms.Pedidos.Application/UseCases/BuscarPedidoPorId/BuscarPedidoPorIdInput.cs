using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidoPorId;

public class BuscarPedidoPorIdInput : IRequest<BuscarPedidoPorIdOutput>
{
    public Guid Id { get; set; }
}