using UnofficialArcaeaAPI.Lib.Core;
using UnofficialArcaeaAPI.Lib.Models;
using UnofficialArcaeaAPI.Lib.Responses;

namespace UnofficialArcaeaAPI.Lib;

public class UaaClient
{
    public HttpClient? HttpClient { get; set; }

    public string Token { get; set; } = "";

    public string UserAgent { get; set; } = "";

    public int Timeout { get; set; } = 60;

    public string ApiUrl { get; set; } = "";

    public UaaUserApi User { get; private set; } = null!;
    public UaaSongApi Song { get; private set; } = null!;
    public UaaAssetsApi Assets { get; private set; } = null!;
    public UaaDataApi Data { get; private set; } = null!;

    public UaaClient Initialize()
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

        User = new UaaUserApi(HttpClient);
        Song = new UaaSongApi(HttpClient);
        Assets = new UaaAssetsApi(HttpClient);
        Data = new UaaDataApi(HttpClient);

        return this;
    }

    #region Obsoleted

    /// <summary>
    /// Get the latest Arcaea apk download url.
    /// </summary>
    /// <endpoint>/data/update</endpoint>
    /// <returns>Update content with url and version</returns>
    [Obsolete("Use Data.Update instead.")]
    public Task<UaaUpdateContent> Update() => Data.Update();


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
    public Task<UaaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
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
    public Task<UaaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
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
    public Task<UaaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
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
    public Task<UaaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
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
    public Task<UaaPlaydataContent[]> Playdata(string songname, int start, int end)
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
    public Task<UaaPlaydataContent[]> Playdata(string songname, double start, double end)
        => Data.Playdata(songname, start, end);

    #endregion Obsoleted
}