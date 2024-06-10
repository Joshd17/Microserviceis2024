using MassTransit;
using SignalRService.Handlers;

namespace SignalRService.Extensions;

public static class MessagingExtensions
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<SignalMessageConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("host.docker.internal", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("signalr_queue", e =>
                {
                    e.Consumer<SignalMessageConsumer>(context);
                });
            });
        });

        return services;
    }
}