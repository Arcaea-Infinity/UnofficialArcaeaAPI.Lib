using System.Text.Json;
using UnofficialArcaeaAPI.Lib.Models;
using UnofficialArcaeaAPI.Lib.Responses;
using UnofficialArcaeaAPI.Lib.Utils;

namespace UnofficialArcaeaAPI.Lib.Core;

public sealed class UaaUserApi
{
    private readonly HttpClient _client;

    internal UaaUserApi(HttpClient client)
    {
        _client = client;
    }

    #region /user/info

    private async Task<UaaUserInfoContent> GetInfoAsyncCore(string? user, int? usercode, int recent, AuaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add("recent", recent.ToString());

        if (user is not null)
            qb.Add("user", user);
        else
            qb.Add("usercode", usercode.ToString()!);

        if (replyWith.HasFlag(AuaReplyWith.SongInfo))
            qb.Add("withsonginfo", "true");
        var resp = await _client.GetAsync("user/info" + qb.Build());
        var json = JsonSerializer.Deserialize<UaaResponse<UaaUserInfoContent>>(
            await resp.Content.ReadAsStringAsync())!;
        if (json.Status < 0)
            throw new UaaRequestException(json.Status, json.Message!);
        return json.Content!;
    }

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <endpoint>/user/info</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="recent">The number of recently played songs expected, range 0-7</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<UaaUserInfoContent> GetInfoAsync(string user, int recent = 0, AuaReplyWith replyWith = AuaReplyWith.None)
        => GetInfoAsyncCore(user, null, recent, replyWith);

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <endpoint>/user/info</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="recent">The number of recently played songs expected, range 0-7</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<UaaUserInfoContent> GetInfoAsync(int usercode, int recent = 0, AuaReplyWith replyWith = AuaReplyWith.None)
        => GetInfoAsyncCore(null, usercode, recent, replyWith);

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <endpoint>/user/info</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<UaaUserInfoContent> GetInfoAsync(string user, AuaReplyWith replyWith)
        => GetInfoAsyncCore(user, null, 0, replyWith);

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <endpoint>/user/info</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<UaaUserInfoContent> GetInfoAsync(int usercode, AuaReplyWith replyWith)
        => GetInfoAsyncCore(null, usercode, 0, replyWith);

    #endregion /user/info

    #region /user/best

    private async Task<UaaUserBestContent> GetBestAsyncCore(string? user, int? usercode, string songname,
        AuaSongQueryType queryType, ArcaeaDifficulty difficulty, AuaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add(queryType == AuaSongQueryType.SongId ? "songid" : "songname", songname)
            .Add("difficulty", ((int)difficulty).ToString());

        if (user is not null)
            qb.Add("user", user);
        else
            qb.Add("usercode", usercode.ToString()!);

        if (replyWith.HasFlag(AuaReplyWith.Recent))
            qb.Add("withrecent", "true");
        if (replyWith.HasFlag(AuaReplyWith.SongInfo))
            qb.Add("withsonginfo", "true");

        var resp = await _client.GetAsync("user/best" + qb.Build());
        var json = JsonSerializer.Deserialize<UaaResponse<UaaUserBestContent>>(
            await resp.Content.ReadAsStringAsync())!;
        if (json.Status < 0)
            throw new UaaRequestException(json.Status, json.Message!);
        return json.Content!;
    }

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(string user, string songname,
        AuaSongQueryType queryType = AuaSongQueryType.SongName, ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBestAsyncCore(user, null, songname, queryType, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(int usercode, string songname,
        AuaSongQueryType queryType = AuaSongQueryType.SongName, ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBestAsyncCore(null, usercode, songname, queryType, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(string user, string songname,
        ArcaeaDifficulty difficulty,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBestAsyncCore(user, null, songname, AuaSongQueryType.SongName, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(int usercode, string songname,
        ArcaeaDifficulty difficulty,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBestAsyncCore(null, usercode, songname, AuaSongQueryType.SongName, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(string user, string songname, AuaReplyWith replyWith)
        => GetBestAsyncCore(user, null, songname, AuaSongQueryType.SongName, ArcaeaDifficulty.Future, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(int usercode, string songname,
        AuaReplyWith replyWith)
        => GetBestAsyncCore(null, usercode, songname, AuaSongQueryType.SongName, ArcaeaDifficulty.Future, replyWith);

    #endregion /user/best

    #region /user/best30

    private async Task<UaaUserBest30Content> GetBest30AsyncCore(string? user, int? usercode, int overflow,
        AuaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add("overflow", overflow.ToString());

        if (user is not null)
            qb.Add("user", user);
        else
            qb.Add("usercode", usercode.ToString()!);

        if (replyWith.HasFlag(AuaReplyWith.Recent))
            qb.Add("withrecent", "true");
        if (replyWith.HasFlag(AuaReplyWith.SongInfo))
            qb.Add("withsonginfo", "true");

        var resp = await _client.GetAsync("user/best30" + qb.Build());
        var json = JsonSerializer.Deserialize<UaaResponse<UaaUserBest30Content>>(
            await resp.Content.ReadAsStringAsync())!;
        if (json.Status < 0)
            throw new UaaRequestException(json.Status, json.Message!);
        return json.Content!;
    }

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <endpoint>/user/best30</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="overflow">The number of the overflow records below the best30 minimum, range 0-10</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<UaaUserBest30Content> GetBest30Async(string user, int overflow = 0,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBest30AsyncCore(user, null, overflow, replyWith);

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <endpoint>/user/best30</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="overflow">The number of the overflow records below the best30 minimum, range 0-10</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<UaaUserBest30Content> GetBest30Async(int usercode, int overflow = 0,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBest30AsyncCore(null, usercode, overflow, replyWith);

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <endpoint>/user/best30</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<UaaUserBest30Content> GetBest30Async(string user, AuaReplyWith replyWith)
        => GetBest30AsyncCore(user, null, 0, replyWith);

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <endpoint>/user/best30</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<UaaUserBest30Content> GetBest30Async(int usercode, AuaReplyWith replyWith)
        => GetBest30AsyncCore(null, usercode, 0, replyWith);

    #endregion /user/best30
}