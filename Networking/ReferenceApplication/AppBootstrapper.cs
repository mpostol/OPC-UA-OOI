
using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Windows;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.ReferenceApplication.Consumer;
using UAOOI.Networking.ReferenceApplication.MEF;
using UAOOI.Networking.ReferenceApplication.Producer;
using UAOOI.Networking.ReferenceApplication.Properties;

namespace UAOOI.Networking.ReferenceApplication
{
  internal class AppBootstrapper : MefBootstrapper
  {

    #region MefBootstrapper
    /// <summary>
    /// Run the bootstrapper process.
    /// </summary>
    /// <param name="runWithDefaultConfiguration">if set to <c>true</c> run with default configuration.</param>
    public override void Run(bool runWithDefaultConfiguration)
    {
      base.Run(runWithDefaultConfiguration);
      m_ConsumerConfigurationFactory.Setup();
      m_OPCUAServerProducerSimulator.Setup();
    }
    protected override void ConfigureContainer()
    {
      base.ConfigureContainer();
      this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Settings.Default.MessageHandlerProvider));
    }
    /// <summary>
    /// Initializes the shell.
    /// </summary>
    /// <remarks>The base implementation ensures the shell is composed in the container.</remarks>
    protected override void InitializeShell()
    {
      base.InitializeShell();
      Application.Current.MainWindow = (MainWindow)this.Shell;
      Application.Current.MainWindow.Show();
      m_ConsumerConfigurationFactory = Container.GetExportedValue<ConsumerDataManagementSetup>();
      m_OPCUAServerProducerSimulator = Container.GetExportedValue<OPCUAServerProducerSimulator>(); 
    }
    /// <summary>
    /// Creates the shell or main window of the application.
    /// </summary>
    /// <returns>The shell of the application.</returns>
    /// <remarks>If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
    /// <see cref="T:Prism.Bootstrapper" /> will attach the default <see cref="T:Prism.Regions.IRegionManager" /> of
    /// the application in its <see cref="F:Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
    /// in order to be able to add regions by using the <see cref="F:Prism.Regions.RegionManager.RegionNameProperty" />
    /// attached property from XAML.</remarks>
    protected override DependencyObject CreateShell()
    {
      return this.Container.GetExportedValue<MainWindow>();
    }
    #endregion

    internal static void RunInReleaseMode()
    {
      AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
      AppBootstrapper _bootstrapper;
      try
      {
        _bootstrapper = new AppBootstrapper();
        _bootstrapper.Run();
      }
      catch (Exception ex)
      {
        HandleException(ex);
        ITraceSource _logger = new TraceSourceBase();
        _logger.TraceData(TraceEventType.Critical, 51, $"Exception while composing the application: {ex}");
      }
    }
    private ConsumerDataManagementSetup m_ConsumerConfigurationFactory = null;
    private OPCUAServerProducerSimulator m_OPCUAServerProducerSimulator = null;
    private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      HandleException(e.ExceptionObject as Exception);
    }
    private static void HandleException(Exception ex)
    {
      if (ex == null)
        return;
      MessageBox.Show(Properties.Resources.UnhandledException, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
      Environment.Exit(1);
    }
  }
}
