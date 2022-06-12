using ArcaeaUnlimitedAPI.Lib.Models;

namespace ArcaeaUnlimitedAPI.Lib.Test;

public static class TestAssets
{
    public static async Task<bool> Test(AuaClient client)
    {
        var passed = true;

        var iconImg = await client.Assets.Icon(1, true);
        var charImg = await client.Assets.Char(1, true);
        var songImg = await client.Assets.Song("ifi", AuaSongQueryType.SongId);
        
        await File.WriteAllBytesAsync("Test.Icon.png", iconImg);
        await File.WriteAllBytesAsync("Test.Char.png", charImg);
        await File.WriteAllBytesAsync("Test.Song.jpg", songImg);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("- Image wrote.");

        return passed;
    }
}