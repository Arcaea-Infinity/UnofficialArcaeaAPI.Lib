using System.Text.Json.Serialization;
using UnofficialArcaeaAPI.Lib.Models;

namespace UnofficialArcaeaAPI.Lib.Responses;

#pragma warning disable CS8618

public class UaaUserInfoContent
{
    [JsonPropertyName("account_info")]
    public UaaAccountInfo AccountInfo { get; set; }

    [JsonPropertyName("best30_avg")]
    public double Best30Avg { get; set; }

    [JsonPropertyName("recent10_avg")]
    public double Recent10Avg { get; set; }

    [JsonPropertyName("recent_score")]
    public UaaRecord[]? RecentScore { get; set; }

    [JsonPropertyName("song_info")]
    public UaaChartInfo[]? SongInfo { get; set; }
}