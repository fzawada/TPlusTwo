using System.Diagnostics.CodeAnalysis;
using TPlusTwo.Core.RepoTrades;

namespace TPlusTwo.UseCases.RepoTrades;

public class CreateRepoTradeCommand
{
    public required TradeDate TradeDate { get; init; }
    public required SettlementDate SettlementDate { get; init; }
    public required Nominal Nominal { get; init; }
    public required Instrument Instrument { get; init; }

    [SetsRequiredMembers]
    public CreateRepoTradeCommand(
        TradeDate tradeDate,
        SettlementDate settlementDate,
        Nominal nominal,
        Instrument instrument)
    {
        TradeDate = tradeDate;
        SettlementDate = settlementDate;
        Nominal = nominal;
        Instrument = instrument;
    }
}
