using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;
using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidosPorStatus;

public record BuscarPedidosPorStatusInput : IRequest<BuscarPedidosPorStatusOutput>
{
    public PedidoStatus Status { get; set; }
};