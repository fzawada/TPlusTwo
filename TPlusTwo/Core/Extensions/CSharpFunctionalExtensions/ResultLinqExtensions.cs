using CSharpFunctionalExtensions;

namespace TPlusTwo.Core.Extensions.CSharpFunctionalExtensions;

public static class ResultLinqExtensions
{
    // Functor map
    public static Result<TResult, IError> Select<T, TResult>(
        this Result<T, IError> source,
        Func<T, TResult> selector)
    {
        if (source.IsSuccess) return Result.Success<TResult, IError>(selector(source.Value));
        return Result.Failure<TResult, IError>(source.Error);
    }

    // Applicative SelectMany that *accumulates* errors across independent computations.
    // IMPORTANT: This assumes 'bind' does NOT actually need the left value (typical for independent validations).
    public static Result<TResult, IError> SelectMany<TLeft, TRight, TResult>(
        this Result<TLeft, IError> left,
        Func<TLeft, Result<TRight, IError>> bind,
        Func<TLeft, TRight, TResult> project)
    {
        // Force the right computation even if left failed (to accumulate all errors).
        // Since later clauses don't depend on earlier values, we safely pass default!
        var right = bind(left.IsSuccess ? left.Value : default!);

        var errors = new List<IError>(2);

        if (left.IsFailure)
        {
            errors.Add(left.Error);
        }

        if (right.IsFailure)
        {
            errors.Add(right.Error);
        }

        return
            errors.Count == 2 ? Result.Failure<TResult, IError>(new AggregateError(errors))
            : errors.Count == 1 ? Result.Failure<TResult, IError>(errors.First())
            : Result.Success<TResult, IError>(project(left.Value, right.Value));
    }
}