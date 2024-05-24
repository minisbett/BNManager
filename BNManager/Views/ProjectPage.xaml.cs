using BNManager.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views;

internal sealed partial class ProjectPage : Page
{
  private ProjectViewModel ViewModel { get; }

  public ProjectPage(ProjectViewModel projectViewModel)
  {
    ViewModel = projectViewModel;

    InitializeComponent();
  }
}
