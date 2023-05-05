using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Responses;

#pragma warning disable CS8618

public class UaaDataUpdateContent
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }
}