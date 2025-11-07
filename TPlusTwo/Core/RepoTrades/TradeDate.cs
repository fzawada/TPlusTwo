using Vogen;

namespace TPlusTwo.Core.RepoTrades;

[ValueObject<DateOnly>]
public readonly partial record struct TradeDate
{
    private static Validation Validate(DateOnly input) =>
        input == DateOnly.MinValue
            ? Validation.Invalid("TradeDate cannot be " + input)
            : Validation.Ok;
}
