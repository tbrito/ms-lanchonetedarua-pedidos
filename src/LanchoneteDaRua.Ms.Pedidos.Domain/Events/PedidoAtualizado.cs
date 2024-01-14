using LanchoneteDaRua.Ms.Pedidos.Domain.ValueObjects;

namespace LanchoneteDaRua.Ms.Pedidos.Domain.Events;

public class PedidoAtualizado : IDomainEvent
{
    public PedidoAtualizado(Guid id, decimal total, InformacaoDePagamento informacaoDePagamento, string nomeCompleto, string email, string queueName)
    {
        Id = id;
        Total = total;
        InformacaoDePagamento = informacaoDePagamento;
        NomeCompleto = nomeCompleto;
        Email = email;
        QueueName = queueName;
    }

    public Guid Id { get; private set; }
    public decimal Total { get; private set; }
    public InformacaoDePagamento InformacaoDePagamento { get; private set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public string QueueName { get; set; }
}