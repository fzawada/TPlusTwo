using Vogen;

namespace TPlusTwo.Core.RepoTrades;

[ValueObject<DateOnly>]
public readonly partial record struct SettlementDate
{
    private static Validation Validate(DateOnly input) =>
        input == DateOnly.MinValue
            ? Validation.Invalid("SettlementDate cannot be " + input)
            : Validation.Ok;
}
