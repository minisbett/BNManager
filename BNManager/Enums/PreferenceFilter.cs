namespace BNManager.Enums;

/// <summary>
/// An enum containing the different preference filter options for filtering nominators.
/// </summary>
internal enum PreferenceFilter
{
  /// <summary>
  /// Filtered to only show nominators with either matching genre or language preferences.
  /// </summary>
  SoftPreferred,

  /// <summary>
  /// Filtered to only show nominators with matching genre and language preferences.
  /// </summary>
  ExactPreferred,

  /// <summary>
  /// Filtered to not show nominators with matching genre or language anti-preferences.
  /// </summary>
  NoAntiPreferred
}
