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
    
    private async Task<AuaUpdateContent> GetUpdate()
    {
        var response = JsonSerializer.Deserialize<AuaResponse<AuaUpdateContent>>(
            await _client.GetStringAsync("/data/update"))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    /// <summary>
    /// Get latest Arcaea apk download url.
    /// </summary>
    /// <endpoint>/data/update</endpoint>
    /// <returns>Update content with url and version</returns>
    public Task<AuaUpdateContent> Update() => GetUpdate();
    
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
}