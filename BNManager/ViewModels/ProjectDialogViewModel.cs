using BNManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the project dialog view.
/// </summary>
internal partial class ProjectDialogViewModel : ObservableObject
{
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
  /// Bool whether the dialog is in create or edit mode.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(DialogTitle))]
  [NotifyPropertyChangedFor(nameof(PrimaryButtonText))]
  private bool _isCreateMode;

  /// <summary>
  /// The original, unmodified name of the project. Used for name change detection in edit mode.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(ProjectWithNameAlreadyExists))]
  [NotifyPropertyChangedFor(nameof(NamePlaceholder))]
  private string _originalName = "";

  /// <summary>
  /// The dialog title, depending on the create/edit mode.
  /// </summary>
  public string DialogTitle => IsCreateMode ? "Create Project" : "Edit Project";

  /// <summary>
  /// The text of the primary button, depending on the create/edit mode.
  /// </summary>
  public string PrimaryButtonText => IsCreateMode ? "Create" : "Save";

  /// <summary>
  /// The placeholder for the name input field, either displaying an example or the original name.
  /// </summary>
  public string NamePlaceholder => OriginalName == "" ? "eg. 'No Title'" : OriginalName;

  /// <summary>
  /// Bool whether a project with the same name already exists.
  /// </summary>
  public bool ProjectWithNameAlreadyExists => Utils.GetSanitizedString(Name) != OriginalName && ProjectService.Projects.Any(p => p.Name == Utils.GetSanitizedString(Name));

  /// <summary>
  /// Bool whether all input field requirements are met.
  /// </summary>
  public bool HasRequiredInput => !string.IsNullOrWhiteSpace(Name) && (StdEnabled || TaikoEnabled || CtbEnabled || ManiaEnabled);
}
