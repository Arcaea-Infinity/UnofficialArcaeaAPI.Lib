using System.Text.Json.Serialization;
using ArcaeaUnlimitedAPI.Lib.Models;

namespace ArcaeaUnlimitedAPI.Lib.Responses;

#pragma warning disable CS8618

public class AuaSongRandomContent
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("ratingClass")]
    public int RatingClass { get; set; }

    [JsonPropertyName("songinfo")]
    public AuaChartInfo? SongInfo { get; set; }
}