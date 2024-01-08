using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarPedido;

public class AtualizarPedidoInput : CriarPedidoInput
{
    public Pedido ToEntityUpdate()
    {
        return new Pedido(
            Cliente.ToEntity(),
            InformacaoDePagamento.ToEntity(),
            PedidoItems.Select(item => item.ToEntity()).ToList(),
            true
        );
    }
}