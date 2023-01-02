using System.Text.Json;
using ArcaeaUnlimitedAPI.Lib.Models;
using ArcaeaUnlimitedAPI.Lib.Responses;
using ArcaeaUnlimitedAPI.Lib.Utils;

namespace ArcaeaUnlimitedAPI.Lib.Core;

public class AuaDataApi
{
    private readonly HttpClient _client;

    public AuaDataApi(HttpClient client)
    {
        _client = client;
    }

    #region /data/update

    private async Task<AuaUpdateContent> GetUpdate()
    {
        var response = JsonSerializer.Deserialize<AuaResponse<AuaUpdateContent>>(
            await _client.GetStringAsync("/data/update"))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get the latest Arcaea apk download url.
    /// </summary>
    /// <endpoint>/data/update</endpoint>
    /// <returns>Update content with url and version</returns>
    public Task<AuaUpdateContent> Update() => GetUpdate();

    #endregion /data/update

    #region /data/playdata

    private static async Task<AuaPlaydataContent[]> GetPlaydata(HttpClient client, string songname,
        AuaSongQueryType queryType, ArcaeaDifficulty difficulty,
        int? startInt, int? endInt, double? startDouble, double? endDouble)
    {
        var qb = new QueryBuilder()
            .Add(queryType == AuaSongQueryType.SongId ? "songid" : "songname", songname)
            .Add("difficulty", ((int)difficulty).ToString())
            .Add("start", (startDouble is null
                ? startInt.ToString()
                : ((int)Math.Round(startDouble.Value * 100)).ToString())!)
            .Add("end", (endDouble is null
                ? endInt.ToString()
                : ((int)Math.Round(endDouble.Value * 100)).ToString())!);

        var response = JsonSerializer.Deserialize<AuaResponse<AuaPlaydataContent[]>>(
            await client.GetStringAsync("playdata" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        ArcaeaDifficulty difficulty, int start, int end)
        => await GetPlaydata(_client, songname, queryType, difficulty, start, end, null, null);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        ArcaeaDifficulty difficulty, double start, double end)
        => await GetPlaydata(_client, songname, queryType, difficulty, null, null, start, end);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        int start, int end)
        => await GetPlaydata(_client, songname, queryType, ArcaeaDifficulty.Future, start, end, null,
            null);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        double start, double end)
        => await GetPlaydata(_client, songname, queryType, ArcaeaDifficulty.Future, null, null, start,
            end);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, int start, int end)
        => await GetPlaydata(_client, songname, AuaSongQueryType.SongName, ArcaeaDifficulty.Future,
            start, end, null, null);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, double start, double end)
        => await GetPlaydata(_client, songname, AuaSongQueryType.SongName, ArcaeaDifficulty.Future,
            null, null, start, end);

    #endregion /data/playdata

    #region /data/theory

    private async Task<AuaUserBest30Content> GetTheory(string version,
        int overflow, AuaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add("version", version)
            .Add("overflow", overflow.ToString());

        if (replyWith.HasFlag(AuaReplyWith.Recent))
            qb.Add("withrecent", "true");
        if (replyWith.HasFlag(AuaReplyWith.SongInfo))
            qb.Add("withsonginfo", "true");

        var response = JsonSerializer.Deserialize<AuaResponse<AuaUserBest30Content>>(
            await _client.GetStringAsync("data/theory" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get the highest best30 scores in a specified version of Arcaea.
    /// </summary>
    /// <endpoint>/data/theory</endpoint>
    /// <param name="version">The version of Arcaea, formatted like <c>4.0</c></param>
    /// <param name="overflow">The number of the overflow records below the best30 minimum, range 0-10</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns></returns>
    public Task<AuaUserBest30Content> Theory(string version,
        int overflow = 0, AuaReplyWith replyWith = AuaReplyWith.None)
        => GetTheory(version, overflow, replyWith);

    #endregion /data/theory

    #region /data/density

    private async Task<int[][]> GetDensity(string songname, AuaSongQueryType queryType, ArcaeaDifficulty difficulty)
    {
        var qb = new QueryBuilder()
            .Add(queryType == AuaSongQueryType.SongId ? "songid" : "songname", songname);
        qb.Add("difficulty", ((int)difficulty).ToString());

        var response = JsonSerializer.Deserialize<AuaResponse<int[][]>>(
            await _client.GetStringAsync("data/theory" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get song play density.
    /// </summary>
    /// <endpoint>/data/density</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>
    /// A data array. <br/>
    /// The each sub-array in the data contains 3 items:
    /// <list type="number">
    ///     <item>Formatted score (<c>score / 10000</c>)</item>
    ///     <item>Formatted potential (<c>potential / 10</c>)</item>
    ///     <item>User count</item>
    /// </list>
    /// </returns>
    /// <remarks>It is a large data set, so it is not recommended to use this API frequently.</remarks>
    public Task<int[][]> Density(string songname, AuaSongQueryType queryType = AuaSongQueryType.SongName,
        ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future)
        => GetDensity(songname, queryType, difficulty);

    /// <summary>
    /// Get song play density.
    /// </summary>
    /// <endpoint>/data/density</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <returns>
    /// A data array. <br/>
    /// The each sub-array in the data contains 3 items:
    /// <list type="number">
    ///     <item>Formatted score (<c>score / 10000</c>)</item>
    ///     <item>Formatted potential (<c>potential / 10</c>)</item>
    ///     <item>User count</item>
    /// </list>
    /// </returns>
    /// <remarks>It is a large data set, so it is not recommended to use this API frequently.</remarks>
    public Task<int[][]> Density(string songname, ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future)
        => GetDensity(songname, AuaSongQueryType.SongName, difficulty);

    #endregion /data/density

    #region /data/challenge

    private async Task<string> GetChallenge(string path, string? body, long? timestamp)
    {
        var qb = new QueryBuilder()
            .Add("path", path);

        if (body is not null) qb.Add("body", body);
        if (timestamp is not null) qb.Add("timestamp", timestamp.ToString()!);

        var response = JsonSerializer.Deserialize<AuaResponse<string>>(
            await _client.GetStringAsync("data/challenge" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get challenge of a specified arcapi path.
    /// </summary>
    /// <param name="path">Request arcapi path.</param>
    /// <param name="body">Request body, optional when body is empty.</param>
    /// <param name="timestamp">Request timestamp.</param>
    /// <returns>Challenge string.</returns>
    /// <remarks>It is designed for the release version of AUA, and not available for the release version.</remarks>
    public Task<string> Challenge(string path, string? body = null, long? timestamp = null)
        => GetChallenge(path, body, timestamp);

    /// <summary>
    /// Get challenge of a specified arcapi path.
    /// </summary>
    /// <param name="path">Request arcapi path.</param>
    /// <param name="timestamp">Request timestamp.</param>
    /// <returns>Challenge string.</returns>
    /// <remarks>It is designed for the release version of AUA, and not available for the release version.</remarks>
    public Task<string> Challenge(string path, long? timestamp = null)
        => GetChallenge(path, null, timestamp);

    #endregion /data/challenge
}