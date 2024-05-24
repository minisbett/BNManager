using BNManager.Enums;
using BNManager.Models;
using Microsoft.UI.Xaml.Controls;
using System.Linq;

namespace BNManager.Views;

internal sealed partial class EditProjectDialog : ContentDialog
{
  /// <summary>
  /// Creates a new instance of <see cref="EditProjectDialog"/> with the specified default values.
  /// </summary>
  /// <param name="project">The existing project, used to pre-fill values.</param>
  public EditProjectDialog(Project project)
  {
    InitializeComponent();

    Loaded += (sender, e) =>
    {
      ViewModel.BeatmapSetId = project.BeatmapSetId;
      ViewModel.Name = project.Name;
      ViewModel.OriginalName = project.Name;
      ViewModel.StdEnabled = project.Modes.Contains(Mode.Standard);
      ViewModel.TaikoEnabled = project.Modes.Contains(Mode.Taiko);
      ViewModel.CtbEnabled = project.Modes.Contains(Mode.Catch);
      ViewModel.ManiaEnabled = project.Modes.Contains(Mode.Mania);
    };
  }
}
