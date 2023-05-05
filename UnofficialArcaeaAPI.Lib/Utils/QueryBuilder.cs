namespace UnofficialArcaeaAPI.Lib.Utils;

internal sealed class QueryBuilder
{
    private readonly Dictionary<string, string> _params = new();

    public QueryBuilder Add(string key, string value)
    {
        _params[key] = value;
        return this;
    }

    public string Build()
    {
        var queryString = _params.Aggregate("", (current, param) =>
            current + $"&{param.Key}={param.Value}");
        return "?" + queryString[1..];
    }
}