namespace TPlusTwo.Core;

public interface IError
{
    string Message { get; }
}

public interface IValidationError : IError
{
}

public class ValidationError : IValidationError
{
    public required string Message { get; init; }

    public static ValidationError Create(string message) =>
        new() { Message = message };
}

public sealed class AggregateError : IError
{
    public IReadOnlyList<IError> Errors { get; private init; }
    public string Message => string.Join("; ", Errors.Select(e => e.Message));

    public AggregateError(IReadOnlyList<IError> errors)
    {
        Errors = errors
            .SelectMany(e => e is AggregateError ae ? ae.Errors : [e])
            .ToList();
    }
}