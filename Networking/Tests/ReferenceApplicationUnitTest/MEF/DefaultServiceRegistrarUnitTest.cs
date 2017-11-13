
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.ReferenceApplication.MEF;

namespace UAOOI.Networking.ReferenceApplication.UnitTest.MEF
{
  [TestClass]
  public class DefaultServiceRegistrarUnitTest
  {

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestMethod1()
    {
      UAOOI.Networking.ReferenceApplication.MEF.DefaultServiceRegistrar.RegisterRequiredServicesIfMissing(null);
    }
    [TestMethod]
    public void RegisterRequiredServicesIfMissing()
    {
      AggregateCatalog _emptyCatalog = new AggregateCatalog();
      AggregateCatalog newCatalog = DefaultServiceRegistrar.RegisterRequiredServicesIfMissing(_emptyCatalog);
      using (CompositionContainer container = new CompositionContainer(newCatalog))
      {
        foreach (ComposablePartDefinition _part in container.Catalog.Parts)
          foreach (var export in _part.ExportDefinitions)
            Debug.WriteLine(string.Format("Part contract name => '{0}'", export.ContractName));
        Assert.AreEqual<int>(4, container.Catalog.Parts.Count());
        MainWindow _MainWindowExportedValue = container.GetExportedValue<MainWindow>();
        Assert.IsNotNull(_MainWindowExportedValue);
        Assert.IsNotNull(_MainWindowExportedValue.MainWindowViewModel);
      }
    }
    [TestMethod]
    public void RegisterRequiredServicesIfMissingITraceSourceIsRegisteredWithContainer()
    {
      AggregateCatalog _catalog = new AggregateCatalog(new AssemblyCatalog(this.GetType().Assembly));
      AggregateCatalog _newCatalog = DefaultServiceRegistrar.RegisterRequiredServicesIfMissing(_catalog);
      using (CompositionContainer _container = new CompositionContainer(_newCatalog))
      {
        Assert.AreEqual<int>(5, _container.Catalog.Parts.Count<ComposablePartDefinition>());
        foreach (ComposablePartDefinition _part in _container.Catalog.Parts)
          foreach (ExportDefinition export in _part.ExportDefinitions)
            Debug.WriteLine(string.Format("Part contract name => '{0}'", export.ContractName));
        IInterface exportedValue = _container.GetExportedValue<IInterface>();
        Assert.IsNotNull(exportedValue);
      }
    }
    [TestMethod]
    public void RegisterRequiredServicesIfMissingApplicationCatalogIsRegisteredWithContainer()
    {
      AggregateCatalog _catalog = new AggregateCatalog(new ApplicationCatalog());
      AggregateCatalog _newCatalog = DefaultServiceRegistrar.RegisterRequiredServicesIfMissing(_catalog);
      using (CompositionContainer _container = new CompositionContainer(_newCatalog))
      {
        //Assert.AreEqual<int>(3, _container.Catalog.Parts.Count<ComposablePartDefinition>());
        foreach (ComposablePartDefinition _part in _container.Catalog.Parts)
        {
          foreach (ImportDefinition _import in _part.ImportDefinitions)
            Debug.WriteLine(string.Format("Imported contracts name => '{0}'", _import.ContractName));
          foreach (ExportDefinition _export in _part.ExportDefinitions)
            Debug.WriteLine(string.Format("Exported contracts name => '{0}'", _export.ContractName));
        }
        ITraceSource _exportedValue = _container.GetExportedValue<ITraceSource>();
        Assert.IsNotNull(_exportedValue);
        IInterface _interface = _container.GetExportedValue<IInterface>();
      }
    }
    [Export(typeof(IInterface))]
    private class LoggerFacade : IInterface { }

  }
  public interface IInterface
  {

  }
}
