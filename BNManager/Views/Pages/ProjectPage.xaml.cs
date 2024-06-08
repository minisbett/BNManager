using BNManager.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BNManager.Views.Pages;

internal sealed partial class ProjectPage : Page
{
  private ProjectViewModel ViewModel { get; set; }

  public ProjectPage()
  {
    InitializeComponent();
  }

  protected override void OnNavigatedTo(NavigationEventArgs e)
  {
    base.OnNavigatedTo(e);

    // Set the view model to the project passed as parameter.
    ViewModel = e.Parameter as ProjectViewModel;
  }
}
