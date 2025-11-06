using Vogen;

namespace TPlusTwo.Core.RepoTrades;

[ValueObject<Guid>]
public readonly partial record struct RepoTradeId
{
    private static Validation Validate(Guid input)
    {
        return
            input == Guid.Empty ? Validation.Invalid("RepoTradeId cannot be empty")
            : !IsUuidV7(input) ? Validation.Invalid("RepoTradeId must be UUIDv7")
            : Validation.Ok;
    }

    private static bool IsUuidV7(Guid guid)
    {
        var bytes = guid.ToByteArray();

        // Version is stored in the upper 4 bits of byte 7 (0-based index)
        int version = (bytes[7] >> 4) & 0x0F;

        return version == 7;
    }
}
