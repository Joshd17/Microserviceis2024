using MediatR;
using MessageContracts;

namespace SettingsService.Handlers;


public class CreateSettingCommandHandler : IRequestHandler<CreateSettingCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateSettingCommand request, CancellationToken cancellationToken)
    {
        if (request.Setting.Length > 20)
        {
            return CommandResult.Fail(new List<string> { "Invalid username" });
        }
        else
        {
            string path = $"/testfiles/{Guid.NewGuid().ToString()}-{request.XTenant}-{DateTime.UtcNow.Date:h:mm:ss}.json";
            await File.WriteAllTextAsync(path, request.Setting, cancellationToken);
            return CommandResult.Success();
        }
    }
}

public class CreateSettingCommand : IRequest<CommandResult>
{
    public string Setting { get; init; }
    public string XTenant { get; init; }
}

