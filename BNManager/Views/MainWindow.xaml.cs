using Microsoft.UI.Xaml;
using Windows.Graphics;

namespace BNManager.Views;

public sealed partial class MainWindow : Window
{
  /// <summary>
  /// The XamlRoot for the main window of the application.
  /// </summary>
  public static XamlRoot XamlRoot { get; private set; }

  public MainWindow()
  {
    InitializeComponent();

    // Set the default app size and set up the title bar.
    AppWindow.Resize(new SizeInt32(1024, 768));
    ExtendsContentIntoTitleBar = true;
    SetTitleBar(MainPage.AppTitleBar);

    // As soon as the main page loaded, make the XamlRoot available to the whole application for dialogs.
    MainPage.Loaded += (_, _) => XamlRoot = MainPage.XamlRoot;
  }
}
