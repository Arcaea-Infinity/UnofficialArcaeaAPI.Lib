using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Responses;

public sealed class UaaResponse<TContent> : UaaResponse
{
    [JsonPropertyName("content")]
    public TContent? Content { get; set; }
}

public class UaaResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}