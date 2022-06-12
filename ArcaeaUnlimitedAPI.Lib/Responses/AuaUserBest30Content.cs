using System.Text.Json.Serialization;
using ArcaeaUnlimitedAPI.Lib.Models;

namespace ArcaeaUnlimitedAPI.Lib.Responses;

#pragma warning disable CS8618

public class AuaUserBest30Content
{
    [JsonPropertyName("best30_avg")] public double Best30Avg { get; set; }
    
    [JsonPropertyName("recent10_avg")] public double Recent10Avg { get; set; }

    [JsonPropertyName("account_info")] public AuaAccountInfo AccountInfo { get; set; }

    [JsonPropertyName("best30_list")] public AuaRecord[] Best30List { get; set; }
    
    [JsonPropertyName("best30_songinfo")] public AuaChartInfo[]? Best30SongInfo { get; set; }
    
    [JsonPropertyName("recent_score")] public AuaRecord? RecentScore { get; set; }
    
    [JsonPropertyName("recent_songinfo")] public AuaChartInfo? RecentSongInfo { get; set; }
}