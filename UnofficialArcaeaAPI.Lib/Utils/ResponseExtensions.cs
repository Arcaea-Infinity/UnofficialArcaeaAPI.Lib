using UnofficialArcaeaAPI.Lib.Responses;

namespace UnofficialArcaeaAPI.Lib.Utils;

internal static class ResponseExtensions
{
    /// <summary>
    /// Because these status code has additional data, we should 
    /// </summary>
    internal static bool HasAdditionalData(this UaaResponse response)
    {
        return response.Status is <= -31 and >= -33;
    }
}