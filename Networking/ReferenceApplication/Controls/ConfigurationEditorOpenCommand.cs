
using CAS.UA.IServerConfiguration;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace UAOOI.Networking.ReferenceApplication.Controls
{
  /// <summary>
  /// Class ConfigurationEditorOpenCommand - Implements <see cref="ICommand"/> open an external configuration editor. 
  /// If external editor cannot be found associated process is executed. 
  /// </summary>
  /// <seealso cref="System.Windows.Input.ICommand" />
  public class ConfigurationEditorOpenCommand : ICommand
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationEditorOpenCommand"/> class.
    /// </summary>
    /// <param name="fileName">Name of the file to be edited.</param>
    public ConfigurationEditorOpenCommand(string fileName)
    {
      m_FileName = fileName;
    }

    #region ICommand
    /// <summary>
    /// Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged;
    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public bool CanExecute(object parameter)
    {
      return true;
    }
    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
    public void Execute(object parameter)
    {
      string _filePath = string.Empty;
      IConfiguration _editor = null;
      try
      {
        string _asseblyLocatioen = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        _filePath = Path.Combine(_asseblyLocatioen, m_FileName);
        FileInfo _configurationFileInfo = new FileInfo(_filePath);
        if (!_configurationFileInfo.Exists)
          throw new FileNotFoundException();
        string _editorPath = Path.Combine(_asseblyLocatioen, Properties.Settings.Default.ConfigurationEditorPlugInSybfolder);
        FileInfo _editorFileInfo = new FileInfo(_editorPath);
        if (_editorFileInfo.Exists)
          _editor = CreateInstance(_editorFileInfo);
        if (_editor != null)
        {
          _editor.ReadConfiguration(_configurationFileInfo);
          _editor.EditConfiguration();
        }
        else
          using (Process process = Process.Start(Path.Combine(_filePath, m_FileName))) { }
      }
      catch (Exception _ex)
      {
        MessageBox.Show($"An error occurs during opening the file {_filePath}. Error message: {_ex}", "Problem with opening a file !", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
      finally
      {
        IDisposable _toDispose = _editor as IDisposable;
        if (_toDispose != null)
          _toDispose.Dispose();
      }
    }
    #endregion

    private string m_FileName;
    private static IConfiguration CreateInstance(FileInfo assemblyFile)
    {
      Assembly _pluginAssembly = Assembly.LoadFrom(assemblyFile.FullName);
      IConfiguration _serverConfiguration;
      string iName = typeof(IConfiguration).ToString();
      _serverConfiguration = null;
      foreach (Type pluginType in _pluginAssembly.GetExportedTypes())
        //Only look at public types
        if (pluginType.IsPublic && !pluginType.IsAbstract && pluginType.GetInterface(iName) != null)
        {
          _serverConfiguration = (IConfiguration)Activator.CreateInstance(pluginType);
          break;
        }
      return _serverConfiguration;
    }
  }
}
