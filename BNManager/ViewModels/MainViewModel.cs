using BNManager.Models;
using BNManager.Services;
using BNManager.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BNManager.ViewModels;

/// <summary>
/// The main view model for the application.
/// </summary>
internal partial class MainViewModel : ObservableObject
{
  /// <summary>
  /// A list of all project view models.
  /// </summary>
  private readonly List<ProjectViewModel> _projects;

  /// <summary>
  /// The search query used to filter projects.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(FilteredProjects))]
  private string _searchQuery = "";

  /// <summary>
  /// The currently selected project.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(IsProjectSelected))]
  private ProjectViewModel _selectedProject;

  /// <summary>
  /// Bool whether a project is selected.
  /// </summary>
  public bool IsProjectSelected => SelectedProject is not null;

  /// <summary>
  /// All project view models, filtered by the search query.
  /// </summary>
  public IEnumerable<ProjectViewModel> FilteredProjects => _projects
    .Where(x => SearchQuery.ToLower().Split(' ').All(tag => x.Name.Contains(tag)));

  public MainViewModel()
  {
    _projects = ProjectService.Projects.Select(x => new ProjectViewModel(x, DeleteProjectCallback)).ToList();
  }

  /// <summary>
  /// A command for opening the create project dialog, and creating a new project.
  /// </summary>
  [RelayCommand]
  private async Task CreateProject()
  {
    CreateProjectDialog cpd = new CreateProjectDialog()
    {
      XamlRoot = MainPage.XamlRoot
    };

    if (await cpd.ShowAsync() == ContentDialogResult.Primary)
    {
      CreateProjectDialogViewModel vm = cpd.DataContext as CreateProjectDialogViewModel;

      // Create the project, add the view model to the list of projects, update the UI and select the new project.
      Project project = ProjectService.Create(vm.BeatmapSet);
      _projects.Add(SelectedProject = new ProjectViewModel(project, DeleteProjectCallback));
      OnPropertyChanged(nameof(FilteredProjects));
    }
  }

  /// <summary>
  /// Callback for the deletion of a project, used to remove the view model and update the UI.
  /// </summary>
  /// <param name="vm">The view model of the project.</param>
  private void DeleteProjectCallback(ProjectViewModel vm)
  {
    _projects.Remove(vm);
    if (SelectedProject == vm)
      SelectedProject = null;

    OnPropertyChanged(nameof(FilteredProjects));
  }
}
