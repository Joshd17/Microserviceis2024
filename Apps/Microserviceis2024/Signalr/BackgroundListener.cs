using Microsoft.AspNetCore.SignalR.Client;

namespace Microserviceis2024.Signalr;

public class BackgroundListener : IHostedService
{
    protected async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://signalrservice/messagehub", config => config.HttpMessageHandlerFactory = (x) => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .Build();
        connection.On<string>("ReceiveMessage", (message) =>
        {
            Console.WriteLine($"Received message: {message}");
        });
        await connection.StartAsync(cancellationToken: stoppingToken);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //await ExecuteAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        
    }
}
