using Vogen;

namespace TPlusTwo.Core.RepoTrades;

[ValueObject<int>]
public readonly partial record struct RepoTradeVersion
{
    public static RepoTradeVersion TransientNew = From(-1);
    private static Validation Validate(int input)
    {
        bool isValid = input >= -1;
        return isValid ? Validation.Ok : Validation.Invalid("Version cannot be lower than -1");
    }
}
