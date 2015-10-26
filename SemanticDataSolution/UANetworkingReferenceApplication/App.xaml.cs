using System.Windows;
using System.Windows.Navigation;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnLoadCompleted(NavigationEventArgs e)
    {
      Application _ca = Current;
      IModelViewBindingFactory _cbf = (IModelViewBindingFactory)_ca.MainWindow.DataContext;
      Consumer.OPCUADataModel _model = new Consumer.OPCUADataModel() { ModelViewBindingFactory = _cbf };
      _model.Run();
      base.OnLoadCompleted(e);
    }
  }
}
