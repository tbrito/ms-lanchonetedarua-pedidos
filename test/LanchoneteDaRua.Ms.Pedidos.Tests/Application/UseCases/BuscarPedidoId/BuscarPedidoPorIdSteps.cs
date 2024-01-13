using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidoPorId;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;
using Moq;
using TechTalk.SpecFlow;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Application.UseCases.BuscarPedidoPorId;

[Binding]
public class BuscarPedidoPorIdSteps
{
    private BuscarPedidoPorIdHandler _handler;
    private BuscarPedidoPorIdInput _input;
    private BuscarPedidoPorIdOutput _output;
    private readonly Mock<IPedidoRepository> _mockPedidoRepository;
    private Pedido _pedidoMock;

    public BuscarPedidoPorIdSteps()
    {
        _mockPedidoRepository = new();
    }

    [Given(@"que já existe um pedido com ID '(.*)'")]
    public void DadoQueExisteUmPedidoComID(Guid id)
    {
        _pedidoMock = PedidoMock.CriarPedidoTesteComIdFake(id);
        _mockPedidoRepository.Setup(p =>
                p.BuscarPedidoPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_pedidoMock);
    }

    [Given(@"que não existe um pedido com ID '(.*)'")]
    public void DadoQueNaoExisteUmPedidoComID(Guid id)
    {
        //
    }

    [When(@"eu buscar o pedido com ID '(.*)'")]
    public async Task QuandoEuBuscarOPedidoComID(Guid id)
    {
        _input = new BuscarPedidoPorIdInput { Id = id };
        _handler = new BuscarPedidoPorIdHandler(_mockPedidoRepository.Object);
        _output = await _handler.Handle(_input, new CancellationToken());
    }

    [Then(@"o pedido com ID '(.*)' deve ser retornado")]
    public void EntaoOPedidoComIDDeveSerRetornado(Guid id)
    {
        Assert.NotNull(_output);
        Assert.Equal(id, _output.Id);
    }

    [Then(@"nenhum pedido deve ser retornado")]
    public void EntaoNenhumPedidoDeveSerRetornado()
    {
        Assert.True(_output.HasError);
    }
}
