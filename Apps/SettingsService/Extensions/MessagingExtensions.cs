using MassTransit;
using SettingsService.Handlers;

namespace SettingsService.Extensions;


public static class MessagingExtensions
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("your_queue", e =>
                {
                    e.Consumer<YourMessageConsumer>(context);
                });
            });
        });
        return services;
    }
}
