namespace UnofficialArcaeaAPI.Lib.Core;

public sealed class UaaImageApi
{
    public UaaImageUserApi User { get; }

    internal UaaImageApi(HttpClient client)
    {
        User = new UaaImageUserApi(client);
    }
}