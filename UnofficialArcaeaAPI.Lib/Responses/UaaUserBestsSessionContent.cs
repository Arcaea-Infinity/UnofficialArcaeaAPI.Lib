namespace UnofficialArcaeaAPI.Lib.Responses;

public sealed class UaaUserBestsSessionContent
{
    public string SessionInfo { get; init; } = null!;

    public bool IsCacheSession { get; internal set; }
}