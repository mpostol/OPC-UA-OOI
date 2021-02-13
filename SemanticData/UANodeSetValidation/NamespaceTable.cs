//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// The table of URI entities for the Address Space. The <see cref="Namespaces.OpcUa"/> namespace has index = 0.
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

    #endregion Constructors

    #region Public Members

    internal IModelTableEntry GetURIatIndex(ushort nsi)
    {
      if (nsi >= modelsList.Count)
        throw new ArgumentOutOfRangeException("namespace index", "Namespace index has not been registered");
      return modelsList[nsi];
    }

    /// <summary>
    /// Searches for an index that matches the <paramref name="URI"/>, and returns the zero-based index of the first occurrence within the <see cref="NamespaceTable"/>.
    /// </summary>
    /// <param name="URI">The URI .</param>
    /// <returns>
    /// The zero-based index of the first occurrence of <paramref name="URI"/>
    /// </returns>
    /// <exception cref="System.ArgumentNullException">URI is null.</exception>
    internal int GetURIIndex(string URI)
    {
      return modelsList.FindIndex(x => x.ModelUri == URI);
    }

    /// <summary>
    /// Returns the index of the specified namespace uri, adds it if it does not exist.
    /// </summary>
    internal ushort GetURIIndexOrAppend(string URI)
    {
      int _index = GetURIIndex(URI);
      if (_index == -1)
        _index = Append(URI);
      return (ushort)_index;
    }

    internal IEnumerable<IModelTableEntry> Models => modelsList;

    internal void UpadateModelOrAppend(IModelTableEntry model)
    {
      int index = GetURIIndex(model.ModelUri);
      if (index >= 0)
        modelsList[index] = model;
      else
        modelsList.Add(model);
    }

    #endregion Public Members

    #region private

    //var

    //private readonly Dictionary<string, ModelTableEntry> m_URIDictionary = new Dictionary<string, ModelTableEntry>();
    //private Dictionary<ushort, string> m_IndexDictionary = new Dictionary<ushort, string>();
    //private ushort m_Index = 0;
    private List<IModelTableEntry> modelsList = new List<IModelTableEntry>();

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
      /// Gets the default model table entry.
      /// </summary>
      /// <param name="modelUri">The model URI.</param>
      /// <param name="index">Index of the model.</param>
      /// <returns>UAOOI.SemanticData.UANodeSetValidation.Utilities.IModelTableEntry.</returns>
      /// <remarks>This type is defined in Part 6 F.5 but the definition is not compliant with the UANodeSet schema.
      /// This type is also defined in the Part 3 5.2.9 but the definition is not compliant.</remarks>
      internal static ModelTableEntry GetDefaultModelTableEntry(string modelUri)
      {
        if (string.IsNullOrEmpty(modelUri))
          throw URINullException();
        return new ModelTableEntry
        {
          AccessRestrictions = 0xC,
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

      #endregion IModelTableEntry
    }

    private int Append(string URI)
    {
      int index = GetURIIndex(URI);
      if (index == -1)
      {
        ModelTableEntry _newModel = ModelTableEntry.GetDefaultModelTableEntry(URI);
        modelsList.Add(_newModel);
        index = modelsList.Count - 1;
      }
      return index;
    }

    private static ArgumentNullException URINullException()
    {
      return new ArgumentNullException("modelUri", $"Model URI must be provided for the {nameof(IModelTableEntry)} instance");
    }

    #endregion private
  }
}