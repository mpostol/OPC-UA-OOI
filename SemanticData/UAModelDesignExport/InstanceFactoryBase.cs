//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  /// <summary>
  /// Class InstanceFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UAModelDesignExport.NodeFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IInstanceFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UAModelDesignExport.NodeFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IInstanceFactory" />
  internal abstract class InstanceFactoryBase : NodeFactoryBase, IInstanceFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="InstanceFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public InstanceFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    {}
    /// <summary>
    /// Sets the modeling rule, which defines whether the component of a complex type are instantiated.
    /// This value is defined by processing the object pointed by the HasModelingRule reference.
    /// </summary>
    /// <value>The modeling rule.</value>
    public ModelingRules? ModelingRule
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the type definition.
    /// </summary>
    /// <value>The type definition.</value>
    public XmlQualifiedName TypeDefinition
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the type of the reference if it is component of a complex definition.
    /// </summary>
    /// <value>The type of the reference used for parent child relationship.</value>
    public XmlQualifiedName ReferenceType
    {
      set;
      private get;
    }
    /// <summary>
    /// Updates the instance.
    /// </summary>
    /// <param name="nodeDesign">The node design.</param>
    /// <param name="path">The path.</param>
    /// <param name="traceEvent">The trace event.</param>
    /// <param name="createInstanceType">Type of the create instance.</param>
    protected void UpdateInstance(InstanceDesign nodeDesign, List<string> path, System.Action<TraceMessage> traceEvent, Action<InstanceDesign, List<string>> createInstanceType)
    {
      nodeDesign.Declaration = null;
      nodeDesign.MaxCardinality = 0;
      nodeDesign.MinCardinality = 0;
      nodeDesign.ModellingRule = this.ModelingRule.ConvertModellingRule(x => nodeDesign.ModellingRuleSpecified = x);
      nodeDesign.PreserveDefaultAttributes = false;
      nodeDesign.ReferenceType = this.ReferenceType;
      nodeDesign.TypeDefinition = this.TypeDefinition;
      base.UpdateNode(nodeDesign, path, createInstanceType);
    }

  }
}
