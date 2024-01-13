using Bogus;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarPedido;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;
using MongoDB.Driver;
using Moq;
using TechTalk.SpecFlow;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Application.UseCases.AtualizarPedidos;

[Binding]
public class AtualizarPedidoHandlerSteps
{
    private readonly Mock<IPedidoRepository> _mockPedidoRepository;
    private readonly AtualizarPedidoHandler _handler;
    private Guid _id;
    private AtualizarPedidoInput _input;
    private AtualizarPedidoOutput _output;

    public AtualizarPedidoHandlerSteps()
    {
        _mockPedidoRepository = new Mock<IPedidoRepository>();
        var mockEventProcessor = new Mock<IEventProcessor>();
        _handler = new AtualizarPedidoHandler(_mockPedidoRepository.Object, mockEventProcessor.Object);
    }

    [Given(@"Um pedido existente com ID '(.*)'")]
    public void DadoUmPedidoExistenteComID(string featId)
    {
        _id = new Guid(featId);
        var pedido = PedidoMock.CriarPedidoTesteComIdFake(_id);
        _mockPedidoRepository.SetupSequence(r => r.BuscarPedidoPorIdAsync(_id))
            .ReturnsAsync(pedido);
    }

    [Given(@"O pedido precisa ser atualizado")]
    public void DadoOPedidoPrecisaSerAtualizado()
    {
        _input = PedidoMock.AtualizarPedidoInputFake(_id);
        _output = PedidoMock.AtualizarPedidoOutputFake();
    }

    [Given(@"Ocorre um erro no banco de dados")]
    public void DadoOcorreUmErroNoBancoDeDados()
    {
        _mockPedidoRepository.Setup(r => r.AtualizarPedidoAsync(It.IsAny<Pedido>()))
            .ThrowsAsync(new MongoException("Erro de banco de dados"));
    }

    [When(@"Eu atualizo o pedido")]
    public async Task QuandoEuAtualizoOPedido()
    {
        _output = await _handler.Handle(_input, CancellationToken.None);
    }

    [Then(@"O pedido deve ser atualizado com sucesso")]
    public void EntaoOPedidoDeveSerAtualizadoComSucesso()
    {
        Assert.NotNull(_output);
        Assert.Equal(_input.Id, _output.Id);
    }

    [Then(@"A atualização deve falhar com um erro de banco de dados")]
    public void EntaoAAtualizacaoDeveFalharComUmErroDeBancoDeDados()
    {
        Assert.NotNull(_output);
        Assert.True(_output.HasError);
    }
}
