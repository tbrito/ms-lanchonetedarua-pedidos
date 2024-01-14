namespace LanchoneteDaRua.Ms.Pedidos.Domain.Events;

public interface IDomainEvent
{
    string QueueName { get; set; }
}