using BNManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace BNManager.ViewModels;

/// <summary>
/// The main view model for the application.
/// </summary>
internal partial class MainViewModel : ObservableObject
{
  /// <summary>
  /// The project view models.
  /// </summary>
  public ObservableCollection<ProjectViewModel> Projects { get; } = new ObservableCollection<ProjectViewModel>();

  /// <summary>
  /// The currently selected item in the navigation view.
  /// </summary>
  [ObservableProperty]
  private object _selectedItem;

  public MainViewModel()
  {
    // If a project was created, add it to the tracked view models and select it.
    ProjectService.ProjectCreated += (_, project) =>
    {
      Projects.Add(new ProjectViewModel(project));
      SelectedItem = Projects.Last();
    };

    // If a project was deleted, remove it from the tracked view models.
    ProjectService.ProjectDeleted += (_, project) => Projects.Remove(Projects.First(viewModel => viewModel.Equals(project)));
  }
}