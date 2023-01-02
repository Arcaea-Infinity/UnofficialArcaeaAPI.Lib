using ArcaeaUnlimitedAPI.Lib.Core;
using ArcaeaUnlimitedAPI.Lib.Models;
using ArcaeaUnlimitedAPI.Lib.Responses;

namespace ArcaeaUnlimitedAPI.Lib;

public class AuaClient
{
    public HttpClient? HttpClient { get; set; }

    public string Token { get; set; } = "";

    public string UserAgent { get; set; } = "";

    public int Timeout { get; set; } = 60;

    public string ApiUrl { get; set; } = "";

    public AuaUserApi User { get; private set; } = null!;
    public AuaSongApi Song { get; private set; } = null!;
    public AuaAssetsApi Assets { get; private set; } = null!;
    public AuaDataApi Data { get; private set; } = null!;

    public AuaClient Initialize()
    {
        if (HttpClient is null)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ApiUrl.EndsWith("/") ? ApiUrl : ApiUrl + "/"),
                Timeout = new TimeSpan(0, 0, 0, Timeout)
            };

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent); // For compatibility

            HttpClient = client;
        }

        User = new AuaUserApi(HttpClient);
        Song = new AuaSongApi(HttpClient);
        Assets = new AuaAssetsApi(HttpClient);
        Data = new AuaDataApi(HttpClient);

        return this;
    }

    #region Obsoleted

    /// <summary>
    /// Get the latest Arcaea apk download url.
    /// </summary>
    /// <endpoint>/data/update</endpoint>
    /// <returns>Update content with url and version</returns>
    [Obsolete("Use Data.Update instead.")]
    public Task<AuaUpdateContent> Update() => Data.Update();


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
    [Obsolete("Use Data.Playdata instead.")]
    public Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        ArcaeaDifficulty difficulty, int start, int end)
        => Data.Playdata(songname, queryType, difficulty, start, end);

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
    [Obsolete("Use Data.Playdata instead.")]
    public Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        ArcaeaDifficulty difficulty, double start, double end)
        => Data.Playdata(songname, queryType, difficulty, start, end);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    [Obsolete("Use Data.Playdata instead.")]
    public Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        int start, int end)
        => Data.Playdata(songname, queryType, start, end);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    [Obsolete("Use Data.Playdata instead.")]
    public Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        double start, double end)
        => Data.Playdata(songname, queryType, start, end);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    [Obsolete("Use Data.Playdata instead.")]
    public Task<AuaPlaydataContent[]> Playdata(string songname, int start, int end)
        => Data.Playdata(songname, start, end);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/data/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    [Obsolete("Use Data.Playdata instead.")]
    public Task<AuaPlaydataContent[]> Playdata(string songname, double start, double end)
        => Data.Playdata(songname, start, end);

    #endregion Obsoleted
}