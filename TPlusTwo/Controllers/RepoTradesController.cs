using Microsoft.AspNetCore.Mvc;
using TPlusTwo.UseCases.RepoTrades;

namespace TPlusTwo.Controllers;

//delegate
public delegate /*TODO: Unit<Error> */string HandleCreateRepoTradeCommand(
    CreateRepoTradeCommand cmd);


[ApiController] //ApiController causes any validation errors to be returned before hitting the action
[Route("[controller]")]
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

    [HttpPost]
    public void CreateRepoTrade2(HandleCreateRepoTradeCommand handleCreateRepoTradeCommand, CreateRepoTradeCommand cmd)
    {
        handleCreateRepoTradeCommand(cmd);
    }
}
