using CSharpFunctionalExtensions;
using TPlusTwo.Controllers;
using TPlusTwo.Ports.RepoTrades;
using TPlusTwo.UseCases.RepoTrades;

namespace TPlusTwo.TypesRegistration;

public static class Registrar
{
    public static void RegisterTypes(IServiceCollection svcs)
    {
        svcs.AddTransient<StoreRepoTrade>(x => repo => UnitResult.Success<IError>());
        svcs.AddTransient<CreateRepoTradeCommandHandler>();
        svcs.AddTransient<HandleCreateRepoTradeCommand>(prov =>
            prov.GetRequiredService<CreateRepoTradeCommandHandler>().HandleImpl);
    }
}
