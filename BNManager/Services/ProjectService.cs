using BNManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BNManager.Services;

/// <summary>
/// A static service for managing projects.
/// </summary>
internal static class ProjectService
{
  /// <summary>
  /// The file path for the projects.json file.
  /// </summary>
  private static readonly string _projectsFile = Path.Combine(
      Environment.GetEnvironmentVariable("localappdata"),
      "BNManager", "projects.json"
  );

  /// <summary>
  /// The list of all loaded projects.
  /// </summary>
  private static List<Project> _projects = new List<Project>();

  /// <summary>
  /// An list of all loaded projects.
  /// </summary>
  public static IReadOnlyList<Project> Projects => _projects.AsReadOnly();

  /// <summary>
  /// An event that is triggered when a project is created.
  /// </summary>
  public static event EventHandler<Project> ProjectCreated;

  /// <summary>
  /// An event that is triggered when a project is deleted.
  /// </summary>
  public static event EventHandler<Project> ProjectDeleted;

  /// <summary>
  /// Loads all projects from the projects.json file and caches them in <see cref="Projects"/>.
  /// </summary>
  /// <returns>The projects.</returns>
  public static void Initialize()
  {
    // Ensure the directory and projects file exist.
    Directory.CreateDirectory(Path.GetDirectoryName(_projectsFile));
    if (!File.Exists(_projectsFile))
      File.WriteAllText(_projectsFile, "[]");

    // Load all projects from the projects folder.
    _projects = JsonConvert.DeserializeObject<List<Project>>(File.ReadAllText(_projectsFile));
  }

  /// <summary>
  /// Creates a new project with the specified beatmap set and returns it.
  /// </summary>
  /// <param name="beatmapSet">The beatmap set of the project.</param>
  public static void Create(BeatmapSet beatmapSet)
  {
    // Create a new project and add it to the list of projects.
    Project project = new Project(beatmapSet.Id, Utils.GetSanitizedString(beatmapSet.Title), beatmapSet.Beatmaps.Select(x => x.Mode).Distinct().ToArray());
    _projects.Add(project);

    // Populate the project with all current nominators.
    foreach (Nominator nominator in MappersGuildService.Nominators)
      project.NominatorStates.Add(new NominatorState(nominator.Id));

    // Save the projects.json file to immediately reflect the changes and invoke the ProjectCreated event.
    Save();
    ProjectCreated?.Invoke(null, project);
  }

  /// <summary>
  /// Deletes the specified project.
  /// </summary>
  /// <param name="project">The project.</param>
  public static void Delete(Project project)
  {
    _projects.Remove(project);
    Save();
    ProjectDeleted?.Invoke(null, project);
  }

  /// <summary>
  /// Saves all projects to the projects.json file.
  /// </summary>
  public static void Save() => File.WriteAllText(_projectsFile, JsonConvert.SerializeObject(_projects, Formatting.Indented));
}
