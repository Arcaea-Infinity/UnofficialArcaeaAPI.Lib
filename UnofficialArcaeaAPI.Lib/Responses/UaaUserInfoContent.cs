﻿using System.Text.Json.Serialization;
using UnofficialArcaeaAPI.Lib.Models;

namespace UnofficialArcaeaAPI.Lib.Responses;

#pragma warning disable CS8618

public class UaaUserInfoContent
{
    [JsonPropertyName("account_info")]
    public AuaAccountInfo AccountInfo { get; set; }

    [JsonPropertyName("recent_score")]
    public UaaRecord[]? RecentScore { get; set; }

    [JsonPropertyName("songinfo")]
    public UaaChartInfo[]? SongInfo { get; set; }
}