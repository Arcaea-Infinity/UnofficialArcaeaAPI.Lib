using ArcaeaUnlimitedAPI.Lib.Models;
using ArcaeaUnlimitedAPI.Lib.Utils;

namespace ArcaeaUnlimitedAPI.Lib.Core;

public class AuaAssetsApi
{
    private readonly HttpClient _client;

    public AuaAssetsApi(HttpClient client)
    {
        _client = client;
    }

    private async Task<byte[]> GetIcon(int partner, bool awakened)
    {
        var qb = new QueryBuilder()
            .Add("partner", partner.ToString())
            .Add("awakened", awakened.ToString());
        return await _client.GetByteArrayAsync("assets/icon" + qb.Build());
    }

    /// <summary>
    /// Get partner icon.
    /// </summary>
    /// <endpoint>/assets/icon</endpoint>
    /// <param name="partner">Partner ID</param>
    /// <param name="awakened">Partner awakened</param>
    /// <returns>Byte array represents the image</returns>
    public async Task<byte[]> Icon(int partner, bool awakened = false)
        => await GetIcon(partner, awakened);


    private async Task<byte[]> GetChar(int partner, bool awakened)
    {
        var qb = new QueryBuilder()
            .Add("partner", partner.ToString())
            .Add("awakened", awakened.ToString());
        return await _client.GetByteArrayAsync("assets/char" + qb.Build());
    }

    /// <summary>
    /// Get partner char image.
    /// </summary>
    /// <endpoint>/assets/char</endpoint>
    /// <param name="partner">Partner ID</param>
    /// <param name="awakened">Partner awakened</param>
    /// <returns>Byte array represents the image</returns>
    public async Task<byte[]> Char(int partner, bool awakened = false)
        => await GetChar(partner, awakened);


    private async Task<byte[]> GetSong(string songnameOrFilename, AuaSongQueryType queryType,
        ArcaeaDifficulty difficulty)
    {
        var qb = new QueryBuilder();

        qb.Add(queryType switch
        {
            AuaSongQueryType.SongId => "songid",
            AuaSongQueryType.FileName => "file",
            _ => "songname"
        }, songnameOrFilename);

        if (queryType != AuaSongQueryType.FileName)
            qb.Add("difficulty", ((int)difficulty).ToString());

        return await _client.GetByteArrayAsync("assets/song" + qb.Build());
    }

    /// <summary>
    /// Get song cover.
    /// </summary>
    /// <endpoint>/assets/song</endpoint>
    /// <param name="songnameOrFilename">Any song name for fuzzy querying, or sid in Arcaea songlist, or file name of the cover</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    public async Task<byte[]> Song(string songnameOrFilename, AuaSongQueryType queryType = AuaSongQueryType.SongName,
        ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future)
        => await GetSong(songnameOrFilename, queryType, difficulty);
    
    /// <summary>
    /// Get song cover.
    /// </summary>
    /// <endpoint>/assets/song</endpoint>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    public async Task<byte[]> Song(string songname, ArcaeaDifficulty difficulty)
        => await GetSong(songname, AuaSongQueryType.SongName, difficulty);
}