
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace UAOOI.Networking.ReferenceApplication.Controls
{
  /// <summary>
  /// Class OpenFileCommand - open selected file using external <see cref="Process"/>
  /// </summary>
  /// <seealso cref="System.Windows.Input.ICommand" />
  public class OpenFileCommand : ICommand
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenFileCommand"/> class.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    public OpenFileCommand(string fileName)
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
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    public void Execute(object parameter)
    {
      string path = string.Empty;
      try
      {
        path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        using (Process process = Process.Start(Path.Combine(path, m_FileName))) { }
      }
      catch (Exception _ex)
      {
        MessageBox.Show($"An error occurs during opening the file {path}. Error message: {_ex}", "Problem with opening a file !", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
    }
    #endregion

    #region private
    private string m_FileName;
    #endregion

  }
}
