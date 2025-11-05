using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using TPlusTwo.Controllers;
using TPlusTwo.Ports.RepoTrades;
using TPlusTwo.UseCases.RepoTrades;

namespace TPlusTwo;

public static class TypesRegistration
{
    public static void RegisterTypes(IServiceCollection svcs)
    {
        svcs.AddTransient<StoreRepoTrade>(x => repo => UnitResult.Success<IError>());
        svcs.AddTransient<CreateRepoTradeCommandHandler>();
        svcs.AddTransient<HandleCreateRepoTradeCommand>(prov =>
            prov.GetRequiredService<CreateRepoTradeCommandHandler>().Handle);
    }
}
