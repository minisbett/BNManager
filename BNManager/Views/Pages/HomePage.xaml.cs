using BNManager.Services;
using BNManager.ViewModels;
using BNManager.Views.Dialogs;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace BNManager.Views.Pages;

public sealed partial class HomePage : Page
{
  public HomePage()
  {
    InitializeComponent();
  }

  /// <summary>
  /// Opens a dialog to create a new project, creating it if the user confirms.
  /// </summary>
  private async void CreateProject_Click(object sender, RoutedEventArgs e)
  {
    CreateProjectDialog cpd = new CreateProjectDialog();
    if (await cpd.ShowAsync() == ContentDialogResult.Primary)
      ProjectService.Create((cpd.DataContext as CreateProjectDialogViewModel).BeatmapSet);
  }

  private async void BnWebsite_Click(object sender, RoutedEventArgs e) => await Windows.System.Launcher.LaunchUriAsync(new Uri("https://bn.mappersguild.com/"));
}
