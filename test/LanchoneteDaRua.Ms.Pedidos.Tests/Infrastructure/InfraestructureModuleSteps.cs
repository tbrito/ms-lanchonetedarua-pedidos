using Amazon.SQS;
using LanchoneteDaRua.Ms.Pedidos.Domain.Repositories;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TechTalk.SpecFlow;

namespace LanchoneteDaRua.Ms.Pedidos.Tests.Infrastructure;

[Binding]
public class InfraestruturaSteps
{
    private IServiceCollection _services;
    private IServiceProvider _provider;
    private IConfiguration _configuration;

    [Given(@"que o IServiceCollection está configurado")]
    public void DadoQueOIServiceCollectionEstaConfigurado()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables();
        
        _configuration = configurationBuilder.Build();
        _services = new ServiceCollection();
        _services.AddSingleton<IConfiguration>(_configuration);
    }

    [When(@"adiciono o módulo de infraestrutura")]
    public void QuandoAdicionoAConfiguracaoDoMongoDB()
    {
        _services.AddInfraestructureLayer(_configuration);
        _provider = _services.BuildServiceProvider();
    }

    [Then(@"o cliente e o banco de dados do MongoDB devem estar disponíveis para injeção")]
    public void EntaoOClienteEOBancoDeDadosDoMongoDBDevemEstarDisponiveisParaInjecao()
    {
        Assert.NotNull(_provider.GetService<IMongoClient>());
        Assert.NotNull(_provider.GetService<IMongoDatabase>());
    }
    
    [Then(@"os repositórios devem estar disponíveis para injeção")]
    public void EntaoOsRepositóriosDevemEstarDisponíveisParaInjecao()
    {
        Assert.NotNull(_provider.GetService<IPedidoRepository>());
    }
    
    [Then(@"os serviços do Message Bus devem estar disponíveis para injeção")]
    public void EntaoOsServicosDoMessageBusDevemEstarDisponiveisParaInjecao()
    {
        Assert.NotNull(_provider.GetService<IMessageBusClient>());
        Assert.NotNull(_provider.GetService<IEventProcessor>());
        Assert.NotNull(_provider.GetService<IAmazonSQS>());
    }

}
