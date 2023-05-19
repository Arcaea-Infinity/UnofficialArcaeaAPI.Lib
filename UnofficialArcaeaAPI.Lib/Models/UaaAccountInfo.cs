using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Models;

#pragma warning disable CS8618

public class UaaAccountInfo
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("is_mutual")]
    public bool IsMutual { get; set; }

    [JsonPropertyName("is_char_uncapped_override")]
    public bool IsCharUncappedOverride { get; set; }

    [JsonPropertyName("is_char_uncapped")]
    public bool IsCharUncapped { get; set; }

    [JsonPropertyName("is_skill_sealed")]
    public bool IsSkillSealed { get; set; }

    [JsonPropertyName("rating")]
    public int Rating { get; set; }

    [JsonPropertyName("join_date")]
    public long JoinDate { get; set; }

    [JsonPropertyName("character")]
    public int Character { get; set; }
}