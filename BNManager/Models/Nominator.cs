using BNManager.Enums;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace BNManager.Models;

/// <summary>
/// Represents a beatmap nominator on the mapper's guild API.
/// </summary>
internal class Nominator
{
  /// <summary>
  /// The osu! ID of the nominator.
  /// </summary>
  [JsonProperty("osuId")]
  public int Id { get; private set; }

  /// <summary>
  /// The osu! username of the nominator.
  /// </summary>
  [JsonProperty("username")]
  public string Name { get; private set; }

  /// <summary>
  /// Info about the group of the nominator in each mode.
  /// </summary>
  [JsonProperty("modesInfo")]
  public ModeInfo[] ModesInfo { get; private set; }

  /// <summary>
  /// The request status of the nominator.
  /// </summary>
  public RequestStatus[] RequestStatus { get; }

  /// <summary>
  /// The link to the nominator's request queue.
  /// </summary>
  [JsonProperty("requestLink")]
  public string RequestLink { get; private set; } = "";

  /// <summary>
  /// A nominator-specific info on how to request.
  /// </summary>
  [JsonProperty("requestInfo")]
  public string RequestInfo { get; private set; } = "";

  /// <summary>
  /// The genre preferences of the nominator.
  /// </summary>
  [JsonProperty("genrePreferences")]
  public string[] GenrePreferences { get; private set; }

  /// <summary>
  /// The negative genre preferences of the nominator.
  /// </summary>
  [JsonProperty("genreNegativePreferences")]
  public string[] GenreNegativePreferences { get; private set; }

  /// <summary>
  /// The language preferences of the nominator.
  /// </summary>
  [JsonProperty("languagePreferences")]
  public string[] LanguagePreferences { get; private set; }

  /// <summary>
  /// The negative language preferences of the nominator.
  /// </summary>
  [JsonProperty("languageNegativePreferences")]
  public string[] LanguageNegativePreferences { get; private set; }

  /// <summary>
  /// The mapper preferences of the nominator.
  /// </summary>
  [JsonProperty("mapperPreferences")]
  public string[] MapperPreferences { get; private set; }

  /// <summary>
  /// The negative mapper preferences of the nominator.
  /// </summary>
  [JsonProperty("mapperNegativePreferences")]
  public string[] MapperNegativePreferences { get; private set; }

  /// <summary>
  /// The detail preferences of the nominator.
  /// </summary>
  [JsonProperty("detailPreferences")]
  public string[] DetailPreferences { get; private set; }

  /// <summary>
  /// The negative detail preferences of the nominator.
  /// </summary>
  [JsonProperty("detailNegativePreferences")]
  public string[] DetailNegativePreferences { get; private set; }

  /// <summary>
  /// The osu!std style preferences of the nominator.
  /// </summary>
  [JsonProperty("osuStylePreferences")]
  public string[] OsuStylePreferences { get; private set; }

  /// <summary>
  /// The negative osu! style preferences of the nominator.
  /// </summary>
  [JsonProperty("osuStyleNegativePreferences")]
  public string[] OsuStyleNegativePreferences { get; private set; }

  /// <summary>
  /// The osu!taiko style preferences of the nominator.
  /// </summary>
  [JsonProperty("taikoStylePreferences")]
  public string[] TaikoStylePreferences { get; private set; }

  /// <summary>
  /// The negative osu!taiko style preferences of the nominator.
  /// </summary>
  [JsonProperty("taikoStyleNegativePreferences")]
  public string[] TaikoStyleNegativePreferences { get; private set; }

  /// <summary>
  /// The osu!catch style preferences of the nominator.
  /// </summary>
  [JsonProperty("catchStylePreferences")]
  public string[] CatchStylePreferences { get; private set; }

  /// <summary>
  /// The negative osu!catch style preferences of the nominator.
  /// </summary>
  [JsonProperty("catchStyleNegativePreferences")]
  public string[] CatchStyleNegativePreferences { get; private set; }

  /// <summary>
  /// The mania osu!style preferences of the nominator.
  /// </summary>
  [JsonProperty("maniaStylePreferences")]
  public string[] ManiaStylePreferences { get; private set; }

  /// <summary>
  /// The negative osu!mania style preferences of the nominator.
  /// </summary>
  [JsonProperty("maniaStyleNegativePreferences")]
  public string[] ManiaStyleNegativePreferences { get; private set; }

  [JsonConstructor]
  private Nominator(string[] requestStatus)
  {
    RequestStatus = requestStatus.Select(x => Enum.Parse<RequestStatus>(x, true)).ToArray();
  }
}