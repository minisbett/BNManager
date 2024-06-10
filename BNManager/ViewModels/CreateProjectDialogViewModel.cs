using BNManager.Models;
using BNManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the create project dialog.
/// </summary>
internal partial class CreateProjectDialogViewModel : ObservableObject
{
  /// <summary>
  /// The regex pattern for the beatmap set URL.
  /// </summary>
  private const string BEATMAPSET_URL_REGEX = @"(?<=https:\/\/osu\.ppy\.sh\/beatmapsets\/)\d+";

  /// <summary>
  /// A dispatcher timer for fetching the beatmap set with a delay.
  /// </summary>
  private readonly DispatcherTimer _fetchTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1000) };

  /// <summary>
  /// The entered beatmap set ID or URL.
  /// </summary>
  [ObservableProperty]
  private string _beatmapSetIdInput;

  /// <summary>
  /// An optional error message displayed.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(HasErrorMessage))]
  private string _errorMessage;

  /// <summary>
  /// The parsed beatmap set.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(HasValidBeatmapSet))]
  [NotifyPropertyChangedFor(nameof(Cover))]
  [NotifyPropertyChangedFor(nameof(Beatmaps))]
  private BeatmapSet _beatmapSet;

  /// <summary>
  /// The view models for the beatmaps in the <see cref="BeatmapSet"/>.
  /// </summary>
  public BeatmapViewModel[] Beatmaps => BeatmapSet?.Beatmaps.OrderBy(b => b.Mode)
                                                            .ThenBy(x => x.DifficultyRating)
                                                            .Select(x => new BeatmapViewModel(x))
                                                            .ToArray();

  /// <summary>
  /// A URL to the cover image of the beatmap set.
  /// </summary>
  public string Cover => BeatmapSet is null ? " " : $"https://assets.ppy.sh/beatmaps/{BeatmapSet.Id}/covers/cover.jpg";

  /// <summary>
  /// Bool whether an error message exists. This shows/hides the error message text block.
  /// </summary>
  public bool HasErrorMessage => ErrorMessage is not null;

  /// <summary>
  /// Bool whether the beatmap set is currently being fetched, showing/hiding the loading animation.
  /// </summary>
  [ObservableProperty]
  private bool _isLoading;

  /// <summary>
  /// Bool whether a valid beatmap set was fetched. This enables/disables the create button and shows/hides the beatmap set preview.
  /// </summary>
  public bool HasValidBeatmapSet => BeatmapSet is not null;

  public CreateProjectDialogViewModel()
  {
    _fetchTimer.Tick += fetchTimer_Tick;
  }

  private async void fetchTimer_Tick(object sender, object e)
  {
    // Stop the timer.
    _fetchTimer.Stop();

    // Parse the beatmap set ID and fetch the beatmap set.
    int id = int.TryParse(BeatmapSetIdInput, out int _id) ? _id : int.Parse(Regex.Match(BeatmapSetIdInput, BEATMAPSET_URL_REGEX).Value);
    BeatmapSet = await MinoApiService.GetBeatmapSetAsync(id);
    IsLoading = false;

    // Show an error message if the beatmap set was not found.
    if (BeatmapSet is null)
      ErrorMessage = "Beatmap set not found.";
  }

  partial void OnBeatmapSetIdInputChanged(string value)
  {
    // Reset the UI and stop the fetch delay timer.
    _fetchTimer.Stop();
    ErrorMessage = null;
    BeatmapSet = null;
    IsLoading = false;

    // Ensure the input is a valid beatmap set ID or URL and display an error message if necessary.
    if (value is "")
      return;
    if (!int.TryParse(value, out _) && !Regex.IsMatch(value, BEATMAPSET_URL_REGEX))
    {
      ErrorMessage = "Invalid input.";
      return;
    }

    // Start the loading animation and timer to fetch the beatmap set delayed.
    IsLoading = true;
    _fetchTimer.Start();
  }
}
