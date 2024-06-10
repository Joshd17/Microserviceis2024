using MassTransit;
using MessageContracts;

namespace UserService.Handlers;

public class UserMessageConsumer : IConsumer<IMiddlewareMessage2>
{
    public async Task Consume(ConsumeContext<IMiddlewareMessage2> context)
    {
        string path = $"/testfiles/{Guid.NewGuid().ToString()}-{context.Message.XTenant}-{DateTime.UtcNow.Date:h:mm:ss}.json";
        await File.WriteAllTextAsync(path, context.Message.Value);
    }
}