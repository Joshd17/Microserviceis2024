using MassTransit;
using MessageContracts;
using Microsoft.AspNetCore.SignalR;

namespace SignalRService.Handlers;

public class SignalMessageConsumer : IConsumer<IMiddlewareMessage2>
{
    private readonly IHubContext<MessageHub> _hubContext;

    public SignalMessageConsumer(IHubContext<MessageHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task Consume(ConsumeContext<IMiddlewareMessage2> context)
    {
        await _hubContext.Clients.All.SendAsync("RecieveMessage", "We had things");
    }
}