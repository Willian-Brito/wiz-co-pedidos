namespace WizCo.Core.Application.Results;

public class Result
{
    public bool Success { get; }
    public List<string> Errors { get; }

    protected Result(bool success, List<string>? errors = null)
    {
        Success = success;
        Errors = errors ?? [];
    }

    public static Result Ok() => new(true);

    public static Result Failure(string error) => new(false, [error]);
}