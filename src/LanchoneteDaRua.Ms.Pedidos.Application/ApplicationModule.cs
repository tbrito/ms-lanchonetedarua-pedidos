using LanchoneteDaRua.Ms.Pedidos.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

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