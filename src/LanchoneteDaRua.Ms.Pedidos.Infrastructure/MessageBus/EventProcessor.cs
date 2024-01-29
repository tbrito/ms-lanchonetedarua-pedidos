using LanchoneteDaRua.Ms.Pedidos.Domain.Events;
using Newtonsoft.Json;

namespace LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;

public class EventProcessor : IEventProcessor
{
    private readonly IMessageBusClient _messageBusClient;

    public EventProcessor(IMessageBusClient messageBusClient)
    {
        _messageBusClient = messageBusClient;
    }

    public async void Process(IEnumerable<IDomainEvent> events)
    {
        foreach (var e in events.ToList())
        {
            var queueUrl = await _messageBusClient.CreateQueueAsync(e.QueueName);
            var payload = JsonConvert.SerializeObject(e);
            
            await _messageBusClient.SendMessageAsync(queueUrl, payload);
        }
    }

}