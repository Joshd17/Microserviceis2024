﻿using MassTransit;
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
                cfg.Host("host.docker.internal", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("settings_queue", e =>
                {
                    e.Consumer<SettingsMessageConsumer>();
                });
            });
        });
        return services;
    }
}
