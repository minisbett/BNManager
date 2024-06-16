using BNManager.Services;
using Microsoft.UI.Xaml;
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
    XamlRoot = App.MainWindow.Content.XamlRoot;
    RequestedTheme = ConfigService.Config.DarkMode ? ElementTheme.Dark : ElementTheme.Light;
  }
}
