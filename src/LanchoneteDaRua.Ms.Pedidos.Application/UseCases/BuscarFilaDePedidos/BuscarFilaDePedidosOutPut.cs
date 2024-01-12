using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarFilaDePedidos;

public class BuscarFilaDePedidosOutPut : Response
{
    public List<PedidosFila> PedidosNaFila { get; set; }

    public static BuscarFilaDePedidosOutPut FromModelList(List<Pedido> pedidos)
    {
        return new BuscarFilaDePedidosOutPut
        {
            PedidosNaFila = PedidosFila.FromModelList(pedidos)
        };
    }
};

public record PedidosFila
{
    public string Senha { get; set; }
    public DateTime EntradaNaFilaEm { get; set; }

    public static PedidosFila FromModel(Pedido pedido)
    {
        var senha = pedido.Id.ToString().Substring(0, 3);
        return new PedidosFila
        {
            Senha = senha, 
            EntradaNaFilaEm = pedido.AtualizadoEm
        };
    }

    public static List<PedidosFila> FromModelList(List<Pedido> pedidos)
    {
        return pedidos.Select(p => FromModel(p)).ToList();
    }
}