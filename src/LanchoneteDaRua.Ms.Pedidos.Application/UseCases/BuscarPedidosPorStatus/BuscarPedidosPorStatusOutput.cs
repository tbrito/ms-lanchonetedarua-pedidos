using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidosPorStatus;

public class BuscarPedidosPorStatusOutput : Response
{
    public List<PedidosLista> Pedidos { get; set; }

    public static BuscarPedidosPorStatusOutput FromModelList(List<Pedido> pedidos)
    {
        return new BuscarPedidosPorStatusOutput
        {
            Pedidos = PedidosLista.FromModelList(pedidos)
        };
    }
};

public record PedidosLista
{
    public string Senha { get; set; }
    public DateTime EntradaNaFilaEm { get; set; }

    public static PedidosLista FromModel(Pedido pedido)
    {
        var senha = pedido.Id.ToString().Substring(0, 3);
        return new PedidosLista
        {
            Senha = senha, 
            EntradaNaFilaEm = pedido.AtualizadoEm
        };
    }

    public static List<PedidosLista> FromModelList(List<Pedido> pedidos)
    {
        return pedidos.Select(p => FromModel(p)).ToList();
    }
}