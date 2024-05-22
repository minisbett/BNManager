using BNManager.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views
{
  public sealed partial class MainPage : Page
  {
    public static new XamlRoot XamlRoot { get; private set; }

    public UIElement AppTitleBar => TitleBar;

    public MainPage()
    {
      InitializeComponent();

      Loaded += (sender, e) =>
      {
        // Make the xaml root available so that it can show dialogs.
        XamlRoot = Content.XamlRoot;
      };
    }
  }
}
