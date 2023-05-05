using System.Text.Json.Serialization;
using UnofficialArcaeaAPI.Lib.Models;

namespace UnofficialArcaeaAPI.Lib.Responses;

#pragma warning disable CS8618

public class UaaUserBest30Content
{
    [JsonPropertyName("best30_avg")]
    public double Best30Avg { get; set; }

    [JsonPropertyName("recent10_avg")]
    public double Recent10Avg { get; set; }

    [JsonPropertyName("account_info")]
    public AuaAccountInfo AccountInfo { get; set; }

    [JsonPropertyName("best30_list")]
    public UaaRecord[] Best30List { get; set; }

    [JsonPropertyName("best30_songinfo")]
    public UaaChartInfo[]? Best30SongInfo { get; set; }

    [JsonPropertyName("recent_score")]
    public UaaRecord? RecentScore { get; set; }

    [JsonPropertyName("recent_songinfo")]
    public UaaChartInfo? RecentSongInfo { get; set; }

    [JsonPropertyName("best30_overflow")]
    public UaaRecord[]? Best30Overflow { get; set; }

    [JsonPropertyName("best30_overflow_songinfo")]
    public UaaChartInfo[]? Best30OverflowSongInfo { get; set; }
}