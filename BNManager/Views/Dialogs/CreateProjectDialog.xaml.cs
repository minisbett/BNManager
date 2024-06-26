using BNManager.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views.Dialogs;

public sealed partial class CreateProjectDialog : ContentDialog
{
  public CreateProjectDialog()
  {
    InitializeComponent();
    XamlRoot = App.MainWindow.Content.XamlRoot;
    RequestedTheme = ConfigService.Config.DarkMode ? ElementTheme.Dark : ElementTheme.Light;
  }
}
