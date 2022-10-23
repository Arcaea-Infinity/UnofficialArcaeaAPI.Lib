using System.Text.Json.Serialization;

namespace ArcaeaUnlimitedAPI.Lib.Responses;

public class AuaSongListContent
{
    [JsonPropertyName("songs")] public AuaSongInfoContent[] Songs { get; set; }
}