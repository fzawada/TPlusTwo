using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
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
    public void CreateRepoTrade(CreateRepoTradeCommand cmd)
    {
        handleCreateRepoTradeCommand(cmd);
    }

    /*
    [HttpPost]
    public void CreateRepoTrade2(HandleCreateRepoTradeCommand handleCreateRepoTradeCommand, CreateRepoTradeCommand cmd)
    {
        handleCreateRepoTradeCommand(cmd);
    }*/
}
