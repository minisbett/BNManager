using BNManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the edit project dialog view.
/// </summary>
internal partial class EditProjectDialogViewModel : ObservableObject
{
  /// <summary>
  /// The ID of the beatmap set.
  /// </summary>
  [ObservableProperty]
  private int _beatmapSetId;

  /// <summary>
  /// The name of the project.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(HasRequiredInput))]
  [NotifyPropertyChangedFor(nameof(ProjectWithNameAlreadyExists))]
  private string _name = "";

  /// <summary>
  /// Bool whether the project targets the standard game mode.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(HasRequiredInput))]
  private bool _stdEnabled = false;

  /// <summary>
  /// Bool whether the project targets the taiko game mode.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(HasRequiredInput))]
  private bool _taikoEnabled = false;

  /// <summary>
  /// Bool whether the project targets the catch game mode.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(HasRequiredInput))]
  private bool _ctbEnabled = false;

  /// <summary>
  /// Bool whether the project targets the mania game mode.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(HasRequiredInput))]
  private bool _maniaEnabled = false;

  /// <summary>
  /// The placeholder for the name input field, displaying the original name.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(ProjectWithNameAlreadyExists))]
  private string _originalName = "";

  /// <summary>
  /// Bool whether a project with the same name already exists.
  /// </summary>
  public bool ProjectWithNameAlreadyExists => ProjectService.Projects.Count(p => p.Name == Utils.GetSanitizedString(Name)) 
    > (Utils.GetSanitizedString(Name) == OriginalName ? 1 : 0);

  /// <summary>
  /// Bool whether all input field requirements are met.
  /// </summary>
  public bool HasRequiredInput => !string.IsNullOrWhiteSpace(Name) && (StdEnabled || TaikoEnabled || CtbEnabled || ManiaEnabled);
}
