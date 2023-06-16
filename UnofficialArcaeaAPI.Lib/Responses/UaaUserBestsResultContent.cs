using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Responses;

public class UaaUserBestsResultContent : UaaUserBestsContent
{
    [JsonPropertyName("query_time")]
    public long QueryTime { get; init; }
}