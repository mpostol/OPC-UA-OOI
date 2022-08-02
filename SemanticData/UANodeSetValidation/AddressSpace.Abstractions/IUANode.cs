//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  /// <summary>
  /// Interface INodeFactory -  a base type that defines a set of fields representing attributes and references of any node in the Address Space.
  /// </summary>
  public partial interface IUANode
  {
    /// <summary>
    /// It holds the value of the BrowseName attribute of modes in the Address Space. The BrowseName is the name used in the information model.
    /// The BrowseName is qualified by the namespace used for the SymbolicName
    /// </summary>
    /// <value>The BrowseName of the node.</value>
    string BrowseName { set; get; }

    /// <summary>
    /// Sets the a symbolic name for the node that can be used as a class/field name by a design tools to enhance auto-generated code.
    /// It should only be specified if the BrowseName cannot be used for this purpose. This field is not used directly to instantiate
    /// Address Space and is intended for use by design tools. Only letters, digits or the underscore (‘_’) are permitted.
    /// This attribute is not exposed in the Address Space.
    /// </summary>
    /// <value>The symbolic name for the node.</value>
    string SymbolicName { set; get; }

    ///// <summary>
    ///// Sets the write mask. The optional WriteMask attribute represents the WriteMask attribute of the Basic NodeClass, which exposes the possibilities of a client
    ///// to write the attributes of the node. The WriteMask attribute does not take any user access rights into account, that is, although an attribute is writable
    ///// this may be restricted to a certain user/user group.
    ///// </summary>
    ///// <remarks>Default Value "0"</remarks>
    ///// <value>The write access.</value>
    //uint WriteAccess { set; get; }

    /// <summary>
    /// Sets the access restrictions.
    /// </summary>
    /// <remarks>
    /// Part 6 Table F.1 – UANodeSet The default AccessRestrictions that apply to all Nodes in the model.
    /// </remarks>
    /// <value>The access restrictions.</value>
    byte AccessRestrictions { set; get; }

    /// <summary>
    /// Sets the release status of the node.
    /// </summary>
    /// <remarks>
    /// It is not exposed in the address space.
    /// Added in the Rel 1.04 to the specification.
    /// </remarks>
    /// <value>The release status.</value>
    ReleaseStatus ReleaseStatus { set; get; }

    //TODO Mantis - report error
    /// <summary>
    /// Sets the data type purpose.
    /// </summary>
    /// <remarks>
    /// Not defined in the specification Part 2, 5, 6 and Errata Release 1.04.2 September 25, 2018
    /// This field is defined in the UADataType in the <c>UADataType</c> but in UA Model Design in the <c>NodeDesign</c>
    /// </remarks>
    /// <value>The data type purpose.</value>
    //DataTypePurpose DataTypePurpose { set; }
    /// <summary>
    /// Sets the category. A list of identifiers used to group related UANodes together for use by tools that create/edit UANodeSet files.
    /// </summary>
    /// <value>The category.</value>
    string[] Category { set; get; }
  }

  public partial interface IUANode
  {
    string NodeId { get; set; }
    IReference[] References { get; }
    LocalizedText[] DisplayName { get; set; }
    LocalizedText[] Description { get; set; }
    IRolePermission[] RolePermissions { get; set; }
    string Documentation { get; set; }
    uint WriteMask { set; get; }
    uint UserWriteMask { set; get; }
  }

  public partial interface IUANode
  {
    NodeId NodeIdNodeId { get; }
    QualifiedName BrowseNameQualifiedName { get; }
    NodeClassEnum NodeClassEnum { get; }

    void RemoveInheritedValues(IUANode baseNode);
  }
}