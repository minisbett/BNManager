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

  [JsonConstructor]
  private Nominator(string[] requestStatus)
  {
    RequestStatus = requestStatus.Select(x => Enum.Parse<RequestStatus>(x, true)).ToArray();
  }
}