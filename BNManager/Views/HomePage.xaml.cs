using BNManager.Services;
using BNManager.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;

namespace BNManager.Views;

public sealed partial class HomePage : Page
{
  public HomePage()
  {
    InitializeComponent();
  }

  /// <summary>
  /// Opens a dialog to create a new project, creating it if the user confirms.
  /// </summary>
  private async void CreateProject_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    CreateProjectDialog cpd = new CreateProjectDialog()
    {
      XamlRoot = MainWindow.XamlRoot
    };

    if (await cpd.ShowAsync() == ContentDialogResult.Primary)
      ProjectService.Create((cpd.DataContext as CreateProjectDialogViewModel).BeatmapSet);
  }
}
