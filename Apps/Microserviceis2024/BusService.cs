﻿using MassTransit;

namespace Microserviceis2024;

public class BusService : IHostedService
{
    private readonly IBusControl _busControl;
    public BusService(IBusControl busControl)
    {
        _busControl = busControl;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return _busControl.StartAsync(cancellationToken);
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _busControl.StopAsync(cancellationToken);
    }
}
