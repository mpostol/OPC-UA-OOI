
using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Windows;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.ReferenceApplication.Consumer;
using UAOOI.Networking.ReferenceApplication.MEF;
using UAOOI.Networking.ReferenceApplication.Producer;
using UAOOI.Networking.ReferenceApplication.Properties;

namespace UAOOI.Networking.ReferenceApplication
{
  internal class AppBootstrapper : MefBootstrapper, IDisposable
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
    protected override void OnInitialized()
    {
      base.OnInitialized();
      EventSourceBootstrapper _eventSourceBootstrapper = Container.GetExportedValue<EventSourceBootstrapper>();
      m_Components.Add(_eventSourceBootstrapper);
      _eventSourceBootstrapper.Run();
      Diagnostic.ReferenceApplicationEventSource.Log.StartingApplication();
      ConsumerDataManagementSetup m_ConsumerConfigurationFactory = Container.GetExportedValue<ConsumerDataManagementSetup>();
      m_Components.Add(m_ConsumerConfigurationFactory);
      m_ConsumerConfigurationFactory.Setup();
      OPCUAServerProducerSimulator m_OPCUAServerProducerSimulator = Container.GetExportedValue<OPCUAServerProducerSimulator>();
      m_OPCUAServerProducerSimulator.Setup();
      m_Components.Add(m_OPCUAServerProducerSimulator);
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
      m_Components.Dispose();
    }
    #endregion

    #region private 
    private CompositeDisposable m_Components = new CompositeDisposable();
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
    #endregion

  }
}
