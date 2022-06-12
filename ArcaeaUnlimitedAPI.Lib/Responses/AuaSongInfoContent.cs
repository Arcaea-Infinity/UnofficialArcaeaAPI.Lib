using System.Text.Json.Serialization;
using ArcaeaUnlimitedAPI.Lib.Models;

namespace ArcaeaUnlimitedAPI.Lib.Responses;

#pragma warning disable CS8618

public class AuaSongInfoContent
{
    [JsonPropertyName("song_id")] public string SongId { get; set; }
    
    [JsonPropertyName("difficulties")] public AuaChartInfo[] Difficulties { get; set; }

    [JsonPropertyName("alias")] public string[] Alias { get; set; }
}