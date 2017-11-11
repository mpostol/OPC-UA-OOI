
using Microsoft.Win32;
using System.Windows;
using UAOOI.Networking.ReferenceApplication.Controls;
using System.ComponentModel.Composition;

namespace UAOOI.Networking.ReferenceApplication
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  [Export()]
  public partial class MainWindow : Window
  {

    public MainWindow()
    {
      InitializeComponent();
    }
    [Import]
    internal MainWindowViewModel MainWindowViewModel
    {
      set { DataContext = value; }
      get { return DataContext as MainWindowViewModel; }
    }
    private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
      this.Close();
    }
    private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      MainWindowViewModel _vm = this.DataContext as MainWindowViewModel;
      if (_vm == null)
        return;
      _vm.SaveFileInteractionEvent += _vmSaveFileInteractionEvent;
    }
    private void _vmSaveFileInteractionEvent(object sender, InteractionRequestedEventArgs e)
    {
      SaveFileConfirmation _confirmation = e.Context as SaveFileConfirmation;
      if (_confirmation == null)
        return;
      string _msg = $"Click Yes to save configuration to {_confirmation.FilePath}, No to slecet new file, Cancel to cancel";
      //switch (MessageBox.Show(_confirmation.Title, _msg, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel))
      switch (MessageBox.Show(_msg, _confirmation.Title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel))
      {
        case MessageBoxResult.None:
        case MessageBoxResult.OK:
        case MessageBoxResult.Yes:
          break;
        case MessageBoxResult.Cancel:
          _confirmation.FilePath = string.Empty;
          break;
        case MessageBoxResult.No:
          OpenFileDialog _dialog = new OpenFileDialog()
          {
            AddExtension = true,
            CheckPathExists = true,
            DefaultExt = ".xml",
            Filter = "Configuration (.xml)|*.xml",
            FileName = _confirmation.FilePath,
            Title = "Save file as ..",
            CheckFileExists = false,
            ValidateNames = true,  
          };
          _confirmation.FilePath = _dialog.ShowDialog().GetValueOrDefault(false) ? _dialog.FileName : string.Empty;
          e.Callback();
          break;
        default:
          break;
      }
    }

  }
}
