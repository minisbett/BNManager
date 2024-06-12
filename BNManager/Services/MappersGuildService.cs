using BNManager.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BNManager.Services;

/// <summary>
/// A static service for loading all Beatmap Nominator using the Mappers' Guild API and caching them.
/// </summary>
internal static class MappersGuildService
{
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

    // Distinct nominators by their ID such that we don't have duplicates.
    Nominators = nominators.GroupBy(x => x.Id).Select(x => x.First()).ToArray();
  }

  /// <summary>
  /// Returns the <see cref="Nominator"/> with the specified ID or null if no such nominator exists.
  /// </summary>
  /// <param name="id">The ID of the nominator.</param>
  /// <returns>The nominator or null if not found.</returns>
  public static Nominator FromId(int id) => Nominators.FirstOrDefault(n => n.Id == id);
}
