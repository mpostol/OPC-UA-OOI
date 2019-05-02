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
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using TraceMessage = UAOOI.SemanticData.BuildingErrorsHandling.TraceMessage;

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
    public uint WriteAccess { set; private get; }
    /// <summary>
    /// Sets the access restrictions.
    /// </summary>
    /// <value>The access restrictions.</value>
    /// <remarks>The AccessRestrictions that apply to the Node.</remarks>
    public AccessRestrictions AccessRestrictions { set; private get; }
    /// <summary>
    /// Sets the release status of the node.
    /// </summary>
    /// <value>The release status.</value>
    /// <remarks>It is not exposed in the address space.
    /// Added in the Rel 1.04 to the specification.</remarks>
    public ReleaseStatus ReleaseStatus { set; private get; }
    /// <summary>
    /// Sets the data type purpose.
    /// </summary>
    /// <value>The data type purpose.</value>
    /// <exception cref="NotImplementedException"></exception>
    /// <remarks>Not defined in the specification Part 2, 5, 6 and Errata Release 1.04.2 September 25, 2018</remarks>
    public DataTypePurpose DataTypePurpose { set; private get; } = DataTypePurpose.Normal;
    /// <summary>
    /// Sets the category. A list of identifiers used to group related UANodes together for use by tools that create/edit UANodeSet files.
    /// </summary>
    /// <remarks>
    /// In the UA Model Design it is a comment separated list of categories assigned to the node (e.g. Part4/Services or Part5/StateMachines).
    /// </remarks>
    /// <value>The category.</value>
    public string[] Category { set; private get; } = null;
    /// <summary>
    /// Adds new value for the Description. The optional Description element shall explain the meaning of the node in a localized text using the same mechanisms
    /// for localization as described for the DisplayName.
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    public void AddDescription(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_Description, TraceEvent);
    }
    /// <summary>
    /// Adds new value for the DisplayName. The DisplayName attribute contains the localized name of the node.
    /// Clients should use this attribute if they want to display the name of the node to the user. They should not use
    /// the BrowseName for this purpose. The server may maintain one or more localized representations for each DisplayName.
    /// Clients negotiate the locale to be returned when they open a session with the server. The section DisplayName defines the structure of the DisplayName.
    /// The string part of the DisplayName is restricted to 512 characters.
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    public void AddDisplayName(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_DisplayName, TraceEvent);
    }
    #endregion

    #region internal API
    /// <summary>
    /// Exports an instance of <see cref="NodeDesign"/>.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="createInstanceType">Type of the create instance.</param>
    /// <returns>NodeDesign.</returns>
    internal abstract XML.NodeDesign Export(List<string> path, Action<XML.InstanceDesign, List<string>> createInstanceType);
    #endregion

    #region private
    protected void UpdateNode(XML.NodeDesign nodeDesign, List<string> path, Action<XML.InstanceDesign, List<string>> createInstanceType)
    {
      string _defaultDisplay = string.IsNullOrEmpty(BrowseName) ? SymbolicName.Name : BrowseName;
      nodeDesign.BrowseName = BrowseName == SymbolicName.Name ? null : BrowseName;
      List<XML.NodeDesign> _Members = new List<XML.NodeDesign>();
      path.Add(SymbolicName.Name);
      base.ExportNodes(_Members, path, createInstanceType);
      XML.InstanceDesign[] _items = _Members.Cast<XML.InstanceDesign>().ToArray<XML.InstanceDesign>();
      nodeDesign.Category = Category == null ? null : string.Join(", ", Category);
      nodeDesign.Children = _items == null || _items.Length == 0 ? null : new XML.ListOfChildren() { Items = _items };
      nodeDesign.Description = m_Description;
      nodeDesign.DisplayName = m_DisplayName == null || m_DisplayName.Value == _defaultDisplay ? null : m_DisplayName;
      nodeDesign.IsDeclaration = false;
      nodeDesign.NotInAddressSpace = false;
      nodeDesign.NumericId = 0;
      nodeDesign.NumericIdSpecified = false;
      nodeDesign.PartNo = 0;
      nodeDesign.Purpose = DataTypePurpose.ConvertToDataTypePurpose();
      nodeDesign.References = m_References.Count == 0 ? null : m_References.Select<ReferenceFactoryBase, XML.Reference>(x => x.Export()).ToArray<XML.Reference>();
      nodeDesign.ReleaseStatus = ReleaseStatus.ConvertToReleaseStatus();
      nodeDesign.StringId = null;
      nodeDesign.SymbolicId = null;
      nodeDesign.SymbolicName = SymbolicName;
      nodeDesign.WriteAccess = WriteAccess;
      // AccessRestrictions _access = AccessRestrictions; model design doesn't support AccessRestrictions
    }
    private XML.LocalizedText m_Description = null;
    private XML.LocalizedText m_DisplayName = null;
    private List<ReferenceFactoryBase> m_References = new List<ReferenceFactoryBase>();
    #endregion

  }

}
