# UnofficialArcaeaAPI.Lib

[![NuGet](https://img.shields.io/nuget/vpre/UnofficialArcaeaAPI.Lib?label=NuGet)](https://www.nuget.org/packages/UnofficialArcaeaAPI.Lib/)

A wrapper for UnofficialArcaeaAPI with C#.

## Install

```shell
dotnet add package UnofficialArcaeaAPI.Lib
```

## Usage

```csharp
using UnofficialArcaeaAPI.Lib;
using UnofficialArcaeaAPI.Lib.Models;

var client = new UaaClient(new UaaClientOptions
{
    ApiUrl = "<API Url>",
    Token = "<Bearer Token>",
    
    // Or if you want
    UserAgent = "<Custom User-Agent>"
});

// Query bests session of Nagiha0798
var best30 = await client.User.GetBestsSessionAsync("Nagiha0798");

// Query songinfo of #1f1e33
var songinfo = await client.Song.GetInfoAsync("ifi", AuaSongQueryType.SongId);

Console.WriteLine(songinfo.SongId);
Console.WriteLine(songinfo.Difficulties[2].NameEn);
```

## Supported endpoints

- [x] [user/info](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/user/info.md)
- [x] [user/best](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/user/best.md)
- [x] [user/bests/session](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/user/bests/session.md)
- [x] [user/bests/result](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/user/bests/result.md)
- [x] [song/info](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/song/info.md)
- [x] [song/list](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/song/list.md)
- [x] [song/alias](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/song/alias.md)
- [x] [song/random](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/song/random.md)
- [x] [assets/icon](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/assets/icon.md)
- [x] [assets/char](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/assets/char.md)
- [x] [assets/song](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/assets/song.md)
- [x] [assets/aff](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/assets/aff.md)
- [x] [assets/preview](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/assets/preview.md)
- [x] [data/update](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/data/update.md)
- [x] [data/theory](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/data/theory.md)
- [x] [data/challenge](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/data/challenge.md)
- [x] [data/cert](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/data/cert.md)
- [ ] [image/user/*](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs/blob/main/image/user.md)

## License

This project is under [616SB License](./LICENSE).
