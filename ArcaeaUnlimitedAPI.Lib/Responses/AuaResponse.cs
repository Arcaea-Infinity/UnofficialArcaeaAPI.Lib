using System.Text.Json.Serialization;

namespace ArcaeaUnlimitedAPI.Lib.Responses;

public class AuaResponse<TContent>
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("content")]
    public TContent? Content { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

public class AuaResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}