using System.Globalization;
using System.Text.Json;
using ArcaeaUnlimitedAPI.Lib.Models;
using ArcaeaUnlimitedAPI.Lib.Responses;
using ArcaeaUnlimitedAPI.Lib.Utils;

namespace ArcaeaUnlimitedAPI.Lib.Core;

public static class AuaCommonApi
{
    public static async Task<AuaUpdateContent> GetUpdate(HttpClient client)
    {
        var response = JsonSerializer.Deserialize<AuaResponse<AuaUpdateContent>>(
            await client.GetStringAsync("update"))!;
        if (response.Status < 0)
            throw new AuaException(response.Status, response.Message!);
        return response.Content!;
    }

    public static async Task<AuaPlaydataContent[]> GetPlaydata(HttpClient client, string songname,
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
}