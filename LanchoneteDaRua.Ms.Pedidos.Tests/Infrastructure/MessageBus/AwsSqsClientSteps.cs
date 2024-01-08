using Amazon.SQS;
using Amazon.SQS.Model;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using Moq;
using TechTalk.SpecFlow;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Infrastructure.MessageBus;

[Binding]
public class AwsSqsClientSteps
{
    private readonly AwsSqsClient _awsSqsClient;
    private readonly Mock<IAmazonSQS> _sqsClientMock = new ();
    private string _queueUrl;
    private string _messageBody;
    private List<Message> _receivedMessages;
    private bool _isSendMessageCalled;
    public AwsSqsClientSteps()
    {
        _awsSqsClient = new AwsSqsClient(_sqsClientMock.Object);
    }

    [Given(@"que eu desejo criar uma nova fila com o nome ""(.*)""")]
    public void DadoQueEuDesejoCriarUmaNovaFilaComONome(string queueName)
    {
        _sqsClientMock.Setup(client => client.CreateQueueAsync(It.IsAny<CreateQueueRequest>(), default))
                      .ReturnsAsync(new CreateQueueResponse { QueueUrl = $"queueName" });
    }

    [When(@"eu solicitar a criação da fila")]
    public async Task QuandoEuSolicitarACriacaoDaFila()
    {
        _queueUrl = await _awsSqsClient.CreateQueueAsync("minha-fila-teste");
    }

    [Then(@"uma nova fila chamada ""(.*)"" deve ser criada")]
    public void EntaoUmaNovaFilaChamadaDeveSerCriada(string queueName)
    {
        _sqsClientMock.Verify(client => 
            client.CreateQueueAsync(It.Is<CreateQueueRequest>(req => req.QueueName == queueName), default), Times.Once);
    }
    
    [Then(@"o URL da fila recém-criada chamada ""(.*)"" deve ser retornado")]
    public void EntaoOUrlDaFilaRecemCriadaDeveSerRetornada(string queueName)
    {
        Assert.Contains(queueName, "minha-fila-teste");
    }

    [Given(@"que eu tenho uma fila com o URL ""(.*)""")]
    public void DadoQueEuTenhoUmaFilaComOURL(string queueUrl)
    {
        _queueUrl = queueUrl;
    }

    [Given(@"eu tenho uma mensagem para enviar")]
    public void DadoEuTenhoUmaMensagemParaEnviar()
    {
        _messageBody = "Test Message";
        _sqsClientMock.Setup(client => client.SendMessageAsync(It.IsAny<SendMessageRequest>(), default))
            .ReturnsAsync(new SendMessageResponse());
    }

    [When(@"eu enviar a mensagem para a fila")]
    public async Task QuandoEuEnviarAMensagemParaAFila()
    {
        await _awsSqsClient.SendMessageAsync(_queueUrl, _messageBody);
    }

    [Then(@"a mensagem deve ser adicionada à fila ""(.*)""")]
    public void EntaoAMensagemDeveSerAdicionadaAFila(string queueUrl)
    {
        _sqsClientMock.Verify(m => m.SendMessageAsync(It.IsAny<SendMessageRequest>(),default), Times.Once);
    }

    [Given(@"que eu tenho uma fila com mensagens com o URL ""(.*)""")]
    public void DadoQueEuTenhoUmaFilaComMensagensComOURL(string queueUrl)
    {
        _queueUrl = queueUrl;
        var messageList = new List<Message> { new Message { Body = "Test Message" } };
        _sqsClientMock.Setup(m => m.ReceiveMessageAsync(It.IsAny<ReceiveMessageRequest>(), default))
                      .ReturnsAsync(new ReceiveMessageResponse { Messages = messageList });
    }

    [When(@"eu solicitar para receber mensagens da fila")]
    public async Task QuandoEuSolicitarParaReceberMensagensDaFila()
    {
        _receivedMessages = await _awsSqsClient.ReceiveMessagesAsync(_queueUrl);
    }

    [Then(@"as mensagens da fila ""(.*)"" devem ser retornadas")]
    public void EntaoAsMensagensDaFilaDevemSerRetornadas(string queueUrl)
    {
        _sqsClientMock.Verify(m => m.ReceiveMessageAsync(It.Is<ReceiveMessageRequest>(req => req.QueueUrl == queueUrl), default), Times.Once);
        Assert.NotNull(_receivedMessages);
        Assert.True(_receivedMessages.Any());
    }
}
