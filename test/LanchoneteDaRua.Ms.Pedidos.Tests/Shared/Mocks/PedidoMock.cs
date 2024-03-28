using LanchoneteDaRua.Ms.Pedidos.Application.UseCases;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarPedido;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido.InputsAuxiliar;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;

public static class PedidoMock
{
    private readonly static Guid Id = new Guid("e4c32c9f-8f3e-4e81-8433-0b8cd1df9e77");
    public static Pedido PedidoFake()
    {
        var cliente = ClienteMock.ClienteFake();

        var informacaoDePagamento = InformacaoDePagamentoMock.InformacaoDePagamentoFake();
        
        var items = new List<PedidoItem>
        {
            new PedidoItem(Guid.NewGuid(), 12, 35.00m),
        };

        return new Pedido(cliente, informacaoDePagamento, items);
    }
    
    public static CriarPedidoInput PedidoInputFake()
    {
        var cliente = ClienteMock.ClienteInputFaker();

        var informacaoDePagamento = InformacaoDePagamentoMock.InformacaoDePagamentoInputFaker();
        
        var items = new List<PedidoItemInput>
        {
            new PedidoItemInput{IdProduto = Guid.NewGuid(), Quantidade = 8, Preco = 152m},
        };

        return new CriarPedidoInput
        {
            Cliente = cliente,
            InformacaoDePagamento = informacaoDePagamento,
            PedidoItems = items
        };
    }

    public static List<Pedido> PedidosListaFake(int status)
    {
        var pedidos = new List<Pedido>();
        
        for (var i = 0; i < 3; i++)
        {
            var pedido = PedidoFake();
            typeof(Pedido).GetProperty(nameof(Pedido.Status)).SetValue(pedido, 3);
            pedidos.Add(pedido);
        }

        return pedidos;
    }

    public static Pedido CriarPedidoTesteComIdFake(Guid id)
    {
        var pedido = PedidoFake();
        typeof(Pedido).GetProperty(nameof(Pedido.Id)).SetValue(pedido, id);
        return pedido;
    }

    public static Pedido CriarPedidoTesteAtualizadoFake()
    {
        var pedido = PedidoFake();
        pedido.AvancarParaProximoEstado();
        return pedido;
    }

    public static AtualizarPedidoInput AtualizarPedidoInputFake(Guid id)
    {
        var cliente = ClienteMock.ClienteInputFaker();
        
        var informacaoDePagamento = InformacaoDePagamentoMock.InformacaoDePagamentoInputFaker();
        
        var items = new List<PedidoItemInput>
        {
            new PedidoItemInput{ IdProduto = Guid.NewGuid(), Quantidade = 12, Preco = 35.00m},
            new PedidoItemInput{IdProduto = Guid.NewGuid(), Quantidade = 2, Preco = 43.00m},
        };
        
        return new AtualizarPedidoInput
        {
            Id = id,
            Cliente = cliente,
            InformacaoDePagamento = informacaoDePagamento,
            PedidoItems = items
        };
    }

    public static AtualizarPedidoOutput AtualizarPedidoOutputFake()
    {
        return new AtualizarPedidoOutput
        {
            Id = Id
        };
    }

    public static AtualizarStatusPedidoInput AtualizarStatusPedidoInputFake()
    {
        return new AtualizarStatusPedidoInput
        {
            PedidoId = Id,
            StatusPagamento = "Recebido"
        };
    }
}