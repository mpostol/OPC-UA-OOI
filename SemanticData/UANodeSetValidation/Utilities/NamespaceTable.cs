//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Threading;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation.Utilities
{

  /// <summary>
  /// The table of namespace uris for a server.
  /// </summary>
  public class NamespaceTable : StringTable
  {
    #region Constructors
    /// <summary>
    /// Creates the collection containing <see cref="Namespaces.OpcUa"/> namespace. 
    /// </summary>
    public NamespaceTable(Action<TraceMessage> traceEvent)
    {
      Append(Namespaces.OpcUa, traceEvent);
    }

    /// <summary>
    /// Creates an empty collection which is marked as shared.
    /// </summary>
    public NamespaceTable(bool shared, Action<TraceMessage> traceEvent)
    {
      Append(Namespaces.OpcUa, traceEvent);

#if DEBUG
      m_shared = shared;
#endif
    }

    ///// <summary>
    ///// Copies a list of strings.
    ///// </summary>
    //public NamespaceTable(IEnumerable<string> namespaceUris, Action<TraceMessage> traceEvent)
    //{
    //  Update(namespaceUris, traceEvent);
    //}
    #endregion

    //#region Public Members
    ///// <summary>
    ///// Updates the table of namespace uris.
    ///// </summary>
    //public new void Update(IEnumerable<string> namespaceUris, Action<TraceMessage> traceEvent)
    //{
    //  if (namespaceUris == null) throw new ArgumentNullException("namespaceUris");

    //  // check that first entry is the UA namespace.
    //  int ii = 0;

    //  foreach (string namespaceUri in namespaceUris)
    //  {
    //    if (ii == 0 && namespaceUri != Namespaces.OpcUa)
    //    {
    //      throw new ArgumentException("The first namespace in the table must be the OPC-UA namespace.");
    //    }

    //    ii++;

    //    if (ii == 2)
    //    {
    //      break;
    //    }
    //  }

    //  base.Update(namespaceUris, traceEvent);
    //}
    //#endregion
  }
}
