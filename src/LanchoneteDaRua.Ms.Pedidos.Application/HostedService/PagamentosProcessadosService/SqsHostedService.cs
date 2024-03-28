using Amazon.SQS.Model;
using LanchoneteDaRua.Ms.Pedidos.Application.UseCases.AtualizarStatusPedido;
using LanchoneteDaRua.Ms.Pedidos.Infrastructure.MessageBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using static System.Formats.Asn1.AsnWriter;

namespace LanchoneteDaRua.Ms.Pedidos.Infrastructure.HostedService.PagamentosProcessados;

public class SqsHostedService : IHostedService, IDisposable
{
    private Timer _timer;
    private IMessageBusClient _messageBusClient;
    private IMediator _mediator;

    public IServiceProvider Services { get; }

    public SqsHostedService(IServiceProvider services)
    {
        //_messageBusClient = messageBusClient;
        //_mediator = mediator;
        Services = services;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(PollSqsQueue, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));

        return Task.CompletedTask;
    }

    private async void PollSqsQueue(object state)
    {
        using (var scope = Services.CreateScope())
        {
            _messageBusClient = scope.ServiceProvider.GetRequiredService<IMessageBusClient>();
            _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var queue = "retorno-pedidos";

            var messages = await _messageBusClient.ReceiveMessagesAsync(queue);

            messages.ForEach(message =>
            {
                var pedido = JsonConvert.DeserializeObject<AtualizarStatusPedidoInput>(message.Body);
                _mediator.Send(pedido);
            });
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}