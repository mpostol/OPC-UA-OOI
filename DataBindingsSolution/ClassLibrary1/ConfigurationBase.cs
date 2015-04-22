using CAS.UA.IServerConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAS.UAOOI.DataBindings
{
  /// <summary>
  /// Class ConfigurationBase - Provides basic implementation of the <see cref="IConfiguration"/>.
  /// </summary>
  public class ConfigurationBase: IConfiguration
  {

    /// <summary>
    /// Occurs any time the configuration is modified.
    /// </summary>
    public event EventHandler<UAServerConfigurationEventArgs> OnModified;

    /// <summary>
    /// Creates the default configuration.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void CreateDefaultConfiguration()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Creates automatically the instance configurations on the best effort basis.
    /// </summary>
    /// <param name="descriptors">The descriptors of nodes.</param>
    /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> skip opening configuration file.</param>
    /// <param name="CancelWasPressed">if set to <c>true</c> cancel was pressed.</param>
    /// <exception cref="NotImplementedException"></exception>
    public void CreateInstanceConfigurations(UA.IServerConfiguration.INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the default name of the file.
    /// </summary>
    /// <value>The default name of the file.</value>
    /// <exception cref="NotImplementedException"></exception>
    public string DefaultFileName
    {
      get { throw new NotImplementedException(); }
    }

    /// <summary>
    /// Gets the configuration editor - user interface to edit the plug-in configuration file.
    /// </summary>
    /// <returns>Represents a window or dialog box that makes up an application's user interface to be used to edit configuration file.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public void EditConfiguration()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the instance to be used by a user to configure the selected node.
    /// </summary>
    /// <param name="descriptor">Provides identifying description of the node to be configured.</param>
    /// <returns>Returned object provides access to the instance node configuration edition functionality.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Reads the configuration.
    /// </summary>
    /// <param name="configurationFile">The configuration file.</param>
    /// <exception cref="NotImplementedException"></exception>
    public void ReadConfiguration(System.IO.FileInfo configurationFile)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Saves the configuration file to a specified location.
    /// </summary>
    /// <param name="solutionFilePath">The solution file path.</param>
    /// <param name="configurationFile">The configuration file.</param>
    /// <exception cref="NotImplementedException"></exception>
    /// <remarks><paramref name="solutionFilePath" /> is to be used to create relative file path to configuration files used by the plug-in.</remarks>
    public void SaveConfiguration(string solutionFilePath, System.IO.FileInfo configurationFile)
    {
      throw new NotImplementedException();
    }
  }
}
