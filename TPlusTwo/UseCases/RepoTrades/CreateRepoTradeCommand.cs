using System.ComponentModel.DataAnnotations;

namespace TPlusTwo.UseCases.RepoTrades;

public record class CreateRepoTradeCommand(
    DateOnly TradeDate,
    DateOnly SettlementDate,
    decimal Nominal,
    string Instrument);


