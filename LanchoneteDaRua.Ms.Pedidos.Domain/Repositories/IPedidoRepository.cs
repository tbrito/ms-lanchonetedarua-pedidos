using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

namespace LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;

public interface IPedidoRepository
{
    Task<Pedido> BuscarPedidoPorIdAsync(Guid id);
    Task AdicionarPedidoAsync(Pedido pedido);
    Task AtualizarPedidoAsync(Pedido pedido);
}