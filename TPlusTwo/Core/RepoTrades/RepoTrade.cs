using CSharpFunctionalExtensions;

namespace TPlusTwo.Core.RepoTrades;

public sealed partial record class RepoTrade
{
    public RepoTradeId Id { get; private init; }
    public RepoTradeVersion Version { get; private init; }
    public DateOnly TradeDate { get; private init; }
    public DateOnly SettlementDate { get; private init; }
    public decimal Nominal { get; private init; }
    public string Instrument { get; private init; }

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
            Id = id ?? Id,
            Version = version ?? Version,
            TradeDate = tradeDate ?? TradeDate,
            SettlementDate = settlementDate ?? SettlementDate,
            Nominal = nominal ?? Nominal,
            Instrument = instrument ?? Instrument
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