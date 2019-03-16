//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  /// <summary>
  /// Class TypeFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UAModelDesignExport.NodeFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.ITypeFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UAModelDesignExport.NodeFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.ITypeFactory" />
  internal abstract class TypeFactoryBase : NodeFactoryBase, ITypeFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeFactoryBase" /> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public TypeFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    #region ITypeFactory
    /// <summary>
    /// Sets the base type of the node.
    /// </summary>
    /// <value>The base type represented by the <see cref="T:System.Xml.XmlQualifiedName" />.</value>
    public XmlQualifiedName BaseType
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets a value indicating whether this instance is abstract.
    /// </summary>
    /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
    /// <remarks>Default Value is false</remarks>
    public bool IsAbstract
    {
      set;
      private get;
    }
    #endregion

    //internal API
    internal void Update(TypeDesign nodeDesign, List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      nodeDesign.BaseType = this.BaseType;
      nodeDesign.ClassName = String.Empty;
      nodeDesign.IsAbstract = this.IsAbstract;
      nodeDesign.NoClassGeneration = false;
      base.UpdateNode(nodeDesign, path, createInstanceType);
    }

  }
}
