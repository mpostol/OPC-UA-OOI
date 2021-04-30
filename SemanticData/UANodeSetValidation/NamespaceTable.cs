//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// The table of URI entities for the Address Space. The <see cref="Namespaces.OpcUa"/> namespace has index = 0.
  /// </summary>
  public class NamespaceTable : INamespaceTable
  {
    #region Constructors

    /// <summary>
    /// Creates the <see cref="NamespaceTable"/> instance containing <see cref="Namespaces.OpcUa"/> namespace.
    /// </summary>
    internal NamespaceTable()
    {
      Append(new Uri(Namespaces.OpcUa));
    }

    #endregion Constructors

    #region IAddressSpaceURIRecalculate

    /// <summary>
    /// Searches for an index that matches the <paramref name="URI" />, and returns the zero-based index of the first occurrence within the namespace table.
    /// </summary>
    /// <param name="URI">The URI to search for in the namespace table.</param>
    /// <returns>The zero-based index of the first occurrence of <paramref name="URI" /> that matches the conditions defined by <paramref name="URI" />, if found; otherwise, –1.</returns>
    ushort INamespaceTable.GetURIIndexOrAppend(Uri URI)
    {
      int _index = GetURIIndex(URI);
      if (_index == -1)
        _index = Append(URI);
      return (ushort)_index;
    }

    //TODO AddressSpacePrototyping - IMNamespace must be required in case of export #584
    void INamespaceTable.RegisterModel(IModelTableEntry model)
    {
      int index = GetURIIndex((model ?? throw new ArgumentNullException("", "Model table entry must not be null")).ModelUri);
      if (index >= 0)
        modelsList[index] = model;
      else
        modelsList.Add(model);
    }

    void INamespaceTable.RegisterDependency(IModelTableEntry model)
    {
      int index = GetURIIndex((model ?? throw new ArgumentNullException("", "Model table entry must not be null")).ModelUri);
      if (index == -1)
        modelsList.Add(model);
    }

    /// <summary>
    /// Gets the model table entry.
    /// </summary>
    /// <param name="nsi">The namespace index.</param>
    /// <returns>IModelTableEntry.</returns>
    /// <exception cref="ArgumentOutOfRangeException">namespace index - Namespace index has not been registered</exception>
    public IModelTableEntry GetModelTableEntry(ushort nsi)
    {
      if (nsi >= modelsList.Count)
        throw new ArgumentOutOfRangeException("namespace index", "Namespace index has not been registered");
      return modelsList[nsi];
    }

    /// <summary>
    /// Gets the index of the URI.
    /// </summary>
    /// <param name="URI">The URI.</param>
    /// <returns>System.Int32.</returns>
    public int GetURIIndex(Uri URI)
    {
      return modelsList.FindIndex(x => x.ModelUri == URI);
    }

    #endregion IAddressSpaceURIRecalculate

    #region Public Members

    internal IEnumerable<IModelTableEntry> Models => modelsList;
    //TODO AddressSpacePrototyping - IMNamespace must be required in case of export #584
    //internal Uri DefaultModelURI => modelsList[defaultModelIndex].ModelUri;
    //int INamespaceTable.DefaultModelIndex => defaultModelIndex;

    internal bool ValidateNamesapceTable(Action<Uri> add2UndefinedModelUriList)
    {
      if (modelsList.Count == 0)
        return false;
      bool returnValue = true;
      foreach (IModelTableEntry item in modelsList)
      {
        if (item is ModelTableEntryFixture)
        {
          add2UndefinedModelUriList(item.ModelUri);
          returnValue = false;
        }
      };
      return returnValue;
    }

    #endregion Public Members

    #region private

    private class ModelTableEntryFixture : IModelTableEntry
    {
      public ModelTableEntryFixture(Uri URI)
      {
        ModelUri = URI;
      }

      public byte AccessRestrictions { get; private set; } = 0xC;
      public Uri ModelUri { get; private set; }
      public DateTime? PublicationDate { get; private set; } = DateTime.UtcNow.Date;
      public IModelTableEntry[] RequiredModel { get; private set; }
      public IRolePermission[] RolePermissions { get; } = new XML.RolePermission[] { new XML.RolePermission() };
      public string Version { get; } = new Version().ToString();
    }

    private List<IModelTableEntry> modelsList = new List<IModelTableEntry>();

    private int Append(Uri URI)
    {
      int index = GetURIIndex(URI);
      if (index == -1)
      {
        modelsList.Add(new ModelTableEntryFixture(URI));
        index = modelsList.Count - 1;
      }
      return index;
    }

    #endregion private
  }
}