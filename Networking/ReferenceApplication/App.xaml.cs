using System.Windows;

namespace UAOOI.Networking.ReferenceApplication
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    /// <summary>
    /// Handles the Startup event of the App control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
    private void App_Startup(object sender, StartupEventArgs e)
    {
      GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize();
      //Producer.OPCUAServerProducerSimulator.CreateDevice(_cbf, x => m_DisposableCollection.Add(x), y => m_Log.Add(y));
      AppBootstrapper _bootstrapper = new AppBootstrapper();
      _bootstrapper.Run();
    }
    /// <summary>
    /// Handles the Exit event of the App control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="ExitEventArgs"/> instance containing the event data.</param>
    private void App_Exit(object sender, ExitEventArgs e)
    {
      ReferenceApplication.Properties.Settings.Default.Save();
    }

  }
}
