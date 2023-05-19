using System.Text.Json.Serialization;
using UnofficialArcaeaAPI.Lib.Models;

namespace UnofficialArcaeaAPI.Lib.Responses;

#pragma warning disable CS8618

public class UaaUserBestsContent
{
    [JsonPropertyName("best30_avg")]
    public double Best30Avg { get; set; }

    [JsonPropertyName("recent10_avg")]
    public double Recent10Avg { get; set; }

    [JsonPropertyName("account_info")]
    public UaaAccountInfo AccountInfo { get; set; }

    [JsonPropertyName("best30_list")]
    public UaaRecord[] Best30List { get; set; }

    [JsonPropertyName("best30_song_info")]
    public UaaChartInfo[]? Best30SongInfo { get; set; }

    [JsonPropertyName("recent_score")]
    public UaaRecord? RecentScore { get; set; }

    [JsonPropertyName("recent_song_info")]
    public UaaChartInfo? RecentSongInfo { get; set; }

    [JsonPropertyName("best30_overflow")]
    public UaaRecord[]? Best30Overflow { get; set; }

    [JsonPropertyName("best30_overflow_song_info")]
    public UaaChartInfo[]? Best30OverflowSongInfo { get; set; }
}