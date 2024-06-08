using BNManager.Services;
using BNManager.Views.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace BNManager.ViewModels;

/// <summary>
/// The main view model for the application.
/// </summary>
internal partial class MainViewModel : ObservableObject
{
  /// <summary>
  /// The project navigation view items for the navigation view.
  /// </summary>
  public ObservableCollection<ProjectNavigationViewItem> ProjectNavigationItems { get; } = new ObservableCollection<ProjectNavigationViewItem>();

  /// <summary>
  /// The item currently selected in the navigation view.
  /// </summary>
  [ObservableProperty]
  private NavigationViewItem _selectedItem;

  public MainViewModel()
  {
    // If a project was created, add it to the tracked view models and select it.
    ProjectService.ProjectCreated += (_, project) =>
    {
      ProjectNavigationViewItem item = new ProjectNavigationViewItem(project);
      ProjectNavigationItems.Add(item);
      SelectedItem = item;
    };

    // If a project was deleted, remove it from the tracked view models.
    ProjectService.ProjectDeleted += (_, project) =>
    {
      ProjectNavigationItems.Remove(ProjectNavigationItems.FirstOrDefault(p => p.Project == project));
    };
  }
}