using UnofficialArcaeaAPI.Lib.Core;

namespace UnofficialArcaeaAPI.Lib;

public sealed class UaaClient
{
    public UaaUserApi User { get; }
    public UaaSongApi Song { get; }
    public UaaAssetsApi Assets { get; }
    public UaaDataApi Data { get; }

    public UaaClient(UaaClientOptions options)
    {
        HttpClient client;

        if (options.HttpClient is not null)
        {
            client = options.HttpClient;
        }
        else
        {
            if (options.ApiUrl is null)
                throw new UaaException("If UaaClientOptions.HttpClient is not provided, " +
                                       "UaaClientOptions.ApiUrl must be set.");

            client = new HttpClient
            {
                BaseAddress = new Uri(options.ApiUrl.EndsWith("/") ? options.ApiUrl : options.ApiUrl + "/"),
                Timeout = options.Timeout
            };

            if (options.Token is not null)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + options.Token);

            if (options.UserAgent is not null)
                client.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
        }

        User = new UaaUserApi(client);
        Song = new UaaSongApi(client);
        Assets = new UaaAssetsApi(client);
        Data = new UaaDataApi(client);
    }
}