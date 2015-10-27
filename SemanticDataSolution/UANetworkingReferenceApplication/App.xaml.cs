using System.Windows;
using System.Windows.Navigation;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    void App_Startup(object sender, StartupEventArgs e)
    {
      // Open a window
      MainWindow window = new MainWindow();
      IModelViewBindingFactory _cbf = (IModelViewBindingFactory)window.DataContext;
      Consumer.OPCUADataModel _model = new Consumer.OPCUADataModel() { ModelViewBindingFactory = _cbf };
      _model.Run();
      window.Show();
    }
  }
}
