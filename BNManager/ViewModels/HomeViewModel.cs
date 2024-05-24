using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the home page.
/// </summary>
internal partial class HomeViewModel
{
  /// <summary>
  /// The command to create a new project, located in the <see cref="MainPage"/>.
  /// </summary>
  private IAsyncRelayCommand _createProjectCommand;

  public HomeViewModel(IAsyncRelayCommand createProjectCallback)
  {
    _createProjectCommand = createProjectCallback;
  }

  /// <summary>
  /// Runs the project creation command.
  /// </summary>
  /// <returns></returns>
  [RelayCommand]
  private async Task CreateProject() => await _createProjectCommand.ExecuteAsync(null);
}