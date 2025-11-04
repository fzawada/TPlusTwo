using CSharpFunctionalExtensions;
using TPlusTwo.Core.RepoTrades;
using TPlusTwo.Ports.RepoTrades;

namespace TPlusTwo.UseCases.RepoTrades;

//delegate needed for easier use of IoC. Otherwise could be a simple Func<>
public delegate UnitResult<IError> HandleCreateRepoTradeCommand(
    CreateRepoTradeCommand cmd);

public static class RepoTrades
{
    public static UnitResult<IError> Handle(StoreRepoTrade storeRepoTrade, CreateRepoTradeCommand cmd)
    {
        //todo: validation via transformation to Value Objects

        //RepoTradeId.From()


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
