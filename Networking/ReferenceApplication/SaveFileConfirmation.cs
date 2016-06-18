using UAOOI.Networking.ReferenceApplication.Controls;

namespace UAOOI.Networking.ReferenceApplication
{
  internal class SaveFileConfirmation : INotification
  {
    #region INotification
    /// <summary>
    /// Gets or sets the content of the notification.
    /// </summary>
    /// <value>The content of the interaction control.</value>
    public object Content
    {
      get; set;
    }
    /// <summary>
    /// Gets or sets the title to use for the notification.
    /// </summary>
    /// <value>The title of the interaction control.</value>
    public string Title
    {
      get; set;
    }
    /// <summary>
    /// Gets or sets the file path.
    /// </summary>
    /// <value>The file path.</value>
    public string FilePath { get; set; }
    #endregion
  }
}
