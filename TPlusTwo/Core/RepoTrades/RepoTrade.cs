using CSharpFunctionalExtensions;

namespace TPlusTwo.Core.RepoTrades;

public partial record class RepoTrade
{
    public RepoTradeId Id { get; private set; }
    public RepoTradeVersion Version { get; private set; }
    public DateOnly TradeDate { get; private set; }
    public DateOnly SettlementDate { get; private set; }
    public decimal Nominal { get; private set; }
    public string Instrument { get; private set; }

    private RepoTrade(
        RepoTradeId id,
        RepoTradeVersion version,
        DateOnly tradeDate,
        DateOnly settlementDate,
        decimal nominal,
        string instrument)
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
        DateOnly tradeDate,
        DateOnly settlementDate,
        decimal nominal,
        string instrument) =>
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
        DateOnly? tradeDate = null,
        DateOnly? settlementDate = null,
        decimal? nominal = null,
        string? instrument = null) =>
        (this with
        {
            Id = id ?? this.Id,
            Version = version ?? this.Version,
            TradeDate = tradeDate ?? this.TradeDate,
            SettlementDate = settlementDate ?? this.SettlementDate,
            Nominal = nominal ?? this.Nominal,
            Instrument = instrument ?? this.Instrument
        }).EnsureInvariants();

    public Result<RepoTrade, IValidationError> EnsureInvariants()
    {
        if (SettlementDate < TradeDate)
        {
            return Result.Failure<RepoTrade, IValidationError>(
                SettlementBeforeTradeDateError.From(this));
        }
        return this;
    }
}