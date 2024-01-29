using LanchoneteDaRua.Ms.Pedidos.Domain.Events;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;
using Moq;
using TechTalk.SpecFlow;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Infrastructure.MessageBus;

[Binding]
public sealed class EventProcessorSteps
{
    private List<IDomainEvent> events = new ();
    private readonly Mock<IMessageBusClient> _messageBusClientMock = new ();
    private readonly EventProcessor _eventProcessorSteps;

    public EventProcessorSteps()
    {
        _eventProcessorSteps = new EventProcessor(_messageBusClientMock.Object);
    }

    [Given(@"que eu tenho uma lista de eventos de domínio")]
    public void DadoQueEuTenhoUmaListaDeEventosDeDominio()
    {
        events.Add(DomainEventsMock.PedidoCriadoFake.Generate());
    }

    [When(@"eu processar esses eventos")]
    public void QuandoEuProcessarEssesEventos()
    {
        _eventProcessorSteps.Process(events);
    }

    [Then(@"uma fila deve ser criada")]
    public void EntaoUmaFilaDeveSerCriada()
    {
        _messageBusClientMock.Verify(m => m.CreateQueueAsync(It.IsAny<string>()), Times.Once);
    }

    [Then(@"cada evento deve ser serializado e enviado para a fila")]
    public void EntaoCadaEventoDeveSerSerializadoEEnviadoParaAFila()
    {
        _messageBusClientMock.Verify(m => m.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(events.Count));
    }
}