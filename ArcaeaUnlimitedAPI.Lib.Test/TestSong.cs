using ArcaeaUnlimitedAPI.Lib.Models;

namespace ArcaeaUnlimitedAPI.Lib.Test;

public static class TestSong
{
    public static async Task<bool> Test(AuaClient client)
    {
        var passed = true;

        Utils.Test(await client.Song.Info("ifi", AuaSongQueryType.SongId), ref passed);
        Utils.Test(await client.Song.Alias("ifi", AuaSongQueryType.SongId), ref passed);
        Utils.Test(await client.Song.Random(10.0, 10.0, AuaReplyWith.All), ref passed);

        return passed;
    }
}