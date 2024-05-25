using BNManager.Services;
using BNManager.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Foundation;

namespace BNManager.ViewModels;

/// <summary>
/// The main view model for the application.
/// </summary>
internal partial class MainViewModel : ObservableObject
{
  /// <summary>
  /// The project view models.
  /// </summary>
  [ObservableProperty]
  private ObservableCollection<ProjectViewModel> _projects;

  /// <summary>
  /// The content frame of the main page.
  /// </summary>
  private Frame _contentFrame;

  /// <summary>
  /// The currently selected item.
  /// </summary>
  [ObservableProperty]
  private object _selectedItem;

  public MainViewModel(Frame contentFrame)
  {
    _contentFrame = contentFrame;
    Projects = new ObservableCollection<ProjectViewModel>(ProjectService.Projects.Select(p => new ProjectViewModel(p)));
  }

  /// <summary>
  /// Changes the content frame to display the correct page, depending on the selected item.
  /// </summary>
  /// <param name="value"></param>
  partial void OnSelectedItemChanged(object value)
  {
    // If a project is selected, navigate to the project page.
    if (value is ProjectViewModel project)
      _contentFrame.Navigate(typeof(ProjectPage), project);
    // Otherwise, navigate to the home page.
    else
      _contentFrame.Navigate(typeof(HomePage));
  }
}