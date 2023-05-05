using System.Text.Json.Serialization;
using UnofficialArcaeaAPI.Lib.Models;

namespace UnofficialArcaeaAPI.Lib.Responses;

#pragma warning disable CS8618

public class UaaSongRandomContent
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("ratingClass")]
    public int RatingClass { get; set; }

    [JsonPropertyName("songinfo")]
    public UaaChartInfo? SongInfo { get; set; }
}