using ArcaeaUnlimitedAPI.Lib.Models;

namespace ArcaeaUnlimitedAPI.Lib.Test;

public static class TestCommon
{
    public static async Task<bool> Test(AuaClient client)
    {
        var passed = true;

        Utils.Test(await client.Update(), ref passed);
        Utils.Test(await client.Playdata("ifi", AuaSongQueryType.SongId, ArcaeaDifficulty.Present, 12.50, 13.00),
            ref passed);

        return passed;
    }
}