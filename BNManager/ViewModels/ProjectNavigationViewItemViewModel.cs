using BNManager.Enums;
using BNManager.Models;
using BNManager.Services;
using BNManager.Views.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the <see cref="Views.Controls.ProjectNavigationViewItem"/>.
/// </summary>
internal partial class ProjectNavigationViewItemViewModel : ObservableObject
{
  /// <summary>
  /// The project the navigation view item represents.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(Cover))]
  private Project _project;

  /// <summary>
  /// The cover image for the beatmap set associated with the project.
  /// </summary>
  public string Cover => $"https://assets.ppy.sh/beatmaps/{Project.BeatmapSetId}/covers/cover.jpg";

  /// <summary>
  /// Prompts the user to edit a project.
  /// </summary>
  [RelayCommand]
  private async Task EditProject()
  {
    EditProjectDialog pd = new EditProjectDialog(Project);
    if (await pd.ShowAsync() == ContentDialogResult.Primary)
    {
      EditProjectDialogViewModel pdvm = pd.DataContext as EditProjectDialogViewModel;

      // Get a list of all targetted modes.
      List<Mode> modes = new List<Mode>();
      if (pdvm.StdEnabled) modes.Add(Mode.Standard);
      if (pdvm.TaikoEnabled) modes.Add(Mode.Taiko);
      if (pdvm.CtbEnabled) modes.Add(Mode.Catch);
      if (pdvm.ManiaEnabled) modes.Add(Mode.Mania);

      // Update the project and UI and save.
      Project.Name = pdvm.Name;
      Project.Modes = modes.ToArray();
      Project.Genre = pdvm.Genre;
      Project.Language = pdvm.Language;
      OnPropertyChanged(nameof(Project));
      ProjectService.Save();
    }
  }

  /// <summary>
  /// Prompts the user to confirm the deletion of the project.
  /// </summary>
  [RelayCommand]
  private async Task DeleteProject()
  {
    if (await new ContentDialog()
    {
      XamlRoot = App.MainWindow.Content.XamlRoot,
      RequestedTheme = ConfigService.Config.DarkMode ? ElementTheme.Dark : ElementTheme.Light,
      Title = "Delete Project",
      Content = $"Are you sure you want to delete this project?\n\n{Project.Name}",
      PrimaryButtonText = "Delete",
      CloseButtonText = "Cancel"
    }.ShowAsync() == ContentDialogResult.Primary)
      ProjectService.Delete(Project);
  }

  /// <summary>
  /// Opens the beatmap set in the web browser.
  /// </summary>
  [RelayCommand]
  private void OpenBeatmapSetInWeb()
  {
    Process.Start(new ProcessStartInfo()
    {
      FileName = $"https://osu.ppy.sh/s/{Project.BeatmapSetId}",
      UseShellExecute = true
    });
  }
}
