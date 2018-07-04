
using System;

namespace UAOOI.Configuration.Core
{
  /// <summary>
  /// Instance of this class provides more information on the type of the changes made in the server configuration.
  /// </summary>
  public class UAServerConfigurationEventArgs: EventArgs
  {
    /// <summary>
    /// Gets or sets a value indicating whether the configuration file has been changed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the configuration file has benn changed; otherwise, <c>false</c>.
    /// </value>
    public bool ConfigurationFileChanged { get; private set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="UAServerConfigurationEventArgs"/> class.
    /// </summary>
    /// <param name="fileChanged">
    /// if set to <c>true</c> indicated that the configuration file has been changed 
    /// and user interface must be regenerated.
    /// </param>
    public UAServerConfigurationEventArgs( bool fileChanged )
    {
      ConfigurationFileChanged = fileChanged;
    }
  }
}
