//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation.Utilities
{

  /// <summary>
  /// The table of namespace uris for a server. The <see cref="Namespaces.OpcUa"/> namespace has index = 0.
  /// </summary>
  public class NamespaceTable
  {

    #region Constructors
    /// <summary>
    /// Creates the <see cref="NamespaceTable"/> instance containing <see cref="Namespaces.OpcUa"/> namespace.
    /// </summary>
    internal NamespaceTable()
    {
      Append(Namespaces.OpcUa);
    }
    #endregion

    #region Public Members
    /// <summary>
    /// Adds model Uri to the table.
    /// </summary>
    internal int Append(string modelUri)
    {
      if (string.IsNullOrEmpty(modelUri))
        throw new ArgumentNullException("value", "URI must not be null or empty");
      ModelTableEntry _newModel = ModelTableEntry.GetDefaultModelTableEntry(modelUri, ref m_Index);
      m_URIDictionary.Add(modelUri, _newModel);
      m_IndexDictionary = m_URIDictionary.Values.ToDictionary(y => y.Index, x => x.ModelUri);
      return m_URIDictionary.Count - 1;
    }
    internal string GetString(ushort nsi)
    {
      if (m_IndexDictionary.ContainsKey(nsi))
        return m_IndexDictionary[nsi];
      throw new ArgumentOutOfRangeException("namespace index", "Namespace index has not been registered");
    }
    internal int GetIndex(string uri)
    {
      if (string.IsNullOrEmpty(uri))
        throw URINullException();
      return m_URIDictionary.ContainsKey(uri) ? m_URIDictionary[uri].Index : -1;
    }
    internal ushort LastNamespaceIndex => (ushort)(m_Index - 1);
    /// <summary>
    /// Returns the index of the specified namespace uri, adds it if it does not exist.
    /// </summary>
    internal ushort GetIndexOrAppend(string uri)
    {
      int _index = GetIndex(uri);
      if (_index == -1)
        _index = Append(uri);
      return (ushort)_index;
    }
    internal IEnumerable<IModelTableEntry> ExportNamespaceTable => m_URIDictionary.Values;
    #endregion

    #region private
    //var
    private readonly Dictionary<string, ModelTableEntry> m_URIDictionary = new Dictionary<string, ModelTableEntry>();
    private Dictionary<ushort, string> m_IndexDictionary = new Dictionary<ushort, string>();
    private ushort m_Index = 0;
    //classes
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
      /// Gets or sets the index of the model.
      /// </summary>
      /// <value>The index.</value>
      internal ushort Index { get; set; } = 0;
      /// <summary>
      /// Gets the default model table entry.
      /// </summary>
      /// <param name="modelUri">The model URI.</param>
      /// <param name="index">Index of the model.</param>
      /// <returns>UAOOI.SemanticData.UANodeSetValidation.Utilities.IModelTableEntry.</returns>
      /// <remarks>This type is defined in Part 6 F.5 but the definition is not compliant with the UANodeSet schema.
      /// This type is also defined in the Part 3 5.2.9 but the definition is not compliant.</remarks>
      internal static ModelTableEntry GetDefaultModelTableEntry(string modelUri, ref ushort index)
      {
        if (string.IsNullOrEmpty(modelUri))
          throw URINullException();
        return new ModelTableEntry
        {
          AccessRestrictions = 0xC,
          Index = index++,
          ModelUri = modelUri,
          PublicationDate = DateTime.UtcNow.Date,
          RequiredModel = null,
          RolePermissions = new IRolePermission[] { new RolePermission() },
          Version = new Version(1, 0).ToString()
        };
      }
      #region IModelTableEntry
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
      public byte AccessRestrictions { get; set; } = 0xC;
      #endregion
    }
    //methods
    private static ArgumentNullException URINullException()
    {
      return new ArgumentNullException("modelUri", $"Model URI must be provided for the {nameof(IModelTableEntry)} instance");
    }
    #endregion
  }
}

