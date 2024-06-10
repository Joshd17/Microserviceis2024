 using MediatR;
 using MessageContracts;

 namespace UserService.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.UserName.Length > 20)
        {
            return CommandResult.Fail(new List<string> { "Invalid username" });
        }
        else
        {
            string path = $"/testfiles/{Guid.NewGuid().ToString()}-{request.XTenant}-{DateTime.UtcNow.Date:h:mm:ss}.json";
            await File.WriteAllTextAsync(path, request.UserName, cancellationToken);
            return CommandResult.Success();
        }
    }
}

public class CreateUserCommand : IRequest<CommandResult>
{
    public string UserName { get; init; }
    public string XTenant { get; init; }
}

