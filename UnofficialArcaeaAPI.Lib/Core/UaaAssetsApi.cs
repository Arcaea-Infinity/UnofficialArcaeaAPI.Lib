﻿using System.Net;
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

    #region /assets/icon

    private async Task<byte[]> GetIconAsyncCore(int partner, bool awakened)
    {
        var qb = new QueryBuilder()
            .Add("partner", partner.ToString())
            .Add("awakened", awakened.ToString());
        var resp = await _client.GetAsync("assets/icon" + qb.Build());
        return await resp.EnsureDataSuccess();
    }

    /// <summary>
    /// Get partner icon.
    /// </summary>
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
        return await resp.EnsureDataSuccess();
    }

    /// <summary>
    /// Get partner char image.
    /// </summary>
    /// <param name="partner">Partner ID</param>
    /// <param name="awakened">Partner awakened</param>
    /// <returns>Byte array represents the image</returns>
    public Task<byte[]> GetCharAsync(int partner, bool awakened = false)
        => GetCharAsyncCore(partner, awakened);

    #endregion /assets/char

    #region /assets/song

    private async Task<byte[]> GetSongAsyncCore(string songNameOrFileName, UaaSongQueryType queryType,
        ArcaeaDifficulty difficulty)
    {
        var qb = new QueryBuilder();

        qb.Add(queryType switch
        {
            UaaSongQueryType.SongId => "song_id",
            UaaSongQueryType.FileName => "file",
            _ => "song_name"
        }, songNameOrFileName);

        if (queryType != UaaSongQueryType.FileName)
            qb.Add("difficulty", ((int)difficulty).ToString());

        var resp = await _client.GetAsync("assets/song" + qb.Build());
        return await resp.EnsureDataSuccess();
    }

    /// <summary>
    /// Get song cover.
    /// </summary>
    /// <param name="songnameOrFilename">Any song name for fuzzy querying, or sid in Arcaea songlist, or file name of the cover</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    public Task<byte[]> GetSongAsync(string songnameOrFilename, UaaSongQueryType queryType = UaaSongQueryType.SongName,
        ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future)
        => GetSongAsyncCore(songnameOrFilename, queryType, difficulty);

    /// <summary>
    /// Get song cover.
    /// </summary>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    public Task<byte[]> GetSongAsync(string songName, ArcaeaDifficulty difficulty)
        => GetSongAsyncCore(songName, UaaSongQueryType.SongName, difficulty);

    #endregion /assets/song

    #region /assets/aff

    private async Task<string> GetAffAsyncCore(string songName, UaaSongQueryType queryType,
        ArcaeaDifficulty difficulty)
    {
        var qb = new QueryBuilder();

        qb.Add(queryType == UaaSongQueryType.SongId ? "song_id" : "song_name", songName);

        if (queryType != UaaSongQueryType.FileName)
            qb.Add("difficulty", ((int)difficulty).ToString());

        var resp = await _client.GetAsync("assets/aff" + qb.Build());
        if (resp.StatusCode != HttpStatusCode.OK)
        {
            var errJson = JsonSerializer.Deserialize<UaaResponse>(await resp.Content.ReadAsStringAsync())!;
            throw new UaaRequestException(errJson.Status, errJson.Message!);
        }

        return await resp.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Get chart aff.
    /// </summary>
    /// <param name="songName">Any song name for fuzzy querying, or sid in Arcaea songlist.</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    /// <remarks>It is not recommended to use this API frequently, and this API only returns affs from the installation package.</remarks>
    public Task<string> GetAffAsync(string songName, UaaSongQueryType queryType = UaaSongQueryType.SongName,
        ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future)
        => GetAffAsyncCore(songName, queryType, difficulty);

    /// <summary>
    /// Get chart aff.
    /// </summary>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    /// <remarks>It is not recommended to use this API frequently, and this API only returns affs from the installation package.</remarks>
    public Task<string> GetAffAsync(string songName, ArcaeaDifficulty difficulty)
        => GetAffAsyncCore(songName, UaaSongQueryType.SongName, difficulty);

    #endregion /assets/aff

    #region /assets/preview

    private async Task<byte[]> GetPreviewAsyncCore(string songName, UaaSongQueryType queryType,
        ArcaeaDifficulty difficulty)
    {
        var qb = new QueryBuilder();

        qb.Add(queryType == UaaSongQueryType.SongId ? "song_id" : "song_name", songName);

        if (queryType != UaaSongQueryType.FileName)
            qb.Add("difficulty", ((int)difficulty).ToString());

        var resp = await _client.GetAsync("assets/preview" + qb.Build());
        return await resp.EnsureDataSuccess();
    }

    /// <summary>
    /// Get chart preview.
    /// </summary>
    /// <param name="songName">Any song name for fuzzy querying, or sid in Arcaea songlist.</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    /// <remarks>This API is not available for release version of AUA. It is provided by AffPreview.</remarks>
    public Task<byte[]> GetPreviewAsync(string songName, UaaSongQueryType queryType = UaaSongQueryType.SongName,
        ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future)
        => GetPreviewAsyncCore(songName, queryType, difficulty);

    /// <summary>
    /// Get chart preview.
    /// </summary>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>Byte array represents the image</returns>
    /// <remarks>This API is not available for release version of AUA. It is provided by AffPreview.</remarks>
    public Task<byte[]> GetPreviewAsync(string songName, ArcaeaDifficulty difficulty)
        => GetPreviewAsyncCore(songName, UaaSongQueryType.SongName, difficulty);

    #endregion /assets/preview
}