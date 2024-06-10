using MassTransit;
using MessageContracts;

namespace Microserviceis2024.Middleware;

public class HeaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;
    
    public HeaderMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            var xHeader = context.Request.Headers.FirstOrDefault(x => x.Key == "X-Tenant");

            if (context.Request.Method == HttpMethods.Post)
            {
                var asyncHeader = context.Request.Headers.Any(x => x.Key == "X-Async");

                if (asyncHeader)
                {
                    await publishEndpoint.Publish<IMiddlewareMessage2>(new { Text = "A middleware message", XTenant = xHeader });
                    return;
                }
            }

        }

        await _next(context);
    }
}