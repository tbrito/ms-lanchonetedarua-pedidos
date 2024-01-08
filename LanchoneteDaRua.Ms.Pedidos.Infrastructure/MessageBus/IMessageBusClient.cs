using Amazon.SQS.Model;

namespace LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;

public interface IMessageBusClient
{
    Task<string> CreateQueueAsync(string queueName);
    Task<SendMessageResponse> SendMessageAsync(string queueUrl, string messageBody);
    Task<List<Message>> ReceiveMessagesAsync(string queueUrl);
}