using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.CriarPedido;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;
using MongoDB.Driver;
using Moq;
using TechTalk.SpecFlow;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Application.UseCases.CriarPedido;

[Binding]
public class CriarPedidoSteps
{
    private CriarPedidoHandler _handler;
    private Mock<IPedidoRepository> _mockPedidoRepository;
    private Mock<IEventProcessor> _mockEventProcessor;
    private CriarPedidoInput _input;
    private CriarPedidoOutput _output;

    public CriarPedidoSteps()
    {
        _mockPedidoRepository = new();
        _mockEventProcessor = new Mock<IEventProcessor>();
    }

    [Given(@"que eu tenho os detalhes de um novo pedido")]
    public void DadoQueEuTenhoOsDetalhesDeUmNovoPedido()
    {
        _input = PedidoMock.PedidoInputFake();
    }

    [Given(@"ocorre um problema no banco de dados")]
    public void DadoQueOcorreUmProblemaNoBancoDeDados()
    {
        _mockPedidoRepository.Setup(p =>
                p.AdicionarPedidoAsync(It.IsAny<Pedido>()))
            .ThrowsAsync(new MongoException("Erro ao inserir Pedido"));
    }

    [When(@"eu criar o pedido")]
    public async Task QuandoEuCriarOPedido()
    {
        _mockPedidoRepository.Setup(p =>
            p.AdicionarPedidoAsync(It.IsAny<Pedido>()));
        
        _handler = new CriarPedidoHandler(_mockPedidoRepository.Object, _mockEventProcessor.Object);
        _output = await _handler.Handle(_input, new CancellationToken());
    }
    
    
    [When(@"eu tentar criar o pedido")]
    public async Task QuandoEuTentarCriarOPedido()
    {
        _handler = new CriarPedidoHandler(_mockPedidoRepository.Object, _mockEventProcessor.Object);
        _output = await _handler.Handle(_input, new CancellationToken());
    }

    [Then(@"um novo pedido deve ser criado")]
    public void EntaoUmNovoPedidoDeveSerCriado()
    {
        Assert.NotNull(_output);
        Assert.NotNull(_output.Id);
    }

    [Then(@"um erro com o código 'InternalServerError' deve ser retornado")]
    public void EntaoUmErroComOCodigoInternalServerErrorDeveSerRetornado()
    {
        Assert.NotNull(_output);
        Assert.Equal(HttpStatusCode.InternalServerError, _output.ErrorCode);
    }
}
