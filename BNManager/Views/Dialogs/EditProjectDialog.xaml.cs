using BNManager.Enums;
using BNManager.Models;
using BNManager.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;

namespace BNManager.Views.Dialogs;

internal sealed partial class EditProjectDialog : ContentDialog
{
  /// <summary>
  /// Creates a new instance of <see cref="EditProjectDialog"/> with the specified default values.
  /// </summary>
  /// <param name="project">The existing project, used to pre-fill values.</param>
  public EditProjectDialog(Project project)
  {
    InitializeComponent();
    XamlRoot = App.MainWindow.Content.XamlRoot;
    RequestedTheme = ConfigService.Config.DarkMode ? ElementTheme.Dark : ElementTheme.Light;

    Loaded += (sender, e) =>
    {
      ViewModel.BeatmapSetId = project.BeatmapSetId;
      ViewModel.Name = project.Name;
      ViewModel.OriginalName = project.Name;
      ViewModel.StdEnabled = project.Modes.Contains(Mode.Standard);
      ViewModel.TaikoEnabled = project.Modes.Contains(Mode.Taiko);
      ViewModel.CtbEnabled = project.Modes.Contains(Mode.Catch);
      ViewModel.ManiaEnabled = project.Modes.Contains(Mode.Mania);
      ViewModel.Genre = project.Genre;
      ViewModel.Language = project.Language;
    };
  }
}
