using UnofficialArcaeaAPI.Lib.Utils;

namespace UnofficialArcaeaAPI.Lib;

/// <summary>
/// Options for customizing <see cref="UaaClient"/>.
/// </summary>
public sealed class UaaClientOptions
{
    /// <summary>
    /// Base API url. Muse be specified, or an <see cref="UaaRequestException"/> will be thrown.
    /// </summary>
    public string? ApiUrl { get; set; }

    /// <summary>
    /// Bearer token for API authentication. Does not need to include <c>"Bearer "</c> part.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Custom User-Agent header for API request.
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Timeout for API request. Defaults to 60 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60);

    /// <summary>
    /// Custom <see cref="HttpClient"/> instance. If specified, all customization will not being performed.
    /// </summary>
    public HttpClient? HttpClient { get; set; }
}