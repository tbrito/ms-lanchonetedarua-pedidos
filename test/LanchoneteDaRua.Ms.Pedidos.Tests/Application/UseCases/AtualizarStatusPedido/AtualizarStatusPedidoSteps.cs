using LanchoneteDaRua.Ms.Pedidos.Application.UseCases;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Domain.Enums;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;
using MongoDB.Driver;
using Moq;
using TechTalk.SpecFlow;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Application.UseCases.AtualizarStatusPedido;

[Binding]
public sealed class AtualizarStatusPedidoSteps
{
    private Mock<IPedidoRepository> _mockPedidoRepository;
    private AtualizarStatusPedidoHandler _handler;
    private AtualizarStatusPedidoInput _input;
    private Response _response;
    private Pedido _pedido;

    public AtualizarStatusPedidoSteps()
    {
        _mockPedidoRepository = new Mock<IPedidoRepository>();
        _handler = new AtualizarStatusPedidoHandler(_mockPedidoRepository.Object);
    }

    [Given(@"que existe um pedido com ID '(.*)'")]
    public void DadoQueExisteUmPedidoComID(Guid id)
    {
        var pedido = PedidoMock.CriarPedidoTesteComIdFake(id);
        _mockPedidoRepository.Setup(r => r.BuscarPedidoPorIdAsync(id))
            .ReturnsAsync(pedido);
    }

    [Given(@"o status atual do pedido é '(.*)'")]
    public async Task DadoOStatusAtualDoPedidoE(string status)
    {
        _pedido = PedidoMock.PedidoFake();
        _input = PedidoMock.AtualizarStatusPedidoInputFake();
    }
    
    [Given(@"ocorre um erro ao salvar no banco")]
    public void DadoOcorreUmErroAoSalvarNoBanco()
    {
        _mockPedidoRepository.Setup(r => r.AtualizarPedidoAsync(It.IsAny<Pedido>()))
            .ThrowsAsync(new MongoException("Erro de banco de dados"));
    }

    [When(@"eu atualizar o status do pedido para '(.*)'")]
    public async Task QuandoEuAtualizarOStatusDoPedidoPara(string status)
    {
        _response = await _handler.Handle(_input, new CancellationToken());
    }
    
    [Then(@"a resposta não deve retornar erro")]
    public void EntaoOStatusDoPedidoDeveSer()
    {
        Assert.False(_response.HasError);
    }
    
    [Then(@"a resposta deve retornar que houve erro")]
    public void EntaoARespostaDeveRetornarQueHouveErro()
    {
        Assert.True(_response.HasError);
    }
}