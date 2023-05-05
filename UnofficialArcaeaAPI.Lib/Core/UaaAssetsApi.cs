using System.Net;
using System.Text.Json;
using UnofficialArcaeaAPI.Lib.Models;
using UnofficialArcaeaAPI.Lib.Responses;
using UnofficialArcaeaAPI.Lib.Utils;

namespace UnofficialArcaeaAPI.Lib.Core;

public sealed class UaaAssetsApi
{
    private readonly HttpClient _client;

    internal UaaAssetsApi(HttpClient client)
    {
        _client = client;
    }

    private static async Task<byte[]> EnsureSuccess(HttpResponseMessage resp)
    {
        if (resp.StatusCode != HttpStatusCode.OK)
        {
            var errJson = JsonSerializer.Deserialize<UaaResponse>(await resp.Content.ReadAsStringAsync())!;
            throw new UaaRequestException(errJson.Status, errJson.Message!);
        }

        return await resp.Content.ReadAsByteArrayAsync();
    }

    #region /assets/icon

    private async Task<byte[]> GetIconAsyncCore(int partner, bool awakened)
    {
        var qb = new QueryBuilder()
            .Add("partner", partner.ToString())
            .Add("awakened", awakened.ToString());
        var resp = await _client.GetAsync("assets/icon" + qb.Build());
        return await EnsureSuccess(resp);
    }

    /// <summary>
    /// Get partner icon.
    /// </summary>
    /// <endpoint>/assets/icon</endpoint>
    /// <param name="partner">Partner ID</param>
    /// <param name="awakened">Partner awakened</param>
    /// <returns>Byte array represents the image</returns>
    public Task<byte[]> GetIconAsync(int partner, bool awakened = false)
        => GetIconAsyncCore(partner, awakened);

    #endregion /assets/icon

    #region /assets/char

    private async Task<byte[]> GetCharAsyncCore(int partner, bool awakened)
    {
        var qb = new QueryBuilder()
            .Add("partner", partner.ToString())
            .Add("awakened", awakened.ToString());
        var resp = await _client.GetAsync("assets/char" + qb.Build());
        return await EnsureSuccess(resp);
    }

    /// <summary>
    /// Get partner char image.
    /// </summary>
    /// <endpoint>/assets/char</endpoint>
    /// <param name="partner">Partner ID</param>
    /// <param name="awakened">Partner awakened</param>
    /// <returns>Byte array represents the image</returns>
    public Task<byte[]> GetCharAsync(int partner, bool awakened = false)
        => GetCharAsyncCore(partner, awakened);

    #endregion /assets/char

    #region /assets/song

    private async Task<byte[]> GetSongAsyncCore(string songnameOrFilename, AuaSongQueryType queryType,
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

        var resp = await _client.GetAsync("assets/song" + qb.Build());
        return await EnsureSuccess(resp);
    }

    /// <summary>
    /// Get song cover.
    /// </summary>
    /// <endpoint>/assets/song</endpoint>
    /// <param name="songnameOrFilename">Any song name for fuzzy querying, or sid in Arcaea songlist, or file name of the cover</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    public Task<byte[]> GetSongAsync(string songnameOrFilename, AuaSongQueryType queryType = AuaSongQueryType.SongName,
        ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future)
        => GetSongAsyncCore(songnameOrFilename, queryType, difficulty);

    /// <summary>
    /// Get song cover.
    /// </summary>
    /// <endpoint>/assets/song</endpoint>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    public Task<byte[]> GetSongAsync(string songname, ArcaeaDifficulty difficulty)
        => GetSongAsyncCore(songname, AuaSongQueryType.SongName, difficulty);

    #endregion /assets/song

    #region /assets/preview

    private async Task<byte[]> GetPreviewAsyncCore(string songname, AuaSongQueryType queryType,
        ArcaeaDifficulty difficulty)
    {
        var qb = new QueryBuilder();

        qb.Add(queryType == AuaSongQueryType.SongId ? "songid" : "songname", songname);

        if (queryType != AuaSongQueryType.FileName)
            qb.Add("difficulty", ((int)difficulty).ToString());

        var resp = await _client.GetAsync("assets/preview" + qb.Build());
        return await EnsureSuccess(resp);
    }

    /// <summary>
    /// Get chart preview.
    /// </summary>
    /// <endpoint>/assets/preview</endpoint>
    /// <param name="songname">Any song name for fuzzy querying, or sid in Arcaea songlist.</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    /// <remarks>This API is not available for release version of AUA. It is provided by AffPreview.</remarks>
    public Task<byte[]> GetPreviewAsync(string songname, AuaSongQueryType queryType = AuaSongQueryType.SongName,
        ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future)
        => GetPreviewAsyncCore(songname, queryType, difficulty);

    /// <summary>
    /// Get chart preview.
    /// </summary>
    /// <endpoint>/assets/preview</endpoint>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    /// <remarks>This API is not available for release version of AUA. It is provided by AffPreview.</remarks>
    public Task<byte[]> GetPreviewAsync(string songname, ArcaeaDifficulty difficulty)
        => GetPreviewAsyncCore(songname, AuaSongQueryType.SongName, difficulty);

    #endregion /assets/preview
}