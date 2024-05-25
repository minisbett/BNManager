using BNManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace BNManager.ViewModels;

/// <summary>
/// The main view model for the application.
/// </summary>
internal partial class MainViewModel : ObservableObject
{
  /// <summary>
  /// The project view models.
  /// </summary>
  public object[] Projects => ProjectService.Projects.Select(p => new ProjectViewModel(p)).ToArray();
}