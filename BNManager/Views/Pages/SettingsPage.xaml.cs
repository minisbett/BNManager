using BNManager.Services;
using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsPage : Page
{
  public SettingsPage()
  {
    InitializeComponent();
  }

  private void BackgroundOpacitySlider_LostFocus(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    // Only save the config when the focus of the slider is lost, since performing this operation on every value change leads to high I/O.
    // Changing the in-memory value on LostFocus as well causes issues when switching from focus to a project page, as the event order unfavors this.
    ConfigService.Save();
  }
}
