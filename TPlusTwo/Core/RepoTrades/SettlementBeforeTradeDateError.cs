namespace TPlusTwo.Core.RepoTrades;

public class SettlementBeforeTradeDateError : IValidationError
{
    private readonly DateOnly tradeDate;
    private readonly DateOnly settlementDate;

    private SettlementBeforeTradeDateError(RepoTrade rt)
    {
        tradeDate = rt.TradeDate;
        settlementDate = rt.SettlementDate;
    }

    public string Message =>
        $"Settlement date {settlementDate:yyyy-MM-dd} cannot be before trade date {tradeDate:yyyy-MM-dd}";

    internal static SettlementBeforeTradeDateError From(RepoTrade rt) =>
        new SettlementBeforeTradeDateError(rt);
}