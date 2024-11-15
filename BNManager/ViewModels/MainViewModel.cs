﻿using BNManager.Services;
using BNManager.Views.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI;

namespace BNManager.ViewModels;

/// <summary>
/// The main view model for the application.
/// </summary>
internal partial class MainViewModel : ObservableObject
{
  /// <summary>
  /// The unfiltered project navigation view items for the navigation view.
  /// </summary>
  public ObservableCollection<ProjectNavigationViewItem> ProjectNavigationItems { get; } = new ObservableCollection<ProjectNavigationViewItem>();

  /// <summary>
  /// The search query used to filter the project navigation items.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(FilteredProjectNavigationItems))]
  private string _searchQuery = "";

  /// <summary>
  /// The filtered project navigation items based on the search query.
  /// </summary>
  public IEnumerable<ProjectNavigationViewItem> FilteredProjectNavigationItems
    => ProjectNavigationItems.Where(p => SearchQuery.Split(' ').All(t => p.Project.Name.Contains(t, StringComparison.OrdinalIgnoreCase)));
    
  /// <summary>
  /// The item currently selected in the navigation view.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(SelectedProjectBeatmapCoverUrl))]
  [NotifyPropertyChangedFor(nameof(BeatmapBackgroundOpacity))]
  private NavigationViewItem _selectedItem;

  /// <summary>
  /// The URL of the beatmap cover of the selected project. If no project is selected (Home, Settings), this returns "msappx://". (valid, empty image source)
  /// </summary>
  public string SelectedProjectBeatmapCoverUrl => 
    SelectedItem is ProjectNavigationViewItem p ? $"https://assets.ppy.sh/beatmaps/{p.Project.BeatmapSetId}/covers/cover.jpg" : "msappx://";

  /// <summary>
  /// The opacity of the beatmap background displayed over the whole application.
  /// </summary>
  public double BeatmapBackgroundOpacity => ConfigService.Config.BackgroundOpacity / 100d;

  public MainViewModel()
  {
    // Update the filtered project navigation items when the source collection changes.
    ProjectNavigationItems.CollectionChanged += (_, _) => OnPropertyChanged(nameof(FilteredProjectNavigationItems));

    // If a project was created, add it to the tracked view models and select it.
    ProjectService.ProjectCreated += (_, project) =>
    {
      ProjectNavigationViewItem item = new ProjectNavigationViewItem(project);
      ProjectNavigationItems.Insert(0, item);
      SelectedItem = item;
    };

    // If a project was deleted, remove it from the tracked view models.
    ProjectService.ProjectDeleted += (_, project) =>
    {
      ProjectNavigationItems.Remove(ProjectNavigationItems.FirstOrDefault(p => p.Project == project));
    };
  }
}