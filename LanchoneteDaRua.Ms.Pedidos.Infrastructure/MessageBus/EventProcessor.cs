using System.Text.Json;
using System.Text.Json.Serialization;
using LanchoneteDaRua.Ms.Pedidos.Domain.Events;

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
        var settings = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true
        };

        var queueUrl = await _messageBusClient.CreateQueueAsync("pedidos-service");

        events.ToList().ForEach(async e =>
        {
            var payload = JsonSerializer.Serialize(e, settings);
            
            await _messageBusClient.SendMessageAsync(queueUrl, payload);
        });
    }
}