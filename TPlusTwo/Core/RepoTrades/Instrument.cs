using Vogen;

namespace TPlusTwo.Core.RepoTrades;

[ValueObject<string>]
public readonly partial record struct Instrument
{
    private static Validation Validate(string input) =>
        string.IsNullOrWhiteSpace(input)
            ? Validation.Invalid("Instrument cannot be empty")
            : Validation.Ok;
}
