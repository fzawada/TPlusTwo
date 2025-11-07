using CSharpFunctionalExtensions;
using Vogen;

namespace TPlusTwo.Core.Extensions.VogenExt;

public static class ValueObjectOrErrorToResultExtension
{
    public static Result<T, IError> ToResult<T>(this ValueObjectOrError<T> voOrError)
    {
        return voOrError.IsSuccess
            ? Result.Success<T, IError>(voOrError.ValueObject)
            : Result.Failure<T, IError>(ValidationError.Create(voOrError.Error.ErrorMessage));
    }
}