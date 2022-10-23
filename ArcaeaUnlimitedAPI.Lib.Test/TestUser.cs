using ArcaeaUnlimitedAPI.Lib.Models;

namespace ArcaeaUnlimitedAPI.Lib.Test;

public static class TestUser
{
    public static async Task<bool> Test(AuaClient client, string username)
    {
        var passed = true;

        Utils.Test(await client.User.Info(username, 7, AuaReplyWith.All), ref passed);
        Utils.Test(
            await client.User.Best(username, "quonwacca", AuaSongQueryType.SongId, ArcaeaDifficulty.Beyond,
                AuaReplyWith.All), ref passed);
        Utils.Test(await client.User.Best30(username, 10, AuaReplyWith.All), ref passed);

        return passed;
    }
}