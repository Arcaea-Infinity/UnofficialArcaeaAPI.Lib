using ArcaeaUnlimitedAPI.Lib;
using ArcaeaUnlimitedAPI.Lib.Test;

string? apiUrl = null, userAgent = null, username = null;

while (apiUrl is null || userAgent is null || username is null)
{
    Console.Write("API Url: ");
    apiUrl = Console.ReadLine()?.Trim();
    Console.Write("Custom User Agent: ");
    userAgent = Console.ReadLine()?.Trim();
    Console.Write("Test User Name: ");
    username = Console.ReadLine()?.Trim();
}

var client = new AuaClient
{
    ApiUrl = apiUrl,
    UserAgent = userAgent
}.Initialize();

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Testing User...");
var userPassed = await TestUser.Test(client, username);

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Testing Song...");
var songPassed = await TestSong.Test(client);

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Testing Assets...");
var assetsPassed = await TestAssets.Test(client);

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Testing Common...");
var commonPassed = await TestCommon.Test(client);

Console.WriteLine("\n=============================");
Console.WriteLine("         Test Result         ");
Console.WriteLine("=============================");
Console.ForegroundColor = ConsoleColor.Gray;
Console.Write("User: ");
Utils.LogIfPassed(userPassed);
Console.Write("Song: ");
Utils.LogIfPassed(songPassed);
Console.Write("Assets: ");
Utils.LogIfPassed(assetsPassed);
Console.Write("Common: ");
Utils.LogIfPassed(commonPassed);
