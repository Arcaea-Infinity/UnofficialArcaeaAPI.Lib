using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Responses;

public sealed class UaaUserBestsSessionContent
{
    [JsonPropertyName("session_info")]
    public string SessionInfo { get; init; } = null!;

    public bool IsCacheSession { get; internal set; }
}