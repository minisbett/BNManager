using BNManager.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views.Dialogs;

internal sealed partial class NominatorStateInfoDialog : ContentDialog
{
  /// <summary>
  /// The view model for the nominator state info dialog.
  /// </summary>
  private NominatorStateViewModel ViewModel { get; }

  /// <summary>
  /// Creates a new instance of <see cref="NominatorStateInfoDialog"/> with the given view model.
  /// </summary>
  /// <param name="state">The view model.</param>
  public NominatorStateInfoDialog(NominatorStateViewModel state)
  {
    InitializeComponent();
    XamlRoot = App.MainWindow.Content.XamlRoot;

    ViewModel = state;
  }
}
