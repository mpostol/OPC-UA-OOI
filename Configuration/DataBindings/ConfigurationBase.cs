//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________


using System;
using System.IO;
using UAOOI.Configuration.Core;
using UAOOI.Configuration.Networking;

namespace UAOOI.Configuration.DataBindings
{
  /// <summary>
  /// Class ConfigurationBase - Provides basic implementation of the <see cref="IConfiguration"/>.
  /// </summary>
  public abstract class ConfigurationBase<ConfigurationDataType> : UANetworkingConfiguration<ConfigurationDataType>, IConfiguration
    where ConfigurationDataType : class, IConfigurationDataFactory, new()
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationBase{ConfigurationDataType}"/> class.
    /// </summary>
    public ConfigurationBase()
    {
      base.OnModified += (x, y) => { OnModified?.Invoke(x, new UAServerConfigurationEventArgs(true)); };
    }
    /// <summary>
    /// Occurs any time the configuration is modified.
    /// </summary>
    /// <exception cref="System.NotImplementedException">
    /// </exception>
    public new event EventHandler<UAServerConfigurationEventArgs> OnModified;
    /// <summary>
    /// Gets the default name of the file.
    /// </summary>
    /// <value>The default name of the file.</value>
    public abstract string DefaultFileName { get; }
    /// <summary>
    /// Creates the default configuration.
    /// </summary>
    public abstract void CreateDefaultConfiguration();
    /// <summary>
    /// Creates automatically the instance configurations on the best effort basis.
    /// </summary>
    /// <param name="descriptors">The descriptors of nodes.</param>
    /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> skip opening configuration file.</param>
    /// <param name="CancelWasPressed">if set to <c>true</c> cancel was pressed.</param>
    public abstract void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed);
    /// <summary>
    /// Gets the configuration editor - user interface to edit the plug-in configuration file.
    /// </summary>
    /// <returns>Represents a window or dialog box that makes up an application's user interface to be used to edit configuration file.</returns>
    public abstract void EditConfiguration();
    /// <summary>
    /// Gets the instance to be used by a user to configure the selected node.
    /// </summary>
    /// <param name="descriptor">Provides identifying description of the node to be configured.</param>
    /// <returns>Returned object provides access to the instance node configuration edition functionality.</returns>
    public abstract IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor);
    /// <summary>
    /// Saves the configuration file to a specified location.
    /// </summary>
    /// <param name="solutionFilePath">The solution file path.</param>
    /// <param name="configurationFile">The configuration file.</param>
    /// <remarks><paramref name="solutionFilePath" /> is to be used to create relative file path to configuration files used by the plug-in.</remarks>
    public abstract void SaveConfiguration(string solutionFilePath, FileInfo configurationFile);

    #region IDisposable Support
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing) { }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion
  }
}
