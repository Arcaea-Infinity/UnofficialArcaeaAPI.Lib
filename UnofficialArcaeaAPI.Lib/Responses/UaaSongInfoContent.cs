using System.Text.Json.Serialization;
using UnofficialArcaeaAPI.Lib.Models;

namespace UnofficialArcaeaAPI.Lib.Responses;

#pragma warning disable CS8618

public class UaaSongInfoContent
{
    [JsonPropertyName("song_id")]
    public string SongId { get; set; }

    [JsonPropertyName("difficulties")]
    public UaaChartInfo[] Difficulties { get; set; }

    [JsonPropertyName("alias")]
    public string[] Alias { get; set; }
}