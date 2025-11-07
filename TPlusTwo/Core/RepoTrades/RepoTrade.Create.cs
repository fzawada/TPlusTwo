using CSharpFunctionalExtensions;

namespace TPlusTwo.Core.RepoTrades;

public partial record class RepoTrade
{
    public static Result<RepoTrade, IValidationError> Create(
        TradeDate tradeDate,
        SettlementDate settlementDate,
        Nominal nominal,
        Instrument instrument) =>
        Parse(
            RepoTradeId.From(Guid.CreateVersion7()),
            RepoTradeVersion.TransientNew,
            tradeDate,
            settlementDate,
            nominal,
            instrument);
}