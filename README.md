# ArcaeaUnlimitedAPI.Lib

[![NuGet](https://img.shields.io/nuget/vpre/ArcaeaUnlimitedAPI.Lib?label=NuGet)](https://www.nuget.org/packages/ArcaeaUnlimitedAPI.Lib/)

A wrapper for ArcaeaUnlimitedAPI with C#.

## Install

```shell
dotnet add package ArcaeaUnlimitedAPI.Lib
```

## Usage

```csharp
using ArcaeaUnlimitedAPI.Lib;
using ArcaeaUnlimitedAPI.Lib.Models;

var client = new AuaClient
{
    ApiUrl = "<API Url>",
    Token = "<Bearer Token>",
    
    // Or if you want
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

## Supported endpoints

- [x] [user/info](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/user/info.md)
- [x] [user/best](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/user/best.md)
- [x] [user/best30](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/user/best30.md)
- [x] [song/info](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/song/info.md)
- [x] [song/list](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/song/list.md)
- [x] [song/alias](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/song/alias.md)
- [x] [song/random](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/song/random.md)
- [x] [assets/icon](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/assets/icon.md)
- [x] [assets/char](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/assets/char.md)
- [x] [assets/song](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/assets/song.md)
- [x] [assets/preview](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/assets/preview.md)
- [x] [data/update](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/data/update.md)
- [x] [data/theory](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/data/theory.md)
- [x] [data/playdata](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/data/playdata.md)
- [x] [data/density](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/data/density.md)
- [x] [data/challenge](https://github.com/Arcaea-Infinity/ArcaeaUnlimitedAPI-Wiki/blob/main/data/challenge.md)

## License

This project is under [616SB License](./LICENSE).
