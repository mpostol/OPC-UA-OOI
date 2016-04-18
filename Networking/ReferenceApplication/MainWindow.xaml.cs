using System.Windows;

namespace UAOOI.Networking.ReferenceApplication
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }
    private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
      this.Close();
    }
    private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }
  }
}
