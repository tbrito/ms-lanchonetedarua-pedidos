using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.ValueObjects;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;

public static class PedidoMock
{
    public static Pedido CriarPedidoTeste()
    {
        var cliente = new Cliente(new Guid("cd2dd635-125e-4a0a-be7d-713b775212bc"), "Joao testador", "joaotestador@lanchonetedarua.com");
        var informacaoDePagamento = new InformacaoDePagamento("4565152625132541", "Joao Testador", "07/2023", "450");
        var items = new List<PedidoItem>
        {
            new PedidoItem(Guid.NewGuid(), 12, 35.00m),
        };

        return new Pedido(cliente, informacaoDePagamento, items);
    }

    public static Pedido CriarPedidoTesteComId(Guid id)
    {
        var pedido = CriarPedidoTeste();
        typeof(Pedido).GetProperty(nameof(Pedido.Id)).SetValue(pedido, id);
        return pedido;
    }

    public static Pedido CriarPedidoTesteAtualizado()
    {
        var pedido = CriarPedidoTeste();
        pedido.AvancarParaProximoEstado();
        return pedido;
    }
}