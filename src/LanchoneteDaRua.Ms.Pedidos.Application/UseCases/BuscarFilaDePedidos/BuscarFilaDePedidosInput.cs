using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarFilaDePedidos;

public record BuscarFilaDePedidosInput : IRequest<BuscarFilaDePedidosOutPut>;