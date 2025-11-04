using CSharpFunctionalExtensions;
using TPlusTwo.Core.RepoTrades;

namespace TPlusTwo.Ports.RepoTrades;

public delegate UnitResult<IError> StoreRepoTrade(RepoTrade repoTrade);


