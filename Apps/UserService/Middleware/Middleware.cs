using MassTransit;

namespace UserService.Middleware;

public class Middleware
{
    private readonly RequestDelegate _next;
    private readonly IPublishEndpoint _publishEndpoint;

    public Middleware(RequestDelegate next, IPublishEndpoint publishEndpoint)
    {
        _next = next;
        _publishEndpoint = publishEndpoint;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post)
        {
            var xHeader = context.Request.Headers.FirstOrDefault(x => x.Key == "X-Tenant");
            var asyncHeader = context.Request.Headers.FirstOrDefault(x => x.Key == "X-Async");
        }
        
        await _publishEndpoint.Publish<IUserMessage>(new { Text = "Hello, world!" });

        foreach (var header in context.Request.Headers)
        {
            Console.WriteLine($"{header.Key}: {header.Value}");
        }

        await _next(context);
    }
}

public interface IUserMessage
{}
