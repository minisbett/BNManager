using BNManager.Models;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace BNManager.ViewModels;

/// <summary>
/// Represents a beatmap in a view.
/// </summary>
internal class BeatmapViewModel
{
  /// <summary>
  /// The backing beatmap.
  /// </summary>
  private readonly Beatmap _beatmap;

  /// <summary>
  /// The mode icon/glyph in the osu extra font for the mode of the beatmap.
  /// </summary>
  public string ModeIcon => _beatmap.Mode.GetModeIcon();

  /// <summary>
  /// The display text (or tooltip) for the displayed beatmap.
  /// </summary>
  public string DisplayText => $"{_beatmap.Version} ({_beatmap.DifficultyRating:n1}★)";

  /// <summary>
  /// The difficulty rating color for the beatmap.
  /// </summary>
  public Brush DifficultyColor => new SolidColorBrush(_beatmap.DifficultyRating switch
  {
    >= 9 => Colors.Black,
    >= 7.7 => Color.FromArgb(255, 24, 21, 142),
    >= 6.7 => Color.FromArgb(255, 101, 99, 222),
    >= 5.8 => Color.FromArgb(255, 198, 69, 184),
    >= 4.9 => Color.FromArgb(255, 255, 78, 111),
    >= 4.2 => Color.FromArgb(255, 255, 128, 104),
    >= 3.3 => Color.FromArgb(255, 246, 240, 92),
    >= 2.5 => Color.FromArgb(255, 124, 255, 79),
    >= 2.0 => Color.FromArgb(255, 79, 255, 213),
    >= 1.25 => Color.FromArgb(255, 79, 192, 255),
    _ => Color.FromArgb(255, 66, 144, 251)
  });

  /// <summary>
  /// Creates a new instance of <see cref="BeatmapViewModel"/> with the specified beatmap to represent.
  /// </summary>
  /// <param name="beatmap">The beatmap to represent.</param>
  public BeatmapViewModel(Beatmap beatmap)
  {
    _beatmap = beatmap;
  }
}
