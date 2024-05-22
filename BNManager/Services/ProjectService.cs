using BNManager.Enums;
using BNManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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

  private static List<Project> _projects = new List<Project>();

  /// <summary>
  /// An list of all loaded projects.
  /// </summary>
  public static IReadOnlyList<Project> Projects => _projects.AsReadOnly();

  /// <summary>
  /// Loads all projects from the projects.json file and caches them in <see cref="Projects"/>.
  /// </summary>
  /// <returns>The projects.</returns>
  public static void Initialize()
  {
    if (!File.Exists(_projectsFile))
      File.WriteAllText(_projectsFile, "[]");

    // Load all projects from the projects folder.
    _projects = JsonConvert.DeserializeObject<List<Project>>(File.ReadAllText(_projectsFile));
  }

  /// <summary>
  /// Creates a new project with the given name and returns it.
  /// </summary>
  /// <param name="name">The name of the project.</param>
  /// <param name="modes">The targetted modes of the project.</param>
  public static Project Create(string name, Mode[] modes)
  {
    // Create a new project and add it to the list of projects.
    Project project = new Project(Utils.GetSanitizedString(name), modes);
    _projects.Add(project);

    // Populate the project with all current nominators.
    foreach (Nominator nominator in MappersGuildService.Nominators)
      project.NominatorStates.Add(new NominatorState(nominator.Id));

    // Save the projects.json file and return the created project.
    Save();
    return project;
  }

  /// <summary>
  /// Deletes the specified project.
  /// </summary>
  /// <param name="project">The project.</param>
  public static void Delete(Project project)
  {
    _projects.Remove(project);
    Save();
  }

  /// <summary>
  /// Saves all projects to the projects.json file.
  /// </summary>
  public static void Save()
  {
    string json = JsonConvert.SerializeObject(_projects, Formatting.Indented);
    File.WriteAllText(_projectsFile, json);
  }
}
