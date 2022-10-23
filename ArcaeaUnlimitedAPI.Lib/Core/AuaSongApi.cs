using System.Text.Json;
using ArcaeaUnlimitedAPI.Lib.Models;
using ArcaeaUnlimitedAPI.Lib.Responses;
using ArcaeaUnlimitedAPI.Lib.Utils;

namespace ArcaeaUnlimitedAPI.Lib.Core;

public class AuaSongApi
{
    private readonly HttpClient _client;

    public AuaSongApi(HttpClient client)
    {
        _client = client;
    }

    private async Task<AuaSongInfoContent> GetInfo(string songname, AuaSongQueryType queryType)
    {
        var qb = new QueryBuilder()
            .Add(queryType == AuaSongQueryType.SongId ? "songid" : "songname", songname);

        var response = JsonSerializer.Deserialize<AuaResponse<AuaSongInfoContent>>(
            await _client.GetStringAsync("song/info" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get information of a song.
    /// </summary>
    /// <endpoint>/song/info</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <returns>Song information</returns>
    public async Task<AuaSongInfoContent> Info(string songname, AuaSongQueryType queryType = AuaSongQueryType.SongName)
        => await GetInfo(songname, queryType);


    private async Task<string[]> GetAlias(string songname, AuaSongQueryType queryType)
    {
        var qb = new QueryBuilder()
            .Add(queryType == AuaSongQueryType.SongId ? "songid" : "songname", songname);

        var response = JsonSerializer.Deserialize<AuaResponse<string[]>>(
            await _client.GetStringAsync("song/alias" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get alias(es) of a song.
    /// </summary>
    /// <endpoint>/song/alias</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <returns>Song alias(es)</returns>
    public async Task<string[]> Alias(string songname, AuaSongQueryType queryType = AuaSongQueryType.SongName)
        => await GetAlias(songname, queryType);


    private async Task<AuaSongRandomContent> GetRandom(double? startDouble, double? endDouble, string? startString,
        string? endString, AuaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add("start", startString ?? startDouble.ToString()!)
            .Add("end", endString ?? endDouble.ToString()!);

        if (replyWith.HasFlag(AuaReplyWith.SongInfo))
            qb.Add("withsonginfo", "true");

        var response = JsonSerializer.Deserialize<AuaResponse<AuaSongRandomContent>>(
            await _client.GetStringAsync("song/random" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get random song.
    /// </summary>
    /// <endpoint>/song/random</endpoint>
    /// <param name="start">Rating range of start</param>
    /// <param name="end">Rating range of end</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>Random song content</returns>
    public async Task<AuaSongRandomContent> Random(double start = 0.0, double end = 12.0,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => await GetRandom(start, end, null, null, replyWith);

    /// <summary>
    /// Get random song.
    /// </summary>
    /// <endpoint>/song/random</endpoint>
    /// <param name="start">Rating range of start (9+ => 9p, 10+ => 10p, etc.)</param>
    /// <param name="end">Rating range of end</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>Random song content</returns>
    public async Task<AuaSongRandomContent> Random(string start = "0", string end = "12",
        AuaReplyWith replyWith = AuaReplyWith.None)
        => await GetRandom(null, null, start, end, replyWith);

    /// <summary>
    /// Get random song.
    /// </summary>
    /// <endpoint>/song/random</endpoint>
    /// <param name="start">Rating range of start</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>Random song content</returns>
    public async Task<AuaSongRandomContent> Random(double start, AuaReplyWith replyWith)
        => await GetRandom(start, 12.0, null, null, replyWith);

    /// <summary>
    /// Get random song.
    /// </summary>
    /// <endpoint>/song/random</endpoint>
    /// <param name="start">Rating range of start (9+ => 9p, 10+ => 10p, etc.)</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>Random song content</returns>
    public async Task<AuaSongRandomContent> Random(string start, AuaReplyWith replyWith)
        => await GetRandom(null, null, start, "12", replyWith);

    /// <summary>
    /// Get random song.
    /// </summary>
    /// <endpoint>/song/random</endpoint>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>Random song content</returns>
    public async Task<AuaSongRandomContent> Random(AuaReplyWith replyWith)
        => await GetRandom(0.0, 12.0, null, null, replyWith);
}