namespace TPlusTwo.Core;

public interface IError
{
    string Message { get; }
}

public interface IValidationError : IError
{
}