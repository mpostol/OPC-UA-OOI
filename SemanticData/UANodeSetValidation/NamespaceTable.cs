//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.AddressSpace.Abstractions;
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

    #region INamespaceTable

    /// <summary>
    /// Searches for an index that matches the <paramref name="URI" />, and returns the zero-based index of the first occurrence within the namespace table.
    /// </summary>
    /// <param name="URI">The URI to search for in the namespace table.</param>
    /// <returns>The zero-based index of the first occurrence of <paramref name="URI" />, if found; otherwise, it is appended.</returns>
    ushort INamespaceTable.GetURIIndexOrAppend(Uri URI)
    {
      int _index = GetURIIndex(URI);
      if (_index == -1)
        _index = Append(URI);
      return (ushort)_index;
    }

    /// <summary>
    /// Updates the model or append it to the existing collection
    /// </summary>
    /// <param name="model">The model in concern.</param>
    /// <exception cref="ArgumentNullException">model - Model table entry must not be null</exception>
    public void RegisterModel(IModelTableEntry model)
    {
      int index = GetURIIndex((model ?? throw new ArgumentNullException("model", "Model table entry must not be null")).ModelUri);
      if (index >= 0)
        modelsList[index] = model;
      else
        modelsList.Add(model);
    }

    /// <summary>
    /// Registers the dependency.
    /// </summary>
    /// <param name="model">The model that is required.</param>
    /// <exception cref="ArgumentNullException">Model table entry must not be null</exception>
    public void RegisterDependency(IModelTableEntry model)
    {
      int index = GetURIIndex((model ?? throw new ArgumentNullException("model", "Model table entry must not be null")).ModelUri);
      if (index == -1)
        modelsList.Add(new ModelTableEntryFixture(model));
    }

    /// <summary>
    /// Gets the model <see cref="Uri" />.
    /// </summary>
    /// <param name="namespaceIndex">Index of the namespace.</param>
    /// <returns>An instance that captures <see cref="Uri" /> of the requested model if already registered, otherwise, null.</returns>
    public Uri GetModelTableEntry(ushort namespaceIndex)
    {
      if (namespaceIndex >= modelsList.Count)
        return null;
      return modelsList[namespaceIndex].ModelUri;
    }

    /// <summary>
    /// Searches for an <paramref name="URI" />, and returns the zero-based index of the first occurrence within the <see cref="INamespaceTable" />.
    /// </summary>
    /// <param name="URI">The URI.</param>
    /// <returns>The zero-based index of the first occurrence of an <paramref name="URI" />, if found; otherwise, –1.</returns>
    public int GetURIIndex(Uri URI)
    {
      return modelsList.FindIndex(x => x.ModelUri == URI);
    }

    #endregion INamespaceTable

    #region Public Members

    internal IEnumerable<IModelTableEntry> Models => modelsList;

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

    private class RolePermission : IRolePermission
    {
      public uint Permissions { get; set; }
      public string Value { get; set; }
    }

    private class ModelTableEntryFixture : IModelTableEntry
    {
      public ModelTableEntryFixture(Uri URI)
      {
        ModelUri = URI;
      }

      public ModelTableEntryFixture(IModelTableEntry modelTableEntry)
      {
        AccessRestrictions = modelTableEntry.AccessRestrictions;
        ModelUri = modelTableEntry.ModelUri;
        PublicationDate = modelTableEntry.PublicationDate;
        RequiredModel = modelTableEntry.RequiredModel;
        RolePermissions = modelTableEntry.RolePermissions;
        Version = modelTableEntry.Version;
      }

      #region IModelTableEntry

      public byte AccessRestrictions { get; private set; } = 0xC;
      public Uri ModelUri { get; private set; }
      public DateTime? PublicationDate { get; private set; } = DateTime.UtcNow.Date;
      public IModelTableEntry[] RequiredModel { get; private set; }
      public IRolePermission[] RolePermissions { get; } = new RolePermission[] { new RolePermission() };
      public Version Version { get; } = new Version();

      #endregion IModelTableEntry
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