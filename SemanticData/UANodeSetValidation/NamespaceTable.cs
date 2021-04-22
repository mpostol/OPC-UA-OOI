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

    ushort INamespaceTable.GetURIIndexOrAppend(Uri URI)
    {
      int _index = GetURIIndex(URI);
      if (_index == -1)
        _index = Append(URI);
      return (ushort)_index;
    }

    //TODO Import all dependencies for the model #575
    void INamespaceTable.UpadateModelOrAppend(IModelTableEntry model, bool defaultModel)
    {
      int index = GetURIIndex(model.ModelUri);
      if (index >= 0)
        modelsList[index] = model;
      else
      {
        modelsList.Add(model);
        index = modelsList.Count - 1;
      }
      if (defaultModel)
        this.defaultModelIndex = index;
    }

    public IModelTableEntry GetModelTableEntry(ushort nsi)
    {
      if (nsi >= modelsList.Count)
        throw new ArgumentOutOfRangeException("namespace index", "Namespace index has not been registered");
      return modelsList[nsi];
    }

    public int GetURIIndex(Uri URI)
    {
      return modelsList.FindIndex(x => x.ModelUri == URI);
    }

    #endregion IAddressSpaceURIRecalculate

    #region Public Members

    internal IEnumerable<IModelTableEntry> Models => modelsList;
    internal Uri DefaultModelURI => modelsList[defaultModelIndex].ModelUri;

    int INamespaceTable.DefaultModelURI => defaultModelIndex;

    #endregion Public Members

    #region private

    //var

    private List<IModelTableEntry> modelsList = new List<IModelTableEntry>();
    private int defaultModelIndex;

    private int Append(Uri URI)
    {
      int index = GetURIIndex(URI);
      if (index == -1)
      {
        modelsList.Add(XML.ModelTableEntry.GetDefaultModelTableEntry(URI.ToString()));
        index = modelsList.Count - 1;
      }
      return index;
    }

    #endregion private
  }
}