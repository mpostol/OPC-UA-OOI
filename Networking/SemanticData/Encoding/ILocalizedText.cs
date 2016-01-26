
namespace UAOOI.Networking.SemanticData.Encoding
{
  /// <summary>
  /// Interface ILocalizedText - human readable qualified with a locale.
  /// </summary>
  public interface ILocalizedText
  {

    /// <summary>
    /// Gets the locale used to create the text.
    /// </summary>
    /// <value>The locale.</value>
    string Locale { get; }
    /// <summary>
    /// Gets the localized text.
    /// </summary>
    /// <value>The localized text.</value>
    string Text { get; }

  }
}
