using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Responses;

public sealed class UaaUserBestsResultContent : UaaUserBestsContent
{
    [JsonPropertyName("query_time")]
    public long QueryTime { get; init; }
    
    [JsonPropertyName("queried_charts")]
    internal int QueriedCharts { get; init; }
    
    [JsonPropertyName("current_account")]
    internal int CurrentAccount { get; init; }
}