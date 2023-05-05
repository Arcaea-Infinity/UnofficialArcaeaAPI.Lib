using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Responses;

public class UaaSongListContent
{
    [JsonPropertyName("songs")]
    public UaaSongInfoContent[] Songs { get; set; }
}