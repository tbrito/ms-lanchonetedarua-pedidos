using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido.InputsAuxiliar;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using MediatR;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;

public class CriarPedidoInput : PedidoInput, IRequest<CriarPedidoOutput>
{
    public Pedido ToEntity()
    {
        return new Pedido(
            Cliente.ToEntity(),
            InformacaoDePagamento.ToEntity(),
            PedidoItems.Select(item => item.ToEntity()).ToList()
        );
    }
};