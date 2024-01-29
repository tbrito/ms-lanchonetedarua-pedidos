using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarPedidosPorStatus;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;
using Moq;
using TechTalk.SpecFlow;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Application.UseCases.BuscarFilaDePedidos;

[Binding]
public sealed class BuscarFilaDePedidosSteps
{
    private BuscarPedidosPorStatusHandler _handler;
    private BuscarPedidosPorStatusInput _input;
    private BuscarPedidosPorStatusOutput _output;
    private readonly Mock<IPedidoRepository> _mockPedidoRepository;
    private List<Pedido> _pedidosMock;

    public BuscarFilaDePedidosSteps()
    {
        _mockPedidoRepository = new Mock<IPedidoRepository>();
    }

    [Given(@"que existem pedidos com status '(.*)'")]
    public void DadoQueExistemPedidosComStatusEmPreparacao(int status)
    {
        _pedidosMock = PedidoMock.PedidosListaFake(status);
        _mockPedidoRepository.Setup(p => p.BuscarPedidoPorStatusAsync(It.IsAny<PedidoStatus>()))
            .ReturnsAsync(_pedidosMock);
    }

    [When(@"eu buscar pela fila de pedidos")]
    public async Task QuandoEuBuscarPelaFilaDePedidos()
    {
        _handler = new BuscarPedidosPorStatusHandler(_mockPedidoRepository.Object);
        _input = new BuscarPedidosPorStatusInput(); 
        _output = await _handler.Handle(_input, new CancellationToken());
    }

    [Then(@"a lista de pedidos em preparação deve ser retornada")]
    public void EntaoAListaDePedidosEmPreparacaoDeveSerRetornada()
    {
        var emPreparacao = _pedidosMock.Count(p => p.Status == PedidoStatus.Empreparacao);
        Assert.NotNull(_output);
        Assert.Equal(emPreparacao, _output.Pedidos.Count);
        Assert.IsAssignableFrom<BuscarPedidosPorStatusOutput>(_output);
    }
}