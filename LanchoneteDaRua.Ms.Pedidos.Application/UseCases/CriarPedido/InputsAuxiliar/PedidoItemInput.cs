using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido.InputsAuxiliar;

public record PedidoItemInput
{
    public Guid IdProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public PedidoItem ToEntity()
    {
        return new PedidoItem(IdProduto, Quantidade, Preco);
    }
}