//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;
using TraceMessage = UAOOI.SemanticData.BuildingErrorsHandling.TraceMessage;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  /// <summary>
  /// Class ModelFactory.
  /// Implements the <see cref="UAOOI.SemanticData.UAModelDesignExport.NodesContainer" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IModelFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UAModelDesignExport.NodesContainer" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IModelFactory" />
  internal class ModelFactory : NodesContainer, IModelFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelFactory"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public ModelFactory(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    //IModelFactory
    /// <summary>
    /// Creates the namespace description for the provided uri.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <remarks>The set of objects that the OPC Unified Architecture server makes available to clients is referred to as its Address Space. The namespace is provided to make the BrowseName unique in the Address Space.</remarks>
    public void CreateNamespace(string uri)
    {
      Namespace _new = new Namespace()
      {
        FilePath = string.Empty,
        InternalPrefix = uri,
        Name = string.Format("Name{0}", m_Count),
        Prefix = string.Format("Prefix{0}", m_Count++),
        Value = uri,
        XmlNamespace = uri,
        XmlPrefix = string.Format("Prefix{0}", m_Count++)
      };
      m_Namespaces.Add(_new);
    }

    //internal  API
    /// <summary>
    /// Exports this instance.
    /// </summary>
    /// <returns>ModelDesign.</returns>
    internal ModelDesign Export()
    {
      List<NodeDesign> _mdNodes = new List<NodeDesign>();
      List<string> path = new List<string>();
      base.ExportNodes(_mdNodes, path, (x, y) => CreateInstanceType(x, y, _mdNodes));
      //TODO add warnings to the model as the Property Nodes.
      //ModelDesign.NodeDesign _newNode = CreateNodeDesign((x, y) => { return CreateInstanceType(x, y, _mdNodes, _item.UAModelContext, traceEvent); }, null, _item, y =>
      //    {
      //      if (y.TraceLevel != TraceEventType.Verbose)
      //        _errors.Add(y.BuildError);
      //      traceEvent(y);
      //    });
      return new ModelDesign()
      {
        Items = _mdNodes.ToArray<NodeDesign>(),
        Namespaces = m_Namespaces.ToArray<Namespace>(),
        TargetNamespace = m_Namespaces[1].Value,
        AnyAttr = null,
        DefaultLocale = null,
        TargetPublicationDate = DateTime.Today,
        TargetPublicationDateSpecified = false,
        TargetVersion = string.Empty,
        TargetXmlNamespace = null
      };
    }

    //private 
    private List<Namespace> m_Namespaces = new List<Namespace>();
    private static int m_Count = 0;
    private void CreateInstanceType(InstanceDesign instance, List<string> browsePath, List<NodeDesign> mdNodes)
    {
      Debug.Assert(instance != null, "CreateInstanceType.instance cannot be null");
      InstanceDesign _ret = null;
      if (instance is MethodDesign)
      {
        MethodDesign _src = (MethodDesign)instance;
        XmlQualifiedName _newSymbolicName = new XmlQualifiedName(browsePath.SymbolicName(), instance.SymbolicName.Namespace);
        MethodDesign _method = new MethodDesign
        {
          BrowseName = _src.BrowseName,
          Children = null,
          Declaration = _src.Declaration,
          Description = _src.Description,
          DisplayName = _src.DisplayName,
          InputArguments = _src.InputArguments,
          IsDeclaration = _src.IsDeclaration,
          MaxCardinality = _src.MaxCardinality,
          MinCardinality = _src.MinCardinality,
          ModellingRule = ModellingRule.None,
          ModellingRuleSpecified = false,
          NonExecutable = _src.NonExecutable,
          NonExecutableSpecified = _src.NonExecutableSpecified,
          NumericId = _src.NumericId,
          NumericIdSpecified = _src.NumericIdSpecified,
          OutputArguments = _src.OutputArguments,
          PartNo = _src.PartNo,
          PreserveDefaultAttributes = _src.PreserveDefaultAttributes,
          References = _src.References,
          ReferenceType = _src.ReferenceType,
          StringId = _src.StringId,
          SymbolicId = _src.SymbolicId,
          SymbolicName = _newSymbolicName,
          TypeDefinition = null,
          WriteAccess = _src.WriteAccess,
        };
        _src.InputArguments = null;
        _src.OutputArguments = null;
        if (instance.Children == null || instance.Children.Items == null || instance.Children.Items.Length == 0)
          instance.Children = null;
        _src.TypeDefinition = _newSymbolicName;
        _ret = _method;
      }
      else
        Debug.Fail("In this release expected Method");
      if (_ret != null)
        mdNodes.Add(_ret);
    }

  }
}
