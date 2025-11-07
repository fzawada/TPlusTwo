using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

using TPlusTwo.Core.RepoTrades;
using TPlusTwo.UseCases.RepoTrades;

namespace TPlusTwo.Controllers;

//delegate needed for easier use of IoC. Otherwise could be a simple Func<>
public delegate UnitResult<IError> HandleCreateRepoTradeCommand(
    CreateRepoTradeCommand cmd);

[ApiController]
[Route("[controller]/[action]")]
public class RepoTradesController : ControllerBase
{
    private readonly HandleCreateRepoTradeCommand handleCreateRepoTradeCommand;

    public RepoTradesController(HandleCreateRepoTradeCommand handleCreateRepoTradeCommand)
    {
        this.handleCreateRepoTradeCommand = handleCreateRepoTradeCommand;
    }

    [HttpPost]
    public void CreateRepoTrade(CreateRepoTradeCommandRaw cmdRaw)
    {
        var cmd =
        from tradeDate in TradeDate.TryFrom(cmdRaw.TradeDate).ToResult()
        from settlementDate in SettlementDate.TryFrom(cmdRaw.SettlementDate).ToResult()
        from nominal in Nominal.TryFrom(cmdRaw.Nominal).ToResult()
        from instrument in Instrument.TryFrom(cmdRaw.Instrument).ToResult()
        select new CreateRepoTradeCommand(tradeDate, settlementDate, nominal, instrument);

        var result = cmd.Bind(handleCreateRepoTradeCommand.Invoke);

        //TODO: handle errors
    }

    /*
    [HttpPost]
    public void CreateRepoTrade2(HandleCreateRepoTradeCommand handleCreateRepoTradeCommand, CreateRepoTradeCommand cmd)
    {
        handleCreateRepoTradeCommand(cmd);
    }*/

    /// <summary>
    /// Raw version is needed, because ASP.NET pipeline doesn't easily allow
    /// to collect validation errors (coming from VOs creation)
    /// into ModelState during JSON deserialization / model binding
    /// </summary>
    public record class CreateRepoTradeCommandRaw
    {
        public required DateOnly TradeDate { get; init; }
        public required DateOnly SettlementDate { get; init; }
        public required decimal Nominal { get; init; }
        public required string Instrument { get; init; }
    }
}
