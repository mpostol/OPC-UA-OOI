//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  /// <summary>
  /// Interface IUANodeSetModelHeader - represents a structure of the <see cref="UANodeSet"/> document header.
  /// </summary>
  internal interface IUANodeSetModelHeader
  {
    /// <summary>
    /// Gets the list of NamespaceUris used in the <see cref="UANodeSet"/>.
    /// </summary>
    /// <value>An array of <see cref="string"/> representing URI.</value>
    string[] NamespaceUris
    {
      get;
    }

    /// <summary>
    /// Gets the list of ServerUris used in the <see cref="UANodeSet"/>.
    /// </summary>
    /// <value>An array of <see cref="string"/> representing URI.</value>
    string[] ServerUris
    {
      get;
    }

    /// <summary>
    /// Gets the list of <see cref="ModelTableEntry"/> that are defined in the <see cref="UANodeSet"/>  along with any dependencies these models have.
    /// </summary>
    /// <value>An array of <see cref="ModelTableEntry"/> representing a model.</value>
    ModelTableEntry[] Models
    {
      get;
    }

    /// <summary>
    /// Gets the list of <see cref="NodeIdAlias"/>used in the <see cref="UANodeSet"/> .
    /// </summary>
    /// <value>An array of <see cref="NodeIdAlias"/> representing alias description.</value>
    NodeIdAlias[] Aliases
    {
      get;
    }

    /// <summary>
    /// Gets the array of <see cref="XmlElement"/> containing any vendor defined extensions to the <see cref="UANodeSet"/>.
    /// </summary>
    /// <value>An array of <see cref="XmlElement"/> representing extension.</value>
    XmlElement[] Extensions
    {
      get;
    }
  }
}