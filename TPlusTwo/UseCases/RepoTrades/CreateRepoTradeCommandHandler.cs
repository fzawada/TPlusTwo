using CSharpFunctionalExtensions;
using TPlusTwo.Core.RepoTrades;
using TPlusTwo.Ports.RepoTrades;

namespace TPlusTwo.UseCases.RepoTrades;

public class CreateRepoTradeCommandHandler
{
    private readonly StoreRepoTrade storeRepoTrade;

    public CreateRepoTradeCommandHandler(StoreRepoTrade storeRepoTrade)
    {
        this.storeRepoTrade = storeRepoTrade;
    }

    public UnitResult<IError> HandleImpl(CreateRepoTradeCommand cmd)
    {
        var result = RepoTrade.Create(
            cmd.TradeDate,
            cmd.SettlementDate,
            cmd.Nominal,
            cmd.Instrument)
            .MapError(e => (IError)e)
            .Bind(x => storeRepoTrade(x));
        return result;
    }
}
