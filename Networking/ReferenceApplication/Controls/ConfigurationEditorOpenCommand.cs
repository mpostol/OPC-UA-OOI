
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using UAOOI.Configuration.DataBindings.Common;

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
    /// Initializes a new instance of the <see cref="ConfigurationEditorOpenCommand" /> class.
    /// </summary>
    /// <param name="fileName">Name of the file to be edited.</param>
    /// <param name="saveResponse">Delegate capturing functionality to go back to the caller and collect information if and where the modified configuration is to be saved. 
    /// If the function return null the save operation must be skipped.</param>
    public ConfigurationEditorOpenCommand(string fileName, Func<FileInfo, FileInfo> saveResponse)
    {
      m_FileName = fileName;
      m_SaveResponse = saveResponse;
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
        string _assemblyLocatioen = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        _filePath = Path.Combine(_assemblyLocatioen, m_FileName);
        FileInfo _configurationFileInfo = new FileInfo(_filePath);
        if (!_configurationFileInfo.Exists)
          throw new FileNotFoundException();
        string _editorPath = Path.Combine(_assemblyLocatioen, Properties.Settings.Default.ConfigurationEditorPlugInFilePath);
        FileInfo _editorFileInfo = new FileInfo(_editorPath);
        if (_editorFileInfo.Exists)
          _editor = CreateInstance(_editorFileInfo);
        if (_editor != null)
        {
          bool _configurationChanged = false;
          _editor.ReadConfiguration(_configurationFileInfo);
          _editor.OnModified += (x, y) => _configurationChanged = true;
          _editor.EditConfiguration();
          if (_configurationChanged)
          {
            FileInfo _res = m_SaveResponse(_configurationFileInfo);
            if (_res != null)
              _editor.SaveConfiguration(String.Empty, _res);
          }
        }
        else
          using (Process process = Process.Start(_filePath)) { }
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

    #region private
    private string m_FileName;
    private Func<FileInfo, FileInfo> m_SaveResponse;
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
    #endregion

  }
}
