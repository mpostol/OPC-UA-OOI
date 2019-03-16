//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;
using TraceMessage = UAOOI.SemanticData.UANodeSetValidation.TraceMessage;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  internal abstract class NodeFactoryBase : NodesContainer, INodeFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">Encapsulates an action used to trace events and model errors.</param>
    public NodeFactoryBase(Action<TraceMessage> traceEvent) : base(traceEvent) { }

    #region INodeFactory
    /// <summary>
    /// It holds the value of the BrowseName attribute of modes in the Address Space.
    /// </summary>
    /// <value>The BrowseName of the node.</value>
    public string BrowseName
    {
      set;
      private get;
    }
    /// <summary>
    /// Add new reference to the references collection of the node. This collection represents all the references defined by the selected Information Model including
    /// references to the instance declarations nodes. The References list specifies references that must be created for the node during Address Space instantiation.
    /// The reference can be forward or inverse.
    /// </summary>
    /// <returns>IReferenceFactory.</returns>
    public IReferenceFactory NewReference()
    {
      ReferenceFactoryBase _ret = new ReferenceFactoryBase();
      m_References.Add(_ret);
      return _ret;
    }
    /// <summary>
    /// Sets the a symbolic name for the node that can be used as a class/field name by a design tools to enhance auto-generated code.
    /// It should only be specified if the BrowseName cannot be used for this purpose. This field is not used directly to instantiate
    /// Address Space and is intended for use by design tools. Only letters, digits or the underscore (‘_’) are permitted.
    /// This attribute is not exposed in the Address Space.
    /// </summary>
    /// <value>The symbolic name for the node.</value>
    public XmlQualifiedName SymbolicName
    {
      set;
      internal get;
    }
    /// <summary>
    /// Sets the write mask. The optional WriteMask attribute represents the WriteMask attribute of the Basic NodeClass, which exposes the possibilities of a client
    /// to write the attributes of the node. The WriteMask attribute does not take any user access rights into account, that is, although an attribute is writable
    /// this may be restricted to a certain user/user group.
    /// </summary>
    /// <value>The write access.</value>
    /// <remarks>Default Value "0"</remarks>
    public uint WriteAccess
    {
      set;
      private get;
    }
    /// <summary>
    /// Adds new value for the Description. The optional Description element shall explain the meaning of the node in a localized text using the same mechanisms
    /// for localization as described for the DisplayName.
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    public void AddDescription(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_Description, this.TraceEvent);
    }
    public void AddDisplayName(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_DisplayName, this.TraceEvent);
    }
    #endregion

    #region internal API
    /// <summary>
    /// Exports an instance of <see cref="NodeDesign"/>.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="createInstanceType">Type of the create instance.</param>
    /// <returns>NodeDesign.</returns>
    internal abstract NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType);
    #endregion

    #region private
    protected void UpdateNode(NodeDesign nodeDesign, List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      string _defaultDisplay = String.IsNullOrEmpty(BrowseName) ? SymbolicName.Name : BrowseName;
      nodeDesign.BrowseName = BrowseName == SymbolicName.Name ? null : BrowseName;
      List<NodeDesign> _Members = new List<NodeDesign>();
      path.Add(this.SymbolicName.Name);
      base.ExportNodes(_Members, path, createInstanceType);
      InstanceDesign[] _items = _Members.Cast<InstanceDesign>().ToArray<InstanceDesign>();
      nodeDesign.Children = _items == null || _items.Length == 0 ? null : new ListOfChildren() { Items = _items };
      nodeDesign.Description = this.m_Description;
      nodeDesign.DisplayName = this.m_DisplayName == null || this.m_DisplayName.Value == _defaultDisplay ? null : m_DisplayName;
      nodeDesign.IsDeclaration = false;
      nodeDesign.NumericId = 0;
      nodeDesign.NumericIdSpecified = false;
      nodeDesign.References = m_References.Count == 0 ? null : m_References.Select<ReferenceFactoryBase, Reference>(x => x.Export()).ToArray<Reference>();
      nodeDesign.StringId = null;
      nodeDesign.SymbolicId = null;
      nodeDesign.SymbolicName = this.SymbolicName;
      nodeDesign.WriteAccess = this.WriteAccess;
    }
    private LocalizedText m_Description = null;
    private LocalizedText m_DisplayName = null;
    private List<ReferenceFactoryBase> m_References = new List<ReferenceFactoryBase>();
    #endregion

  }

}
