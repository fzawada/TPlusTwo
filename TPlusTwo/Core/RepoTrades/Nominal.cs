using Vogen;

namespace TPlusTwo.Core.RepoTrades;

[ValueObject<decimal>]
public readonly partial record struct Nominal
{
    private static Validation Validate(decimal input) =>
        input <= 0
            ? Validation.Invalid("Nominal must be bigger than 0")
            : Validation.Ok;
}
