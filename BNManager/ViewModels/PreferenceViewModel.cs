namespace BNManager.ViewModels;

/// <summary>
/// Represents a preference in the <see cref="Views.Dialogs.NominatorStateInfoDialog"/>.
/// </summary>
internal class PreferenceViewModel
{
  /// <summary>
  /// Bool whether the preference is positive (tendency towards) or negative (tedency against).
  /// </summary>
  public bool IsPositive { get; }

  /// <summary>
  /// The text of the preference. (eg. the genre or language)
  /// </summary>
  public string Text { get; }

  /// <summary>
  /// Creates a new instance of <see cref="PreferenceViewModel"/> with the specified state whether it's positive/negative and the text.
  /// </summary>
  /// <param name="isPositive"></param>
  /// <param name="text"></param>
  public PreferenceViewModel(bool isPositive, string text)
  {
    IsPositive = isPositive;
    Text = text;
  }
}
