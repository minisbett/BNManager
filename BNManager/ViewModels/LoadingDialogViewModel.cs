using CommunityToolkit.Mvvm.ComponentModel;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the <see cref="Views.LoadingDialog"/> view.
/// </summary>
internal partial class LoadingDialogViewModel : ObservableObject
{
  /// <summary>
  /// The info text to be displayed.
  /// </summary>
  [ObservableProperty]
  private string _infoText;
}
