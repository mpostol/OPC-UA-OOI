//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.IO;

namespace UAOOI.Configuration.Core
{
  /// <summary>
  /// Gets access to the server configuration file editor.
  /// </summary>
  public interface IConfiguration: IDisposable
  {

    /// <summary>
    /// Creates the default configuration.
    /// </summary>
    void CreateDefaultConfiguration();

    /// <summary>
    /// Reads the configuration.
    /// </summary>
    /// <param name="configurationFile">The configuration file.</param>
    void ReadConfiguration( FileInfo configurationFile );

    /// <summary>
    /// Gets the configuration editor - user interface to edit the plug-in configuration file.
    /// </summary>
    /// <returns>
    /// Represents a window or dialog box that makes up an application's user interface to be used to edit configuration file.
    /// </returns>
    void EditConfiguration();
    
    /// <summary>
    /// Saves the configuration file to a specified location.
    /// </summary>
    /// <param name="solutionFilePath">The solution file path.</param>
    /// <param name="configurationFile">The configuration file.</param>
    /// <remarks><paramref name="solutionFilePath"/> is to be used to create relative file path to configuration files used by the plug-in.</remarks>
    void SaveConfiguration( string solutionFilePath, FileInfo configurationFile );

    /// <summary>
    /// Gets the instance to be used by a user to configure the selected node.
    /// </summary>
    /// <param name="descriptor">Provides identifying description of the node to be configured.</param>
    /// <returns>
    /// Returned object provides access to the instance node configuration edition functionality.
    /// </returns>
    IInstanceConfiguration GetInstanceConfiguration( INodeDescriptor descriptor );

    /// <summary>
    /// Creates automatically the instance configurations on the best effort basis.
    /// </summary>
    /// <param name="descriptors">The descriptors of nodes.</param>
    /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> skip opening configuration file.</param>
    /// <param name="CancelWasPressed">if set to <c>true</c> cancel was pressed.</param>
    void CreateInstanceConfigurations( INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed );

    /// <summary>
    /// Occurs any time the configuration is modified.
    /// </summary>
    event EventHandler<UAServerConfigurationEventArgs> OnModified;

    /// <summary>
    /// Gets the default name of the file.
    /// </summary>
    /// <value>The default name of the file.</value>
    string DefaultFileName { get; }

  }

}
