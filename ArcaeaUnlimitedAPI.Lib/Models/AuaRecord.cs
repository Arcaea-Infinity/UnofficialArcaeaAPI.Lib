using System.Text.Json.Serialization;

namespace ArcaeaUnlimitedAPI.Lib.Models;

#pragma warning disable CS8618

public class AuaRecord
{
    [JsonPropertyName("user_id")] public int? UserId { get; set; }

    [JsonPropertyName("score")] public int Score { get; set; }

    [JsonPropertyName("health")] public int? Health { get; set; }

    [JsonPropertyName("rating")] public double Rating { get; set; }

    [JsonPropertyName("song_id")] public string SongId { get; set; }

    [JsonPropertyName("modifier")] public int? Modifier { get; set; }

    [JsonPropertyName("difficulty")] public int Difficulty { get; set; }

    [JsonPropertyName("clear_type")] public int? ClearType { get; set; }

    [JsonPropertyName("best_clear_type")] public int? BestClearType { get; set; }

    [JsonPropertyName("time_played")] public long TimePlayed { get; set; }

    [JsonPropertyName("near_count")] public int NearCount { get; set; }

    [JsonPropertyName("miss_count")] public int MissCount { get; set; }

    [JsonPropertyName("perfect_count")] public int PerfectCount { get; set; }

    [JsonPropertyName("shiny_perfect_count")] public int ShinyPerfectCount { get; set; }
}