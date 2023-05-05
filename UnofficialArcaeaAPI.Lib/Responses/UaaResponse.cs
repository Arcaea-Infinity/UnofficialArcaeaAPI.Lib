using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Responses;

public class UaaResponse<TContent>
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("content")]
    public TContent? Content { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

public class UaaResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}