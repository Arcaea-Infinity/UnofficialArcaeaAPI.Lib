using System.Text.Json.Serialization;
using ArcaeaUnlimitedAPI.Lib.Models;

namespace ArcaeaUnlimitedAPI.Lib.Responses;

#pragma warning disable CS8618

public class AuaUserBestContent
{
    [JsonPropertyName("account_info")]
    public AuaAccountInfo AccountInfo { get; set; }

    [JsonPropertyName("record")]
    public AuaRecord Record { get; set; }

    [JsonPropertyName("songinfo")]
    public AuaChartInfo[]? SongInfo { get; set; }

    [JsonPropertyName("recent_score")]
    public AuaRecord? RecentScore { get; set; }

    [JsonPropertyName("recent_songinfo")]
    public AuaChartInfo? RecentSongInfo { get; set; }
}