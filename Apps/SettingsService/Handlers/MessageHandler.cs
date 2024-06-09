using MassTransit;

namespace SettingsService.Handlers;

public class YourMessageConsumer : IConsumer<IMessage>
{
    public async Task Consume(ConsumeContext<IMessage> context)
    {
        Console.WriteLine($"Received: {context.Message.Value}");
        //TODO: Output to file
    }
}

public interface IMessage
{
    public string Value { get; set; }
}