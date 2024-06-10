
using MassTransit;
using Microserviceis2024.Middleware;
using Microserviceis2024.Signalr;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Microserviceis2024;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("host.docker.internal", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });
        builder.Services.AddHostedService<BackgroundListener>();

        builder.Services.AddHostedService<BusService>();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("ocelot.json")
            .Build();
        
        builder.Services.AddOcelot(configuration);
        var app = builder.Build();

        await app.UseOcelot();
        app.UseMiddleware<HeaderMiddleware>();

        await app.RunAsync();
    }
}
