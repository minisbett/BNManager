using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views.Dialogs;

public sealed partial class LoadingDialog : ContentDialog
{
  /// <summary>
  /// The info text displayed in the loading dialog.
  /// </summary>
  public string InfoText
  {
    get => InfoTextBlock.Text;
    set => InfoTextBlock.Text = value;
  }

  public LoadingDialog()
  {
    InitializeComponent();
    XamlRoot = MainWindow.XamlRoot;
  }
}
