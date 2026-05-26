namespace WizCo.Core.Application.Results;

public class Result<T> : Result
{
    public T? Data { get; }

    private Result(bool success, T? data, List<string>? errors = null)
        : base(success, errors)
    {
        Data = data;
    }

    public static Result<T> Ok(T data) => new(true, data);

    public static new Result<T> Failure(string error) => new(false, default, [error]); 
}