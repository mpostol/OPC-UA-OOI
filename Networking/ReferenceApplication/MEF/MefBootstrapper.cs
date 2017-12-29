
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using CommonServiceLocator;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.ReferenceApplication.Diagnostic;

namespace UAOOI.Networking.ReferenceApplication.MEF
{
  internal abstract class MefBootstrapper : BootstrapperBase, IDisposable
  {

    #region BootstrapperBase
    /// <summary>
    /// Run the bootstrapper process.
    /// </summary>
    public override void Run(bool runWithDefaultConfiguration)
    {
      this.Logger = this.CreateLogger();
      if (this.Logger == null)
        throw new InvalidOperationException("Null Logger Exception");
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Logger Was Created Successfully");
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Creating Catalog For MEF");
      this.AggregateCatalog = this.CreateAggregateCatalog();
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Configuring Catalog For MEF");
      this.ConfigureAggregateCatalog();
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Register Default Types If Missing");
      this.RegisterDefaultTypesIfMissing();
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Creating Mef Container");
      this.Container = this.CreateContainer();
      if (this.Container == null)
        throw new InvalidOperationException("Null Composition Container Exception");
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Configuring Mef Container");
      this.ConfigureContainer();
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Configuring Service Locator Singleton");
      this.ConfigureServiceLocator();
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Registering Framework Exception Types");
      this.RegisterFrameworkExceptionTypes();
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Creating Shell");
      this.Shell = this.CreateShell();
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Initializing Shell");
      this.InitializeShell();
      this.Logger.TraceData(TraceEventType.Verbose, 33, "Bootstrapper Sequence Completed");
    }
    /// <summary>
    /// Configures the the <see cref="CommonServiceLocator.ServiceLocator" /> .
    /// </summary>
    /// <remarks>
    /// The base implementation also sets the ServiceLocator provider singleton.
    /// </remarks>
    protected void ConfigureServiceLocator()
    {
      IServiceLocator _serviceLocator = this.Container.GetExportedValue<IServiceLocator>();
      ServiceLocator.SetLocatorProvider(() => _serviceLocator);
    }
    #endregion

    #region private
    /// <summary>
    /// Gets or sets the default <see cref="AggregateCatalog"/> for the application.
    /// </summary>
    /// <value>The default <see cref="AggregateCatalog"/> instance.</value>
    protected AggregateCatalog AggregateCatalog { get; private set; }
    /// <summary>
    /// Gets the default <see cref="CompositionContainer"/> for the application.
    /// </summary>
    /// <value>The default <see cref="CompositionContainer"/> instance.</value>
    protected CompositionContainer Container { get; private set; }
    /// <summary>
    /// Configures the <see cref="AggregateCatalog"/> used by MEF.
    /// </summary>
    /// <remarks>
    /// The base implementation returns a new AggregateCatalog.
    /// </remarks>
    /// <returns>An <see cref="AggregateCatalog"/> to be used by the bootstrapper.</returns>
    protected virtual AggregateCatalog CreateAggregateCatalog()
    {
      return new AggregateCatalog();
    }
    /// <summary>
    /// Configures the <see cref="AggregateCatalog"/> used by MEF.
    /// </summary>
    /// <remarks>
    /// The base implementation does nothing.
    /// </remarks>
    protected virtual void ConfigureAggregateCatalog() { }
    /// <summary>
    /// Creates the <see cref="CompositionContainer"/> that will be used as the default container.
    /// </summary>
    /// <returns>A new instance of <see cref="CompositionContainer"/>.</returns>
    /// <remarks>
    /// The base implementation registers a default MEF catalog of exports of key types.
    /// Exporting your own types will replace these defaults.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The default export provider is in the container and disposed by MEF.")]
    protected virtual CompositionContainer CreateContainer()
    {
      CompositionContainer _container = new CompositionContainer(this.AggregateCatalog);
      return _container;
    }
    /// <summary>
    /// Configures the <see cref="CompositionContainer"/>. 
    /// May be overwritten in a derived class to add specific type mappings required by the application.
    /// </summary>
    /// <remarks>
    /// The base implementation registers all the types direct instantiated by the bootstrapper with the container.
    /// If the method is overwritten, the new implementation should call the base class version.
    /// </remarks>
    protected virtual void ConfigureContainer()
    {
      this.RegisterBootstrapperProvidedTypes();
    }
    /// <summary>
    /// Helper method for configuring the <see cref="CompositionContainer"/>. 
    /// Registers defaults for all the types necessary for the infrastructure to work, if they are not already registered.
    /// </summary>
    public virtual void RegisterDefaultTypesIfMissing()
    {
      this.AggregateCatalog = DefaultServiceRegistrar.RegisterServices(this.AggregateCatalog);
    }
    /// <summary>
    /// Helper method for configuring the <see cref="CompositionContainer"/>. 
    /// Registers all the types direct instantiated by the bootstrapper with the container.
    /// </summary>
    protected virtual void RegisterBootstrapperProvidedTypes()
    {
      this.Container.ComposeExportedValue<ITraceSource>(this.Logger);
      this.Container.ComposeExportedValue<IServiceLocator>(new ServiceLocatorAdapter(this.Container));
      this.Container.ComposeExportedValue<AggregateCatalog>(this.AggregateCatalog);
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposedValue)
        return;
      ReferenceApplicationEventSource.Log.EnteringDispose(nameof(MefBootstrapper), disposing);
      if (disposing)
      {
        this.AggregateCatalog.Dispose();
        this.Container.Dispose();
      }
      disposedValue = true;
    }
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }
    #endregion
    #endregion

  }

}
