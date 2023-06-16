using System.Text.Json.Serialization;

namespace UnofficialArcaeaAPI.Lib.Responses;

internal sealed class UaaUserBestsResultContentInternal : UaaUserBestsResultContent
{
    [JsonPropertyName("queried_charts")]
    public int QueriedCharts { get; init; }

    [JsonPropertyName("current_account")]
    public int CurrentAccount { get; init; }
}