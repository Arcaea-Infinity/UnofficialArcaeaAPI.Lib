using System.Text.Json.Serialization;
using UnofficialArcaeaAPI.Lib.Models;

namespace UnofficialArcaeaAPI.Lib.Responses;

#pragma warning disable CS8618

public class UaaUserBestContent
{
    [JsonPropertyName("account_info")]
    public AuaAccountInfo AccountInfo { get; set; }

    [JsonPropertyName("record")]
    public UaaRecord Record { get; set; }

    [JsonPropertyName("songinfo")]
    public UaaChartInfo[]? SongInfo { get; set; }

    [JsonPropertyName("recent_score")]
    public UaaRecord? RecentScore { get; set; }

    [JsonPropertyName("recent_songinfo")]
    public UaaChartInfo? RecentSongInfo { get; set; }
}