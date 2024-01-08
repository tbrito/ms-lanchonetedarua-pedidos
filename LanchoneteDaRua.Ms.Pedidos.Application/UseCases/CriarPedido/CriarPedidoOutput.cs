namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;

public class CriarPedidoOutput : Response
{
    public CriarPedidoOutput()
    {
        
    }
    public Guid Id { get; set; }
    public CriarPedidoOutput(Guid id)
    {
        Id = id;
    }
}