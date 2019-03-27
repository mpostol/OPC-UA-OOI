//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
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

    /// <summary>
    /// Gets the default model table entry.
    /// </summary>
    /// <param name="modelUri">The model URI.</param>
    /// <returns>UAOOI.SemanticData.UANodeSetValidation.Utilities.IModelTableEntry.</returns>
    public static IModelTableEntry GetDEfaultModelTableEntry(string modelUri)
    {
      if (string.IsNullOrEmpty(modelUri))
        throw new ArgumentNullException(nameof(modelUri), $"Model URI must be provided for the {nameof(IModelTableEntry)} instance");
      return new ModelTableEntry
      {
        AccessRestrictions = 0xC,
        ModelUri = modelUri,
        PublicationDate = DateTime.UtcNow.Date,
        RequiredModel = null,
        RolePermissions = new IRolePermission[] { new RolePermission() },
        Version = string.Empty
      };
    }
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
    /// <summary>
    /// Class RolePermission - default RolePermissions for all Nodes in the model.
    /// </summary>
    /// <remarks>
    /// This type is defined in Part 6 F.5 but the definition is not compliant with the UANodeSet schema. 
    /// This type is also defined in the Part 3 5.2.9 but the definition is not compliant.
    /// </remarks>
    private class RolePermission : IRolePermission
    {

      /// <summary>
      /// Gets or sets the permissions.
      /// </summary>
      /// <remarks>
      /// This is a subtype of the UInt32 DataType with the OptionSetValues Property defined. It is used to define the permissions of a Node. The <c>PermissionType</c> is formally defined in Part3 8.55 Table 38.
      /// </remarks>
      /// <value>The permissions.</value>
      public uint Permissions { get; set; } = 0xC;
      /// <summary>
      /// Gets or sets the value.
      /// </summary>
      /// <remarks>
      /// Not defined in the spec. 
      /// </remarks>
      /// <value>The value.</value>
      public string Value { get; set; } = string.Empty;

    }
    private class ModelTableEntry : IModelTableEntry
    {

      /// <summary>
      /// Initializes a new instance of the <see cref="ModelTableEntry"/> class.
      /// </summary>
      public ModelTableEntry()
      {
        this.AccessRestrictions = (byte)0;
      }
      /// <summary>
      /// Gets or sets the role permissions. The list of default RolePermissions for all Nodes in the model.
      /// </summary>
      /// <value>The role permissions.</value>
      public IRolePermission[] RolePermissions { get; set; }
      /// <summary>
      /// Gets or sets the required model. A list of dependencies for the model. If the model requires a minimum version the PublicationDate shall be specified. 
      /// Tools which attempt to resolve these dependencies may accept any PublicationDate after this date.
      /// </summary>
      /// <value>The required model.</value>
      public IModelTableEntry[] RequiredModel { get; set; }
      /// <summary>
      /// Gets or sets the model URI. The URI for the model. This URI should be one of the entries in the <see cref="NamespaceTable"/> table.
      /// </summary>
      /// <value>The model URI.</value>
      public string ModelUri { get; set; }
      /// <summary>
      /// Gets or sets the version. The version of the model defined in the UANodeSet. This is a human readable string and not intended for programmatic comparisons.
      /// </summary>
      /// <value>The version.</value>
      public string Version { get; set; }
      /// <summary>
      /// Gets or sets the publication date. When the model was published. This value is used for comparisons if the model is defined in multiple UANodeSet files.
      /// </summary>
      /// <value>The publication date.</value>
      public DateTime? PublicationDate { get; set; }
      /// <summary>
      /// Gets or sets the access restrictions. The default AccessRestrictions that apply to all Nodes in the model.
      /// </summary>
      /// <value>The access restrictions.</value>
      public byte AccessRestrictions { get; set; }

    }
  }
}
