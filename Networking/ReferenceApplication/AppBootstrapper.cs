
using System;
using System.ComponentModel.Composition.Hosting;
using System.Reactive.Disposables;
using System.Windows;
using UAOOI.Networking.ReferenceApplication.Consumer;
using UAOOI.Networking.ReferenceApplication.Diagnostic;
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
    }

    /// <summary>
    /// Creates the shell or main window of the application.
    /// </summary>
    /// <returns>The shell of the application as an exported instance of the <see cref="MainWindow" /></returns>
    protected override DependencyObject CreateShell()
    {
      return this.Container.GetExportedValue<MainWindow>();
    }
    protected override void OnInitialized()
    {
      try
      {
        base.OnInitialized();
        m_EventSourceBootstrapper = Container.GetExportedValue<EventSourceBootstrapper>();
        m_EventSourceBootstrapper.Run();
        ReferenceApplicationEventSource.Log.StartingApplication(Settings.Default.MessageHandlerProvider);
        ConsumerDataManagementSetup m_ConsumerConfigurationFactory = Container.GetExportedValue<ConsumerDataManagementSetup>();
        m_ConsumerConfigurationFactory.Setup();
        m_Components.Add(m_ConsumerConfigurationFactory);
        ReferenceApplicationEventSource.Log.PartCreated(nameof(ConsumerDataManagementSetup));
        OPCUAServerProducerSimulator m_OPCUAServerProducerSimulator = Container.GetExportedValue<OPCUAServerProducerSimulator>();
        m_OPCUAServerProducerSimulator.Setup();
        m_Components.Add(m_OPCUAServerProducerSimulator);
        ReferenceApplicationEventSource.Log.PartCreated(nameof(OPCUAServerProducerSimulator));
      }
      catch (Exception _ex)
      {
        ReferenceApplicationEventSource.Log.EnteringMethod(nameof(AppBootstrapper), $"{nameof(AppBootstrapper)} at catch (Exception _ex)");
        ReferenceApplicationEventSource.Log.LogException(_ex);
        throw;
      }
    }
    #endregion

    #region IDisposable
    protected override void Dispose(bool disposing)
    {
      ReferenceApplicationEventSource.Log.EnteringDispose(nameof(AppBootstrapper), disposing);
      m_Components.Dispose();
      base.Dispose(disposing);
      //to keep logging it must be disposed as the last operation.
      m_EventSourceBootstrapper.Dispose();
    }
    #endregion

    #region private 
    private CompositeDisposable m_Components = new CompositeDisposable();
    private EventSourceBootstrapper m_EventSourceBootstrapper;
    private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      ReferenceApplicationEventSource.Log.EnteringMethod(nameof(AppBootstrapper), nameof(AppDomainUnhandledException));
      Exception _ex = e.ExceptionObject as Exception;
      ReferenceApplicationEventSource.Log.LogException(_ex);
      HandleException(_ex);
    }
    private static void HandleException(Exception ex)
    {
      if (ex == null)
        return;
      MessageBox.Show(Properties.Resources.UnhandledException, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
      Environment.Exit(1);
    }
    #endregion

  }
}
