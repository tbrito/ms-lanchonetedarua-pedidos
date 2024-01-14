using LanchoneteDaRua.Ms.Pedidos.Domain.Events;

namespace LanchoneteDaRua.Ms.Pedidos.Domain.Entities;

public abstract class AggregateRoot : IEntityBase
{
    private readonly List<IDomainEvent> _events = new();
    public Guid Id { get; protected set; }
    public IEnumerable<IDomainEvent> Events => _events;

    protected void AddEvent(IDomainEvent @events)
    {
        _events.Add(@events);
    }
}