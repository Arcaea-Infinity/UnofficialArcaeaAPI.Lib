using UnofficialArcaeaAPI.Lib.Models;

namespace UnofficialArcaeaAPI.Lib.Tests;

public class TestUserApi : TestBase
{
    [Fact]
    public async Task TestInfo()
    {
        var userInfo = await DefaultClient.User.GetInfoAsync("ToasterKoishi", 1, UaaReplyWith.All);

        Assert.True(userInfo.AccountInfo.Rating > 1200);

        Assert.NotNull(userInfo.RecentScore);
        Assert.Single(userInfo.RecentScore);

        Assert.NotNull(userInfo.SongInfo);
    }

    [Fact]
    public async Task TestBestsSession()
    {
        var sessionInfo = await DefaultClient.User.GetBestsSessionAsync("ToasterKoishi");
        
        Assert.True(sessionInfo.SessionInfo.Contains('@'));
        Assert.True(Guid.TryParse(sessionInfo.SessionInfo[2..], out _));
    }
}