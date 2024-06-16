using BNManager.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BNManager.Services;

/// <summary>
/// A static service for handling interaction with the Mino API.
/// </summary>
internal static class MinoApiService
{
  /// <summary>
  /// The HTTP client used for the service.
  /// </summary>
  private static readonly HttpClient _http = new HttpClient();

  /// <summary>
  /// Returns the beatmap set with the specified ID. If an error occured, null is returned.
  /// </summary>
  /// <param name="beatmapSetId">The beatmap set ID.</param>
  /// <returns>The beatmap set or null if an error occured.</returns>
  public static async Task<BeatmapSet> GetBeatmapSetAsync(int beatmapSetId)
  {
    // Send the request and return null if it failed (eg. beatmap set not found).
    HttpResponseMessage response = await _http.GetAsync($"https://catboy.best/api/v2/s/{beatmapSetId}");
    if (!response.IsSuccessStatusCode)
      return null;

    // Deserialize the JSON response and return the beatmap set.
    string json = await response.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<BeatmapSet>(json);
  }
}
