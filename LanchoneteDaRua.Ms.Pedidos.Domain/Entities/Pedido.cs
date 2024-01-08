using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;
using LanchoneteDaRua.Ms.Pedidos.Domain.Events;
using LanchoneteDaRua.Ms.Pedidos.Domain.ValueObjects;

namespace LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

public class Pedido : AggregateRoot
{
    public Pedido(Cliente cliente, InformacaoDePagamento informacaoDePagamento, List<PedidoItem> items)
    {
        Id = Guid.NewGuid();
        Total = items.Sum(item => item.Quantidade * item.Preco);
        Cliente = cliente;
        InformacaoDePagamento = informacaoDePagamento;
        Items = items;
        CriadoEm = DateTime.Now;
        Status = PedidoStatus.Recebido;

        AddEvent(new PedidoCriado(Id, Total, informacaoDePagamento, Cliente.NomeCompleto, Cliente.Email));
    }
    
    public Pedido(Cliente cliente, InformacaoDePagamento informacaoDePagamento, List<PedidoItem> items, bool update)
    {
        Id = Id;
        Total = items.Sum(item => item.Quantidade * item.Preco);
        Cliente = cliente;
        InformacaoDePagamento = informacaoDePagamento;
        Items = items;
        CriadoEm = CriadoEm;
        Status = PedidoStatus.Recebido;

        AddEvent(new PedidoAtualizado(Id, Total, informacaoDePagamento, Cliente.NomeCompleto, Cliente.Email));
    }

    public decimal Total { get; private set; }
    public Cliente Cliente { get; private set; }
    public InformacaoDePagamento InformacaoDePagamento { get; private set; }
    public List<PedidoItem> Items { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public PedidoStatus Status { get; private set; }

    public void AvancarParaProximoEstado()
    {
        Status = Status switch
        {
            PedidoStatus.Recebido => PedidoStatus.Empreparacao,
            PedidoStatus.Empreparacao => PedidoStatus.Pronto,
            PedidoStatus.Pronto => PedidoStatus.Finalizado,
            PedidoStatus.Finalizado => throw new InvalidOperationException(
                "Não é possível avançar além do estado 'Finalizado'."),
            _ => throw new InvalidOperationException("Estado desconhecido.")
        };
    }
}