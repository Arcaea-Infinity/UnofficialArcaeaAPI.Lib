using System.Text.Json.Serialization;

#pragma warning disable CS8618

namespace UnofficialArcaeaAPI.Lib.Responses;

public sealed record UaaDataCertContent
{
    [JsonPropertyName("entry")]
    public string Entry { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("cert")]
    public string Cert { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}