using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidoPorId;

public class BuscarPedidoPorIdOutput : Response
{
    public BuscarPedidoPorIdOutput()
    {
        
    }
    public BuscarPedidoPorIdOutput(Guid id, decimal total, DateTime criadoEm, string status)
    {
        Id = id;
        Total = total;
        CriadoEm = criadoEm;
        Status = status;
    }

    public Guid Id { get; set; }
    public decimal Total { get; set; }
    public DateTime CriadoEm { get; set; }
    public string Status { get; set; }
    public static BuscarPedidoPorIdOutput FromEntity(Pedido pedido)
    {
        return new BuscarPedidoPorIdOutput(
            pedido.Id,
            pedido.Total, 
            pedido.CriadoEm,
            pedido.Status.ToString()
        );
    }
}