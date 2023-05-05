namespace UnofficialArcaeaAPI.Lib.Tests;

public abstract class TestBase
{
    public static string? ApiUrl => Environment.GetEnvironmentVariable("UAA_API_URL");
    public static string? Token => Environment.GetEnvironmentVariable("UAA_TOKEN");
    public static string? UserAgent => "UnofficialArcaeaAPI.Lib Unit Test";
    public static TimeSpan Timeout => TimeSpan.FromSeconds(60);

    public static UaaClient DefaultClient => new(new UaaClientOptions
    {
        ApiUrl = ApiUrl,
        Token = Token,
        UserAgent = UserAgent,
        Timeout = Timeout,
    });
}