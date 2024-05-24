using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views;

public sealed partial class LoadingDialog : ContentDialog
{
  /// <summary>
  /// The info text displayed in the loading dialog.
  /// </summary>
  public string InfoText
  {
    get => ViewModel.InfoText;
    set => ViewModel.InfoText = value;
  }

  public LoadingDialog()
  {
    InitializeComponent();
  }
}
