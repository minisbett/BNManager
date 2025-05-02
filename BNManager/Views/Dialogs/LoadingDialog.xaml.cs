using BNManager.Services;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

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

  /// <summary>
  /// Considers the loading process as failed and displays an error message, along with an action button.
  /// </summary>
  /// <param name="message">The error message.</param>
  /// <param name="action">The action.</param>
  public void Fail(string message, string action, bool isActionEnabled)
  {
    ProgressRing.Visibility = Visibility.Collapsed;
    InfoTextBlock.Foreground = new SolidColorBrush(Colors.IndianRed);
    InfoTextBlock.Text = message;
    ErrorActionButton.Visibility = Visibility.Visible;
    ErrorActionButton.Content = action;
    ErrorActionButton.IsEnabled = isActionEnabled;
  }

  private void ErrorActionButton_Click(object sender, RoutedEventArgs e)
  {
    Hide();
  }
}
