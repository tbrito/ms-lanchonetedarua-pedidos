using LanchoneteDaRua.Ms.Pedidos.Application.UseCases;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.HostedService.PagamentosProcessados;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LanchoneteDaRua.Ms.Pedidos.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(AbstractHandler<,>).Assembly);
        });
        
        return services;
    }
}