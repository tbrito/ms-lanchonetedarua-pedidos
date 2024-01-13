using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.BuscarFilaDePedidos;
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
    private BuscarFilaDePedidosHandler _handler;
    private BuscarFilaDePedidosInput _input;
    private BuscarFilaDePedidosOutPut _output;
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
        _handler = new BuscarFilaDePedidosHandler(_mockPedidoRepository.Object);
        _input = new BuscarFilaDePedidosInput(); 
        _output = await _handler.Handle(_input, new CancellationToken());
    }

    [Then(@"a lista de pedidos em preparação deve ser retornada")]
    public void EntaoAListaDePedidosEmPreparacaoDeveSerRetornada()
    {
        var emPreparacao = _pedidosMock.Count(p => p.Status == PedidoStatus.Empreparacao);
        Assert.NotNull(_output);
        Assert.Equal(emPreparacao, _output.PedidosNaFila.Count);
        Assert.IsAssignableFrom<BuscarFilaDePedidosOutPut>(_output);
    }
}