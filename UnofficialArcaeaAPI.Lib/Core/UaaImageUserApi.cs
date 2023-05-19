using System.Text.Json;
using UnofficialArcaeaAPI.Lib.Models;
using UnofficialArcaeaAPI.Lib.Responses;
using UnofficialArcaeaAPI.Lib.Utils;

namespace UnofficialArcaeaAPI.Lib.Core;

public sealed class UaaImageUserApi
{
    private readonly HttpClient _client;

    internal UaaImageUserApi(HttpClient client)
    {
        _client = client;
    }

    #region /user/info

    private async Task<byte[]> GetInfoAsyncCore(string? user, int? userCode, int recent,
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
        var resp = await _client.GetAsync("image/user/info" + qb.Build());
        return await resp.EnsureDataSuccess();
    }

    /// <summary>
    /// Get user info image.
    /// </summary>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="recent">The number of recently played songs expected, range 0-7</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info image</returns>
    public Task<byte[]> GetInfoAsync(string user, int recent = 0,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetInfoAsyncCore(user, null, recent, replyWith);

    /// <summary>
    /// Get user info image.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="recent">The number of recently played songs expected, range 0-7</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info image</returns>
    public Task<byte[]> GetInfoAsync(int userCode, int recent = 0,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetInfoAsyncCore(null, userCode, recent, replyWith);

    /// <summary>
    /// Get user info image.
    /// </summary>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info image</returns>
    public Task<byte[]> GetInfoAsync(string user, UaaReplyWith replyWith)
        => GetInfoAsyncCore(user, null, 0, replyWith);

    /// <summary>
    /// Get user info image.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo only.</param>
    /// <returns>User info image</returns>
    public Task<byte[]> GetInfoAsync(int userCode, UaaReplyWith replyWith)
        => GetInfoAsyncCore(null, userCode, 0, replyWith);

    #endregion /user/info

    #region /user/best

    private async Task<byte[]> GetBestAsyncCore(string? user, int? userCode, string songname,
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
        return await resp.EnsureDataSuccess();
    }

    /// <summary>
    /// Get user best score image.
    /// </summary>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best image</returns>
    public Task<byte[]> GetBestAsync(string user, string songName,
        UaaSongQueryType queryType = UaaSongQueryType.SongName, ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetBestAsyncCore(user, null, songName, queryType, difficulty, replyWith);

    /// <summary>
    /// Get user best score image.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best image</returns>
    public Task<byte[]> GetBestAsync(int userCode, string songName,
        UaaSongQueryType queryType = UaaSongQueryType.SongName, ArcaeaDifficulty difficulty = ArcaeaDifficulty.Future,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetBestAsyncCore(null, userCode, songName, queryType, difficulty, replyWith);

    /// <summary>
    /// Get user best score image.
    /// </summary>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best image</returns>
    public Task<byte[]> GetBestAsync(string user, string songName,
        ArcaeaDifficulty difficulty,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetBestAsyncCore(user, null, songName, UaaSongQueryType.SongName, difficulty, replyWith);

    /// <summary>
    /// Get user best score image.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best image</returns>
    public Task<byte[]> GetBestAsync(int userCode, string songName,
        ArcaeaDifficulty difficulty,
        UaaReplyWith replyWith = UaaReplyWith.None)
        => GetBestAsyncCore(null, userCode, songName, UaaSongQueryType.SongName, difficulty, replyWith);

    /// <summary>
    /// Get user best score image.
    /// </summary>
    /// <param name="user">User name or 9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best image</returns>
    public Task<byte[]> GetBestAsync(string user, string songName, UaaReplyWith replyWith)
        => GetBestAsyncCore(user, null, songName, UaaSongQueryType.SongName, ArcaeaDifficulty.Future, replyWith);

    /// <summary>
    /// Get user best score image.
    /// </summary>
    /// <param name="userCode">9-digit user code</param>
    /// <param name="songName">Any song name for fuzzy querying</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns>User best image</returns>
    public Task<byte[]> GetBestAsync(int userCode, string songName,
        UaaReplyWith replyWith)
        => GetBestAsyncCore(null, userCode, songName, UaaSongQueryType.SongName, ArcaeaDifficulty.Future, replyWith);

    #endregion /user/best
}