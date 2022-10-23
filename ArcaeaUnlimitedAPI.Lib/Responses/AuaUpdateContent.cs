using System.Text.Json.Serialization;

namespace ArcaeaUnlimitedAPI.Lib.Responses;

#pragma warning disable CS8618

public class AuaUpdateContent
{
    [JsonPropertyName("url")] public string Url { get; set; }

    [JsonPropertyName("version")] public string Version { get; set; }
}