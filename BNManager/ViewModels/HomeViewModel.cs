using BNManager.Services;
using BNManager.Views;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the home page.
/// </summary>
internal partial class HomeViewModel
{
  /// <summary>
  /// A command for opening the create project dialog, and creating a new project.
  /// </summary>
  [RelayCommand]
  private async Task CreateProject()
  {
    CreateProjectDialog cpd = new CreateProjectDialog()
    {
      XamlRoot = MainWindow.XamlRoot
    };

    if (await cpd.ShowAsync() == ContentDialogResult.Primary)
      ProjectService.Create((cpd.DataContext as CreateProjectDialogViewModel).BeatmapSet);
  }
}