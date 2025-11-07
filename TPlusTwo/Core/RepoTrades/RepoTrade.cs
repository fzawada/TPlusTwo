using CSharpFunctionalExtensions;

namespace TPlusTwo.Core.RepoTrades;

public sealed partial record class RepoTrade
{
    public RepoTradeId Id { get; private init; }
    public RepoTradeVersion Version { get; private init; }
    public TradeDate TradeDate { get; private init; }
    public SettlementDate SettlementDate { get; private init; }
    public Nominal Nominal { get; private init; }
    public Instrument Instrument { get; private init; }

    private RepoTrade(
        RepoTradeId id,
        RepoTradeVersion version,
        TradeDate tradeDate,
        SettlementDate settlementDate,
        Nominal nominal,
        Instrument instrument)
    {
        Id = id;
        Version = version;
        TradeDate = tradeDate;
        SettlementDate = settlementDate;
        Nominal = nominal;
        Instrument = instrument;
    }

    public static Result<RepoTrade, IValidationError> Parse(
        RepoTradeId id,
        RepoTradeVersion version,
        TradeDate tradeDate,
        SettlementDate settlementDate,
        Nominal nominal,
        Instrument instrument) =>
        new RepoTrade(
            id,
            version,
            tradeDate,
            settlementDate,
            nominal,
            instrument)
            .EnsureInvariants();

    public Result<RepoTrade, IValidationError> With(
        RepoTradeId? id = null,
        RepoTradeVersion? version = null,
        TradeDate? tradeDate = null,
        SettlementDate? settlementDate = null,
        Nominal? nominal = null,
        Instrument? instrument = null) =>
        (this with
        {
            Id = id ?? Id,
            Version = version ?? Version,
            TradeDate = tradeDate ?? TradeDate,
            SettlementDate = settlementDate ?? SettlementDate,
            Nominal = nominal ?? Nominal,
            Instrument = instrument ?? Instrument
        }).EnsureInvariants();

    public Result<RepoTrade, IValidationError> EnsureInvariants()
    {
        if (SettlementDate.Value < TradeDate.Value)
        {
            return Result.Failure<RepoTrade, IValidationError>(
                SettlementBeforeTradeDateError.From(this));
        }
        return this;
    }
}