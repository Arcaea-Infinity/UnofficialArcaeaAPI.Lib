using System.Text.Json;
using ArcaeaUnlimitedAPI.Lib.Models;
using ArcaeaUnlimitedAPI.Lib.Responses;
using ArcaeaUnlimitedAPI.Lib.Utils;

namespace ArcaeaUnlimitedAPI.Lib.Core;

public class AuaUserApi
{
    private readonly HttpClient _client;

    public AuaUserApi(HttpClient client)
    {
        _client = client;
    }

    #region /user/info

    private async Task<AuaUserInfoContent> GetInfo(string? user, int? usercode, int recent, AuaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add("recent", recent.ToString());

        if (user is not null)
            qb.Add("user", user);
        else
            qb.Add("usercode", usercode.ToString()!);

        if (replyWith.HasFlag(AuaReplyWith.SongInfo))
            qb.Add("withsonginfo", "true");

        var response = JsonSerializer.Deserialize<AuaResponse<AuaUserInfoContent>>(
            await _client.GetStringAsync("user/info" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <endpoint>/user/info</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="recent">The number of recently played songs expected, range 0-7</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<AuaUserInfoContent> Info(string user, int recent = 0, AuaReplyWith replyWith = AuaReplyWith.None)
        => GetInfo(user, null, recent, replyWith);

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <endpoint>/user/info</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="recent">The number of recently played songs expected, range 0-7</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<AuaUserInfoContent> Info(int usercode, int recent = 0, AuaReplyWith replyWith = AuaReplyWith.None)
        => GetInfo(null, usercode, recent, replyWith);

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <endpoint>/user/info</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<AuaUserInfoContent> Info(string user, AuaReplyWith replyWith)
        => GetInfo(user, null, 0, replyWith);

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <endpoint>/user/info</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<AuaUserInfoContent> Info(int usercode, AuaReplyWith replyWith)
        => GetInfo(null, usercode, 0, replyWith);

    #endregion /user/info

    #region /user/best

    private async Task<AuaUserBestContent> GetBest(string? user, int? usercode, string songname,
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

        var response = JsonSerializer.Deserialize<AuaResponse<AuaUserBestContent>>(
            await _client.GetStringAsync("user/best" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
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
    public Task<AuaUserBestContent> Best(string user, string songname,
        AuaSongQueryType queryType = AuaSongQueryType.SongName, ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBest(user, null, songname, queryType, difficulty, replyWith);

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
    public Task<AuaUserBestContent> Best(int usercode, string songname,
        AuaSongQueryType queryType = AuaSongQueryType.SongName, ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBest(null, usercode, songname, queryType, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<AuaUserBestContent> Best(string user, string songname,
        ArcaeaDifficulty difficulty,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBest(user, null, songname, AuaSongQueryType.SongName, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<AuaUserBestContent> Best(int usercode, string songname,
        ArcaeaDifficulty difficulty,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBest(null, usercode, songname, AuaSongQueryType.SongName, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<AuaUserBestContent> Best(string user, string songname, AuaReplyWith replyWith)
        => GetBest(user, null, songname, AuaSongQueryType.SongName, ArcaeaDifficulty.Future, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <endpoint>/user/best</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<AuaUserBestContent> Best(int usercode, string songname,
        AuaReplyWith replyWith)
        => GetBest(null, usercode, songname, AuaSongQueryType.SongName, ArcaeaDifficulty.Future, replyWith);

    #endregion /user/best

    #region /user/best30

    private async Task<AuaUserBest30Content> GetBest30(string? user, int? usercode, int overflow,
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

        var response = JsonSerializer.Deserialize<AuaResponse<AuaUserBest30Content>>(
            await _client.GetStringAsync("user/best30" + qb.Build()))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <endpoint>/user/best30</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="overflow">The number of the overflow records below the best30 minimum, range 0-10</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<AuaUserBest30Content> Best30(string user, int overflow = 0,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBest30(user, null, overflow, replyWith);

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <endpoint>/user/best30</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="overflow">The number of the overflow records below the best30 minimum, range 0-10</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<AuaUserBest30Content> Best30(int usercode, int overflow = 0,
        AuaReplyWith replyWith = AuaReplyWith.None)
        => GetBest30(null, usercode, overflow, replyWith);

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <endpoint>/user/best30</endpoint>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<AuaUserBest30Content> Best30(string user, AuaReplyWith replyWith)
        => GetBest30(user, null, 0, replyWith);

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <endpoint>/user/best30</endpoint>
    /// <param name="usercode">9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<AuaUserBest30Content> Best30(int usercode, AuaReplyWith replyWith)
        => GetBest30(null, usercode, 0, replyWith);

    #endregion /user/best30
}