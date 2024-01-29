using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;

namespace LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;

public interface IPedidoRepository
{
    Task<Pedido> BuscarPedidoPorIdAsync(Guid id);
    Task<List<Pedido>> BuscarPedidoPorStatusAsync(PedidoStatus status);
    Task AdicionarPedidoAsync(Pedido pedido);
    Task AtualizarPedidoAsync(Pedido pedido);
}