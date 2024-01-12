using System.Linq.Expressions;
using LanchoneteDaRua.Ms.Pedidos.Domain.Entities;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.Repository;
using LanchoneteDaRua.Ms.Pedidos.Tests.Shared.Mocks;
using MongoDB.Driver;
using Moq;
using TechTalk.SpecFlow;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Infrastructure.Repository;

[Binding]
public class PedidoRepositorySteps
{
    private readonly PedidoRepository _pedidoRepository;
    private Pedido _pedido;
    private Pedido _resultadoBusca;
    private Guid _pedidoId;
    private Mock<IMongoCollection<Pedido>> _mockPedidosCollection;

    public PedidoRepositorySteps()
    {
        _mockPedidosCollection = new Mock<IMongoCollection<Pedido>>();
        var mockDatabase = new Mock<IMongoDatabase>();
        mockDatabase.Setup(m => m.GetCollection<Pedido>("pedidos", null))
            .Returns(_mockPedidosCollection.Object);

        _pedidoRepository = new PedidoRepository(mockDatabase.Object);
    }

    [Given(@"que existe um pedido com o ID '(.*)'")]
    public void DadoQueExisteUmPedidoComOID(string id)
    {
        _pedidoId = new Guid(id);
        _pedido = PedidoMock.CriarPedidoTeste();

        var mockCursor = new Mock<IAsyncCursor<Pedido>>();
        mockCursor.SetupSequence(_ => _.Current)
            .Returns(new[] { _pedido }) // Retorna o pedido desejado
            .Returns(Enumerable.Empty<Pedido>());

        mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true)
            .Returns(false);

        mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true) 
            .ReturnsAsync(false);
        
        _mockPedidosCollection.Setup(c => c.FindSync(
                It.Is<Expression<Func<Pedido, bool>>>(expr => expr.Compile().Invoke(_pedido)),
                It.IsAny<FindOptions<Pedido, Pedido>>(),
                It.IsAny<CancellationToken>()))
            .Returns(mockCursor.Object);
    }

    [Given(@"que eu tenho um novo pedido")]
    public void DadoQueEuTenhoUmNovoPedido()
    {
        _pedido = PedidoMock.CriarPedidoTeste();
    }

    [Given(@"eu tenho uma atualização para esse pedido")]
    public void DadoEuTenhoUmaAtualizacaoParaEssePedido()
    {
        _pedido = PedidoMock.CriarPedidoTesteAtualizado();
    }

    [When(@"eu buscar o pedido com o ID '(.*)'")]
    public async void QuandoEuBuscarOPedidoComOID(string id)
    {
        _resultadoBusca = await _pedidoRepository.BuscarPedidoPorIdAsync(new Guid(id));
    }

    [When(@"eu adicionar este pedido")]
    public async void QuandoEuAdicionarEstePedido()
    {
        await _pedidoRepository.AdicionarPedidoAsync(_pedido);
    }

    [When(@"eu atualizar o pedido com o ID '(.*)'")]
    public async void QuandoEuAtualizarOPedidoComOID(string id)
    {
        await _pedidoRepository.AtualizarPedidoAsync(_pedido);
    }

    [Then(@"o pedido correspondente deve ser retornado")]
    public void EntaoOPedidoCorrespondenteDeveSerRetornado()
    {
        Assert.Equal(_pedido.Id, _resultadoBusca.Id);
    }

    [Then(@"o pedido deve ser salvo no sistema")]
    public async void EntaoOPedidoDeveSerSalvoNoSistema()
    {
        var pedidoSalvo = await _pedidoRepository.BuscarPedidoPorIdAsync(_pedido.Id);
        Assert.NotNull(pedidoSalvo);
    }

    [Then(@"o pedido atualizado deve ser salvo no sistema")]
    public async void EntaoOPedidoAtualizadoDeveSerSalvoNoSistema()
    {
        var pedidoAtualizado = await _pedidoRepository.BuscarPedidoPorIdAsync(_pedido.Id);
        Assert.NotNull(pedidoAtualizado);
    }
}