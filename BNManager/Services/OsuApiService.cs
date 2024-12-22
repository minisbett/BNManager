using BNManager.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BNManager.Services;

/// <summary>
/// A static service for handling interaction with any 3-rd party osu! API mirror.
/// </summary>
internal static class OsuApiService
{
  /// <summary>
  /// The mirrors used for the service, in order of priority. The $ID placeholder should be replaced with the beatmap set ID.
  /// </summary>
  private static readonly string[] _mirrors =
  {
    "https://catboy.best/api/v2/s/$ID", 
    "https://osu.direct/api/v2/s/$ID"
  };

    /// <summary>
    /// The HTTP client used for the service.
    /// </summary>
    private static readonly HttpClient _http = new HttpClient()
    {
        DefaultRequestHeaders = { { "User-Agent", $"bnmanager/{App.Version}" } }
    };

  /// <summary>
  /// Returns the beatmap set with the specified ID. If an error occured, null is returned.
  /// </summary>
  /// <param name="beatmapSetId">The beatmap set ID.</param>
  /// <returns>The beatmap set or null if an error occured.</returns>
  public static async Task<BeatmapSet> GetBeatmapSetAsync(int beatmapSetId, CancellationToken cancellationToken)
  {
    // Iterate through the mirrors and try to get the beatmap set.
    foreach (string mirror in _mirrors)
    {
      try
      {
        // Check if the operation was cancelled.
        if (cancellationToken.IsCancellationRequested)
          return null;

        // Send the request and abort if the beatmap was not found, or go to the next mirror if the request failed elsewise.
        HttpResponseMessage response = await _http.GetAsync(mirror.Replace("$ID", beatmapSetId.ToString()), cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
          break;
        else if (!response.IsSuccessStatusCode)
          continue;

        // Try to deserialize the JSON response and return the beatmap set.
        string json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<BeatmapSet>(json);
      }
      catch { continue; }
    }

    // If all mirrors failed, return null.
    return null;
  }
}
