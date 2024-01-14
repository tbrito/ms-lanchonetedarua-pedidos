using LanchoneteDaRua.Ms.Pedidos.Domain.Events;

namespace LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;

public interface IEventProcessor
{
    void Process(IEnumerable<IDomainEvent> events);
}