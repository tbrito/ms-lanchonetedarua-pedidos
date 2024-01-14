using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using MongoDB.Driver;

namespace LanchoneteDaRua.Ms.Pedidos.Infrastructure.Repository;

public class PedidoRepository : IPedidoRepository
{
    private readonly IMongoCollection<Pedido> _pedidosCollection;

    public PedidoRepository(IMongoDatabase database)
    {
        _pedidosCollection = database.GetCollection<Pedido>("pedidos");
    }

    public async Task<Pedido> BuscarPedidoPorIdAsync(Guid id)
    {
        var pedido =  await _pedidosCollection.Find(x => x.Id == id).SingleOrDefaultAsync();
        return pedido;
    }
    
    public async Task<List<Pedido>> BuscarPedidoPorStatusAsync(PedidoStatus status)
    {
        return await _pedidosCollection.Find(x => x.Status == status).ToListAsync();
    }

    public async Task AdicionarPedidoAsync(Pedido pedido)
    {
        await _pedidosCollection.InsertOneAsync(pedido);
    }

    public async Task AtualizarPedidoAsync(Pedido pedido)
    {
        await _pedidosCollection.ReplaceOneAsync(o => o.Id == pedido.Id, pedido);
    }
}