using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Responses;

#pragma warning disable CS8618

public class UaaPlaydataContent
{
    [JsonPropertyName("fscore")]
    public int FScore { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}