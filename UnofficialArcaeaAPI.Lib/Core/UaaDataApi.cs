using System.Text.Json;
using UnofficialArcaeaAPI.Lib.Models;
using UnofficialArcaeaAPI.Lib.Responses;
using UnofficialArcaeaAPI.Lib.Utils;

namespace UnofficialArcaeaAPI.Lib.Core;

public sealed class UaaDataApi
{
    private readonly HttpClient _client;

    internal UaaDataApi(HttpClient client)
    {
        _client = client;
    }

    #region /data/update

    private async Task<UaaDataUpdateContent> GetUpdateAsyncCore()
    {
        var resp = await _client.GetAsync("/data/update");
        var json = JsonSerializer.Deserialize<UaaResponse<UaaDataUpdateContent>>(
            await resp.Content.ReadAsStringAsync())!;
        if (json.Status < 0)
            throw new UaaRequestException(json.Status, json.Message!);
        return json.Content!;
    }

    /// <summary>
    /// Get the latest Arcaea apk download url.
    /// </summary>
    /// <returns>Update content with url and version</returns>
    public Task<UaaDataUpdateContent> GetUpdateAsync() => GetUpdateAsyncCore();

    #endregion /data/update

    #region /data/theory

    private async Task<UaaUserBestsContent> GetTheoryAsyncCore(string version,
        int overflow, AuaReplyWith replyWith)
    {
        var qb = new QueryBuilder()
            .Add("version", version)
            .Add("overflow", overflow.ToString());

        if (replyWith.HasFlag(AuaReplyWith.Recent))
            qb.Add("with_recent", "true");
        if (replyWith.HasFlag(AuaReplyWith.SongInfo))
            qb.Add("with_song_info", "true");

        var resp = await _client.GetAsync("data/theory" + qb.Build());
        var json = JsonSerializer.Deserialize<UaaResponse<UaaUserBestsContent>>(
            await resp.Content.ReadAsStringAsync())!;
        if (json.Status < 0)
            throw new UaaRequestException(json.Status, json.Message!);
        return json.Content!;
    }

    /// <summary>
    /// Get the highest best30 scores in a specified version of Arcaea.
    /// </summary>
    /// <param name="version">The version of Arcaea, formatted like <c>4.0</c></param>
    /// <param name="overflow">The number of the overflow records below the best30 minimum, range 0-10</param>
    /// <param name="replyWith">Additional information to reply with. Supports songinfo and recent.</param>
    /// <returns></returns>
    public Task<UaaUserBestsContent> GetTheoryAsync(string version,
        int overflow = 0, AuaReplyWith replyWith = AuaReplyWith.None)
        => GetTheoryAsyncCore(version, overflow, replyWith);

    #endregion /data/theory

    #region /data/challenge

    private async Task<string> GetChallengeAsyncCore(string path, string? body, long? timestamp)
    {
        var qb = new QueryBuilder()
            .Add("path", path);

        if (body is not null) qb.Add("body", body);
        if (timestamp is not null) qb.Add("timestamp", timestamp.ToString()!);

        var resp = await _client.GetAsync("data/challenge" + qb.Build());
        var json = JsonSerializer.Deserialize<UaaResponse<string>>(
            await resp.Content.ReadAsStringAsync())!;
        if (json.Status < 0)
            throw new UaaRequestException(json.Status, json.Message!);
        return json.Content!;
    }

    /// <summary>
    /// Get challenge of a specified arcapi path.
    /// </summary>
    /// <param name="path">Request arcapi path.</param>
    /// <param name="body">Request body, optional when body is empty.</param>
    /// <param name="timestamp">Request timestamp.</param>
    /// <returns>Challenge string.</returns>
    /// <remarks>It is designed for the release version of AUA, and not available for the release version.</remarks>
    public Task<string> GetChallengeAsync(string path, string? body = null, long? timestamp = null)
        => GetChallengeAsyncCore(path, body, timestamp);

    /// <summary>
    /// Get challenge of a specified arcapi path.
    /// </summary>
    /// <param name="path">Request arcapi path.</param>
    /// <param name="timestamp">Request timestamp.</param>
    /// <returns>Challenge string.</returns>
    /// <remarks>It is designed for the release version of AUA, and not available for the release version.</remarks>
    public Task<string> GetChallengeAsync(string path, long? timestamp = null)
        => GetChallengeAsyncCore(path, null, timestamp);

    #endregion /data/challenge

    #region /data/cert

    /// <summary>
    /// Get cert data of current version of Arcaea.
    /// </summary>
    /// <returns></returns>
    public async Task<UaaDataCertContent> GetCertAsync()
    {
        var resp = await _client.GetAsync("data/cert");
        var json = JsonSerializer.Deserialize<UaaResponse<UaaDataCertContent>>(
            await resp.Content.ReadAsStringAsync())!;
        if (json.Status < 0)
            throw new UaaRequestException(json.Status, json.Message!);
        return json.Content!;
    }

    #endregion /data/cert
}