using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;

public class AwsSqsClient : IMessageBusClient
{
    private readonly IAmazonSQS _sqsClient;
    public AwsSqsClient(IAmazonSQS sqsClient)
    {
        _sqsClient = sqsClient;
    }

    public async Task<string> CreateQueueAsync(string queueName)
    {
        var createQueueResponse = await _sqsClient.CreateQueueAsync(new CreateQueueRequest
        {
            QueueName = queueName
        });

        return createQueueResponse.QueueUrl;
    }

    public async Task<SendMessageResponse> SendMessageAsync(string queueUrl, string messageBody)
    {
        var response = await _sqsClient.SendMessageAsync(new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = messageBody
        });
        
        return response;
    }

    public async Task<List<Message>> ReceiveMessagesAsync(string queue)
    {
        var queueUrl = $"https://sqs.us-east-1.amazonaws.com/997423607772/{queue}";
        var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
        {
            QueueUrl = queueUrl,
            MaxNumberOfMessages = 10,
            WaitTimeSeconds = 20
        });

        return receiveMessageResponse.Messages;
    }
}
