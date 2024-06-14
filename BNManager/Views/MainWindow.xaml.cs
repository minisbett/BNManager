using Microsoft.UI.Xaml;
using Windows.Graphics;

namespace BNManager.Views;

public sealed partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();

    // Set the default app size and set up the title bar.
    AppWindow.Resize(new SizeInt32(1024, 768));
    ExtendsContentIntoTitleBar = true;
    SetTitleBar(MainPage.AppTitleBar);
  }
}
