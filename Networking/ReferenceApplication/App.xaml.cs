
using System.Windows;
using UAOOI.Networking.ReferenceApplication.Properties;

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
      m_Bootstrapper.Run();
    }
    /// <summary>
    /// Handles the Exit event of the App control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="ExitEventArgs"/> instance containing the event data.</param>
    private void App_Exit(object sender, ExitEventArgs e)
    {
      m_Bootstrapper.Dispose();
      Settings.Default.Save();
    }
    AppBootstrapper m_Bootstrapper = new AppBootstrapper();

  }
}
