using BNManager.Models;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using System;
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
  public Brush DifficultyColor
  {
    get
    {
      // The difficulty ratings and their corresponding colors.
      double[] difficultyRatings = { 0.1, 1.25, 2, 2.5, 3.3, 4.2, 4.9, 5.8, 6.7, 7.7, 9 };
      Color[] range = {
            Color.FromArgb(255, 66, 144, 251),
            Color.FromArgb(255, 79, 192, 255),
            Color.FromArgb(255, 79, 255, 213),
            Color.FromArgb(255, 124, 255, 79),
            Color.FromArgb(255, 246, 240, 92),
            Color.FromArgb(255, 255, 128, 104),
            Color.FromArgb(255, 255, 78, 111),
            Color.FromArgb(255, 198, 69, 184),
            Color.FromArgb(255, 101, 99, 222),
            Color.FromArgb(255, 24, 21, 142),
            Color.FromArgb(255, 0, 0, 0)
        };

      // Find the last index of the difficulty rating that is lower than the beatmap's difficulty rating.
      // If none is found, return the last color in the range. If the first, return the first color in the range.
      int index = Array.FindIndex(difficultyRatings, value => _beatmap.DifficultyRating < value);
      if (index == -1) return new SolidColorBrush(range[^1]);
      else if (index == 0) return new SolidColorBrush(range[0]);

      // Calculate the portion of the color between the two difficulty ratings.
      double proportion = (_beatmap.DifficultyRating - difficultyRatings[index - 1]) / (difficultyRatings[index] - difficultyRatings[index - 1]);

      // Interpolate the color between the two colors based on the proportion and return it.
      byte red = (byte)(range[index - 1].R + (range[index].R - range[index - 1].R) * proportion);
      byte green = (byte)(range[index - 1].G + (range[index].G - range[index - 1].G) * proportion);
      byte blue = (byte)(range[index - 1].B + (range[index].B - range[index - 1].B) * proportion);
      return new SolidColorBrush(Color.FromArgb(255, red, green, blue));
    }
  }


  /// <summary>
  /// Creates a new instance of <see cref="BeatmapViewModel"/> with the specified beatmap to represent.
  /// </summary>
  /// <param name="beatmap">The beatmap to represent.</param>
  public BeatmapViewModel(Beatmap beatmap)
  {
    _beatmap = beatmap;
  }
}
