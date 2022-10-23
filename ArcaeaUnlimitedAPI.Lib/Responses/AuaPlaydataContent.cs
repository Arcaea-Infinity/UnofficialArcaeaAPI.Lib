using System.Text.Json.Serialization;

namespace ArcaeaUnlimitedAPI.Lib.Responses;

#pragma warning disable CS8618

public class AuaPlaydataContent
{
    [JsonPropertyName("fscore")] public int FScore { get; set; }

    [JsonPropertyName("count")] public int Count { get; set; }
}