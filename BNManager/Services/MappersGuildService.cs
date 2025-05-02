using BNManager.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BNManager.Services;

/// <summary>
/// A static service for loading all Beatmap Nominator using the Mappers' Guild API and caching them.
/// </summary>
internal static class MappersGuildService
{
  private static readonly string _nominatorsCacheFile = Path.Combine(
      Environment.GetEnvironmentVariable("localappdata"),
      "BNManager", "nominators_cache.json"
  );

  /// <summary>
  /// An array of cached Mappers' Guild Beatmap Nominators.
  /// </summary>
  public static Nominator[] Nominators { get; private set; }

  /// <summary>
  /// Loads the Beatmap Nominators from the Mappers' Guild API and caches them in <see cref="Nominators"/>.
  /// </summary>
  public static async Task InitializeAsync()
  {
    // Send the request to the API and parse the JSON response.
    using HttpClient http = new HttpClient();
    JObject json = JObject.Parse(await http.GetStringAsync($"https://bn.mappersguild.com/api/relevantInfo"));

    // Parse all users in all modes.
    Nominator[] nominators = json["allUsersByMode"].SelectMany(x => x["users"]).Select(x => x.ToObject<Nominator>()).ToArray();

    // If no nominators were found, the Mappers' Guild backend is probably down.
    if (nominators.Length == 0)
      throw new BnSiteDownException();

    // Distinct nominators by their ID such that we don't have duplicates.
    Nominators = nominators.GroupBy(x => x.Id).Select(x => x.First()).ToArray();

    // Save the nominators to the cache file.
    File.WriteAllText(_nominatorsCacheFile, JsonConvert.SerializeObject(Nominators));
  }

  /// <summary>
  /// Bool whether a cache file exists or not.
  /// </summary>
  public static bool IsCacheAvailable => File.Exists(_nominatorsCacheFile);

  /// <summary>
  /// Loads the Beatmap Nominators from the cache file and stores them in <see cref="Nominators"/>.
  /// </summary>
  public static void InitializeFromCache()
  {
    // Load all nominators from the cache file.
    Nominators = JsonConvert.DeserializeObject<Nominator[]>(File.ReadAllText(_nominatorsCacheFile));
  }

  /// <summary>
  /// Returns the <see cref="Nominator"/> with the specified ID or null if no such nominator exists.
  /// </summary>
  /// <param name="id">The ID of the nominator.</param>
  /// <returns>The nominator or null if not found.</returns>
  public static Nominator FromId(int id) => Nominators.FirstOrDefault(n => n.Id == id);
}

public class BnSiteDownException : Exception;