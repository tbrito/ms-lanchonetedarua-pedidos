using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido.InputsAuxiliar;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases;

public class PedidoInput
{
    public ClienteInput Cliente { get; set; }
    public InformacaoDePagamentoInput InformacaoDePagamento { get; set; }
    public List<PedidoItemInput> PedidoItems { get; set; }
}