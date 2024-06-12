using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views.Dialogs;

public sealed partial class CreateProjectDialog : ContentDialog
{
  public CreateProjectDialog()
  {
    InitializeComponent();
    XamlRoot = MainWindow.XamlRoot;
  }
}
