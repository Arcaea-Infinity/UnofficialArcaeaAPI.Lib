# ArcaeaUnlimitedAPI.Lib

A Library for ArcaeaUnlimitedAPI with C#.

## Usage
```csharp
using ArcaeaUnlimitedAPI.Lib;
using ArcaeaUnlimitedAPI.Lib.Models;

var client = new AuaClient
{
    ApiUrl = "<API Url>",
    UserAgent = "<Custom User-Agent>"
}.Initialize();

// Query best 30 of Nagiha0798
var best30 = await client.User.Best30("Nagiha0798",
    10,                      // Overflow count
    AuaReplyWith.SongInfo);  // Reply with songinfo

Console.WriteLine(best30.AccountInfo.Rating);
Console.WriteLine(best30.Best30List[0].SongId);


// Query songinfo of #1f1e33
var songinfo = await client.Song.Info("ifi", AuaSongQueryType.SongId);

Console.WriteLine(songinfo.SongId);
Console.WriteLine(songinfo.Difficulties[2].NameEn);
```

## License
This project is under [616SB License](./LICENSE).
