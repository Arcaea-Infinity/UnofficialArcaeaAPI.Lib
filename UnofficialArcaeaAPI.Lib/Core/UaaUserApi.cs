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

    private async Task<UaaUserInfoContent> GetInfoAsyncCore(string? user, int? userCode, int recent,
        UaaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add("recent", recent.ToString());

        if (user is not null)
            qb.Add("user_name", user);
        else
            qb.Add("user_code", userCode.ToString()!);

        if (replyWith.HasFlag(UaaReplyWith.SongInfo))
            qb.Add("with_song_info", "true");
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
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="recent">The number of recently played songs expected, range 0-7</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<UaaUserInfoContent> GetInfoAsync(string user, int recent = 0,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetInfoAsyncCore(user, null, recent, replyWith);

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="recent">The number of recently played songs expected, range 0-7</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<UaaUserInfoContent> GetInfoAsync(int userCode, int recent = 0,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetInfoAsyncCore(null, userCode, recent, replyWith);

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<UaaUserInfoContent> GetInfoAsync(string user, UaaReplyWith replyWith)
        => GetInfoAsyncCore(user, null, 0, replyWith);

    /// <summary>
    /// Get user info.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info content</returns>
    public Task<UaaUserInfoContent> GetInfoAsync(int userCode, UaaReplyWith replyWith)
        => GetInfoAsyncCore(null, userCode, 0, replyWith);

    #endregion /user/info

    #region /user/best

    private async Task<UaaUserBestContent> GetBestAsyncCore(string? user, int? userCode, string songname,
        UaaSongQueryType queryType, ArcaeaDifficulty difficulty, UaaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add(queryType == UaaSongQueryType.SongId ? "song_id" : "song_name", songname)
            .Add("difficulty", ((int)difficulty).ToString());

        if (user is not null)
            qb.Add("user_name", user);
        else
            qb.Add("user_code", userCode.ToString()!);

        if (replyWith.HasFlag(UaaReplyWith.Recent))
            qb.Add("with_recent", "true");
        if (replyWith.HasFlag(UaaReplyWith.SongInfo))
            qb.Add("with_song_info", "true");

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
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(string user, string songName,
        UaaSongQueryType queryType = UaaSongQueryType.SongName, ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetBestAsyncCore(user, null, songName, queryType, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(int userCode, string songName,
        UaaSongQueryType queryType = UaaSongQueryType.SongName, ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetBestAsyncCore(null, userCode, songName, queryType, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(string user, string songName,
        ArcaeaDifficulty difficulty,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetBestAsyncCore(user, null, songName, UaaSongQueryType.SongName, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(int userCode, string songName,
        ArcaeaDifficulty difficulty,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetBestAsyncCore(null, userCode, songName, UaaSongQueryType.SongName, difficulty, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(string user, string songName, UaaReplyWith replyWith)
        => GetBestAsyncCore(user, null, songName, UaaSongQueryType.SongName, ArcaeaDifficulty.Future, replyWith);

    /// <summary>
    /// Get user best score.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best content</returns>
    public Task<UaaUserBestContent> GetBestAsync(int userCode, string songName,
        UaaReplyWith replyWith)
        => GetBestAsyncCore(null, userCode, songName, UaaSongQueryType.SongName, ArcaeaDifficulty.Future, replyWith);

    #endregion /user/best

    #region /user/bests/session

    private async Task<UaaUserBestsSessionContent> GetBestsSessionAsyncCore(string? user, int? userCode)
    {
        var qb = new QueryBuilder();

        if (user is not null)
            qb.Add("user_name", user);
        else
            qb.Add("user_code", userCode.ToString()!);

        var resp = await _client.GetAsync("user/bests/session" + qb.Build());
        var json = JsonSerializer.Deserialize<UaaResponse<UaaUserBestsSessionContent>>(
            await resp.Content.ReadAsStringAsync())!;

        if (json.Status == -33)
            json.Content!.IsCacheSession = true;
        else if (json.Status < 0)
            throw new UaaRequestException(json.Status, json.Message!);

        return json.Content!;
    }

    /// <summary>
    /// Get user bests session information.
    /// </summary>
    /// <param name="user">User name or 9-digit user code</param>
    /// <returns>User bests session information</returns>
    public Task<UaaUserBestsSessionContent> GetBestsSessionAsync(string user)
        => GetBestsSessionAsyncCore(user, null);

    /// <summary>
    /// Get user bests session information.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <returns>User bests session information</returns>
    public Task<UaaUserBestsSessionContent> GetBestsSessionAsync(int userCode)
        => GetBestsSessionAsyncCore(null, userCode);

    #endregion /user/bests/session

    #region /user/bests/result

    private async Task<UaaUserBestsResultContent> GetBestsResultAsyncCore(string sessionInfo, int overflow,
        UaaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add("session_info", sessionInfo)
            .Add("overflow", overflow.ToString());

        if (replyWith.HasFlag(UaaReplyWith.Recent))
            qb.Add("with_recent", "true");
        if (replyWith.HasFlag(UaaReplyWith.SongInfo))
            qb.Add("with_song_info", "true");

        var resp = await _client.GetAsync("user/bests/result" + qb.Build());
        var json = JsonSerializer.Deserialize<UaaResponse<UaaUserBestsResultContent>>(
            await resp.Content.ReadAsStringAsync())!;

        if (json.Status is -31 or -32)
            throw new UaaBestsSessionPendingException(json.Status, json.Message!)
            {
                QueriedCharts = json.Content!.QueriedCharts,
                CurrentAccount = json.Content.CurrentAccount
            };
        if (json.Status < 0)
            throw new UaaRequestException(json.Status, json.Message!);

        return json.Content!;
    }

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <param name="sessionInfo">Session info from <see cref="GetBestsSessionAsync(string)"></see></param>
    /// <param name="overflow">The number of the overflow records below the best30 minimum, range 0-10</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<UaaUserBestsResultContent> GetBestsResultAsync(string sessionInfo, int overflow = 0,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetBestsResultAsyncCore(sessionInfo, overflow, replyWith);

    /// <summary>
    /// Get user top 30 score.
    /// </summary>
    /// <param name="sessionInfo">Session info from <see cref="GetBestsSessionAsync(string)"></see></param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best30 content</returns>
    public Task<UaaUserBestsResultContent> GetBestsResultAsync(string sessionInfo, UaaReplyWith replyWith)
        => GetBestsResultAsyncCore(sessionInfo, 0, replyWith);

    #endregion /user/bests/result
}