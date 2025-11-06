using TPlusTwo.Core.RepoTrades;
// If you're using Vogen's exception type from the Vogen package:
using Vogen; // for ValueObjectValidationException

namespace TPlusTwo.Tests.Core.RepoTrades;

[TestFixture]
public class RepoTradeIdTests
{
    [Test]
    public void TryFrom_EmptyGuid_ReturnsInvalid()
    {
        var result = RepoTradeId.TryFrom(Guid.Empty);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error.ErrorMessage, Is.EqualTo("RepoTradeId cannot be empty"));
    }

    [Test]
    public void TryFrom_Version4Guid_ReturnsInvalid()
    {
        var v4 = Guid.NewGuid(); // version 4 by default
        var result = RepoTradeId.TryFrom(v4);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error.ErrorMessage, Is.EqualTo("RepoTradeId must be UUIDv7"));
    }

    [Test]
    public void TryFrom_Version7Guid_ReturnsValid()
    {
        var v7 = Guid.CreateVersion7();
        var result = RepoTradeId.TryFrom(v7);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.ValueObject, Is.EqualTo(v7));
    }
}