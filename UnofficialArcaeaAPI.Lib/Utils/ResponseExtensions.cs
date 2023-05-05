using System.Net;
using System.Text.Json;
using UnofficialArcaeaAPI.Lib.Responses;

namespace UnofficialArcaeaAPI.Lib.Utils;

internal static class ResponseExtensions
{
    internal static async Task<byte[]> EnsureDataSuccess(this HttpResponseMessage resp)
    {
        if (resp.StatusCode != HttpStatusCode.OK)
        {
            var errJson = JsonSerializer.Deserialize<UaaResponse>(await resp.Content.ReadAsStringAsync())!;
            throw new UaaRequestException(errJson.Status, errJson.Message!);
        }

        return await resp.Content.ReadAsByteArrayAsync();
    }
}