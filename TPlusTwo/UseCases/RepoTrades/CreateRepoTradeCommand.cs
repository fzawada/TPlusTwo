namespace TPlusTwo.UseCases.RepoTrades;

public record class CreateRepoTradeCommand
{
    public required DateOnly TradeDate { get; init; }
    public required DateOnly SettlementDate { get; init; }
    public required decimal Nominal { get; init; }
    public required string Instrument { get; init; }
}
