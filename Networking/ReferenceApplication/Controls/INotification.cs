namespace UAOOI.Networking.ReferenceApplication.Controls
{

  /// <summary>
  /// Represents an interaction request used for notifications.
  /// </summary>
  public interface INotification
  {

    /// <summary>
    /// Gets or sets the content of the notification.
    /// </summary>
    /// <value>The content of the interaction control.</value>
    object Content { get; set; }
    /// <summary>
    /// Gets or sets the title to use for the notification.
    /// </summary>
    /// <value>The title of the interaction control.</value>
    string Title { get; set; }

  }
}