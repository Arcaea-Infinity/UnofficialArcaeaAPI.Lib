using System.Net.Http.Headers;
using ArcaeaUnlimitedAPI.Lib.Core;
using ArcaeaUnlimitedAPI.Lib.Models;
using ArcaeaUnlimitedAPI.Lib.Responses;

namespace ArcaeaUnlimitedAPI.Lib;

public class AuaClient
{
    public HttpClient? HttpClient { get; set; }

    public string UserAgent { get; set; } = "";

    public int Timeout { get; set; } = 60;

    public string ApiUrl { get; set; } = "";

    public AuaUserApi User { get; private set; } = null!;
    public AuaSongApi Song { get; private set; } = null!;
    public AuaAssetsApi Assets { get; private set; } = null!;

    public AuaClient Initialize()
    {
        if (HttpClient is null)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ApiUrl.EndsWith("/") ? ApiUrl : ApiUrl + "/"),
                Timeout = new TimeSpan(0, 0, 0, Timeout)
            };
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            HttpClient = client;
        }

        User = new AuaUserApi(HttpClient);
        Song = new AuaSongApi(HttpClient);
        Assets = new AuaAssetsApi(HttpClient);

        return this;
    }

    /// <summary>
    /// Get latest Arcaea apk download url.
    /// </summary>
    /// <endpoint>/update</endpoint>
    /// <returns>Update content with url and version</returns>
    public async Task<AuaUpdateContent> Update()
        => await AuaCommonApi.GetUpdate(HttpClient!);


    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        ArcaeaDifficulty difficulty, int start, int end)
        => await AuaCommonApi.GetPlaydata(HttpClient!, songname, queryType, difficulty, start, end, null, null);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="difficulty">Song difficulty</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        ArcaeaDifficulty difficulty, double start, double end)
        => await AuaCommonApi.GetPlaydata(HttpClient!, songname, queryType, difficulty, null, null, start, end);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        int start, int end)
        => await AuaCommonApi.GetPlaydata(HttpClient!, songname, queryType, ArcaeaDifficulty.Future, start, end, null,
            null);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying or sid in Arcaea songlist</param>
    /// <param name="queryType">Specify the query type between songname and songid</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, AuaSongQueryType queryType,
        double start, double end)
        => await AuaCommonApi.GetPlaydata(HttpClient!, songname, queryType, ArcaeaDifficulty.Future, null, null, start,
            end);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, int start, int end)
        => await AuaCommonApi.GetPlaydata(HttpClient!, songname, AuaSongQueryType.SongName, ArcaeaDifficulty.Future,
            start, end, null, null);

    /// <summary>
    /// Get global play data of a song.
    /// </summary>
    /// <endpoint>/playdata</endpoint>
    /// <param name="songname">Any song name for fuzzy querying</param>
    /// <param name="start">Range of potential start (100*)</param>
    /// <param name="end">Range of potential end (100*)</param>
    /// <returns>Play data content</returns>
    public async Task<AuaPlaydataContent[]> Playdata(string songname, double start, double end)
        => await AuaCommonApi.GetPlaydata(HttpClient!, songname, AuaSongQueryType.SongName, ArcaeaDifficulty.Future,
            null, null, start, end);
}