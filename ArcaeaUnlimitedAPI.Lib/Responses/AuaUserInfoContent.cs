using System.Text.Json.Serialization;
using ArcaeaUnlimitedAPI.Lib.Models;

namespace ArcaeaUnlimitedAPI.Lib.Responses;

#pragma warning disable CS8618

public class AuaUserInfoContent
{
    [JsonPropertyName("account_info")]
    public AuaAccountInfo AccountInfo { get; set; }

    [JsonPropertyName("recent_score")]
    public AuaRecord[]? RecentScore { get; set; }

    [JsonPropertyName("songinfo")]
    public AuaChartInfo[]? SongInfo { get; set; }
}