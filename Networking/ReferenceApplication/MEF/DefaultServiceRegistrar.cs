
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using UAOOI.Networking.SemanticData;

namespace UAOOI.Networking.ReferenceApplication.MEF
{
  internal static class DefaultServiceRegistrar
  {

    /// <summary>
    /// Registers the required by the application types that are not already registered in the <see cref="AggregateCatalog"/>.
    /// </summary>
    ///<param name="aggregateCatalog">The <see cref="AggregateCatalog"/> to register the required types in, if they are not already registered.</param>
    public static AggregateCatalog RegisterRequiredServicesIfMissing(AggregateCatalog aggregateCatalog)
    {
      if (aggregateCatalog == null)
        throw new ArgumentNullException(nameof(aggregateCatalog));
      IEnumerable<ComposablePartDefinition> _partsToRegister = GetRequiredPartsToRegister(aggregateCatalog);
      DefaultsCatalog _defaults = new DefaultsCatalog(_partsToRegister);
      aggregateCatalog.Catalogs.Add(_defaults);
      return aggregateCatalog;
    }

    private static IEnumerable<ComposablePartDefinition> GetRequiredPartsToRegister(AggregateCatalog aggregateCatalog)
    {
      List<ComposablePartDefinition> _partsToRegister = new List<ComposablePartDefinition>();
      ComposablePartCatalog _catalogWithDefaults = GetDefaultComposablePartCatalog();
      foreach (ComposablePartDefinition _part in _catalogWithDefaults.Parts)
        foreach (ExportDefinition _export in _part.ExportDefinitions)
        {
          bool _exportAlreadyRegistered = false;
          foreach (ComposablePartDefinition registeredPart in aggregateCatalog.Parts)
            foreach (ExportDefinition registeredExport in registeredPart.ExportDefinitions)
              if (string.Compare(registeredExport.ContractName, _export.ContractName, StringComparison.Ordinal) == 0)
              {
                _exportAlreadyRegistered = true;
                break;
              }
          if (_exportAlreadyRegistered != false)
            continue;
          if (!_partsToRegister.Contains(_part))
            _partsToRegister.Add(_part);
        }
      return _partsToRegister;
    }
    /// <summary>
    /// Returns an <see cref="AssemblyCatalog" /> for the current assembly
    /// </summary>
    /// <remarks>
    /// To ensure that the calling assembly is this one, the call is in this
    /// private helper method.
    /// </remarks>
    /// <returns>
    /// Returns an <see cref="AssemblyCatalog" /> for the current assembly
    /// </returns>
    private static ComposablePartCatalog GetDefaultComposablePartCatalog()
    {
      return new AggregateCatalog(new AssemblyCatalog(Assembly.GetAssembly(typeof(AppBootstrapper))), 
                                  new AssemblyCatalog(Assembly.GetAssembly(typeof(DataManagementSetup))));
    }

  }
}
