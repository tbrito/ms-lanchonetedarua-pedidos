using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;
using MediatR;
using Newtonsoft.Json;

namespace LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;

public class AtualizarStatusPedidoInput : IRequest<Response>
{
    public Guid PedidoId { get; set; }
    

    public string StatusPagamento { get; set; }
}