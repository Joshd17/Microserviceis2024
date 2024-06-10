using System.Collections.Immutable;

namespace MessageContracts;
public record CommandResult
{
    public bool IsSuccess { get; private set; }
    public IReadOnlyList<string> Errors => _errors.ToImmutableList();
    private IEnumerable<string> _errors { get; set; } = new List<string>();
    private CommandResult(bool isSuccess, IEnumerable<string>? errors = null)
    {
        IsSuccess = isSuccess;
        _errors = errors ?? new List<string>();
    }
    

    public static CommandResult Fail(IEnumerable<string> errors)
    {
        return new CommandResult(false, errors);
    }
    
    public static CommandResult Success()
    {
        return new CommandResult(true);
    }
}
