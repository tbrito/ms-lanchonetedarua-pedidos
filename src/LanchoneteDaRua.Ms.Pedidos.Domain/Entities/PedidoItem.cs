namespace LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

public class PedidoItem
{
    public PedidoItem(Guid idProduto, int quantidade, decimal preco)
    {
        Id = Guid.NewGuid();
        IdProduto = idProduto;
        Quantidade = quantidade;
        Preco = preco;
    }

    public Guid Id { get; private set; }
    public Guid IdProduto { get; private set; }
    public int Quantidade { get; private set; }
    public decimal Preco { get; private set; }

}