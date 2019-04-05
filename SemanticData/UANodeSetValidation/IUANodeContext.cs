//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  internal interface IUANodeContext : IUANodeBase
  {

    /// <summary>
    /// Builds the symbolic identifier.
    /// </summary>
    /// <param name="path">The browse path.</param>
    void BuildSymbolicId(List<string> path);
    Dictionary<string, IUANodeContext> GetDerivedChildren();
    /// <summary>
    /// Gets or sets a value indicating whether the node is in recursion chain - selected for analysis second time.
    /// </summary>
    /// <value><c>true</c> if the node is in recursion chain; otherwise, <c>false</c>.</value>
    bool InRecursionChain { get; set; }
    bool IsPropertyVariableType { get; }
    QualifiedName m_BrowseName { get; set; }
    XmlQualifiedName ExportBrowseNameBaseType(Action<NodeId> traceEvent);
    /// <summary>
    /// Updates this instance in case the wrapped <see cref="UANode"/> is recognized in the model.
    /// </summary>
    /// <param name="node">The node <see cref="UANode"/> containing definition to be added to the model.</param>
    void Update(UANode node);

  }

}