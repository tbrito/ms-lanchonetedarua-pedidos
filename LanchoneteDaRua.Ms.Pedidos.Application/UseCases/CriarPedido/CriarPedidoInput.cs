using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido.InputsAuxiliar;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;

public class CriarPedidoInput : IRequest<CriarPedidoOutput>
{
    public ClienteInput Cliente { get; set; }
    public InformacaoDePagamentoInput InformacaoDePagamento { get; set; }
    public List<PedidoItemInput> PedidoItems { get; set; }

    public Pedido ToEntity()
    {
        return new Pedido(
            Cliente.ToEntity(),
            InformacaoDePagamento.ToEntity(),
            PedidoItems.Select(item => item.ToEntity()).ToList()
        );
    }
};