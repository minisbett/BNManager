using BNManager.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BNManager.Views.Pages;

/// <summary>
/// The page representing a singular <see cref="Project"/>.
/// </summary>
internal sealed partial class ProjectPage : Page
{
  public ProjectPage()
  {
    InitializeComponent();
  }

  protected override void OnNavigatedTo(NavigationEventArgs e)
  {
    base.OnNavigatedTo(e);

    // Set the project in the view model to the navigation parameter.
    ViewModel.Project = e.Parameter as Project;
  }
}
