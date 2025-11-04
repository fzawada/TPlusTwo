using Vogen;

namespace TPlusTwo.Core.RepoTrades;

[ValueObject<Guid>]
public readonly partial record struct RepoTradeId
{
    private static Validation Validate(Guid input)
    {
        bool isValid = input != Guid.Empty;

        return isValid
            ? Validation.Ok
            : Validation.Invalid("RepoTradeId cannot be empty");
    }
}
