using BNManager.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace BNManager.Services;

/// <summary>
/// A static service for managing the configuration.
/// </summary>
internal static class ConfigService
{
  /// <summary>
  /// The file path for the config.json file.
  /// </summary>
  private static readonly string _configFile = Path.Combine(
      Environment.GetEnvironmentVariable("localappdata"),
      "BNManager", "config.json"
  );

  /// <summary>
  /// The static config instance.
  /// </summary>
  public static Config Config { get; private set; } = new Config();

  /// <summary>
  /// Loads the config from the config.json file.
  /// </summary>
  public static void Load()
  {
    Directory.CreateDirectory(Path.GetDirectoryName(_configFile));

    // Load the config from the config file if it exists.
    if (File.Exists(_configFile))
      Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(_configFile));

    // Write the config back to the config file to ensure it contains all properties.
    Save();
  }

  /// <summary>
  /// Saves the config to the config.json file.
  /// </summary>
  public static void Save() => File.WriteAllText(_configFile, JsonConvert.SerializeObject(Config));
}
