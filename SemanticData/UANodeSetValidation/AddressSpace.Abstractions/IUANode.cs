//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  /// <summary>
  /// Interface IUANode -  a base type that defines a set of fields representing attributes and references of any node in the Address Space.
  /// </summary>
  public partial interface IUANode: IEquatable<IUANode>
  {
    /// <summary>
    /// Sets or gets a symbolic name for the node that can be used as a class/field name by a design tools to enhance auto-generated code.
    /// It should only be specified if the BrowseName cannot be used for this purpose. This field is not used directly to instantiate
    /// Address Space and is intended for use by design tools. Only letters, digits or the underscore (‘_’) are permitted.
    /// </summary>
    /// <remarks>
    /// This attribute is not exposed in the Address Space.
    /// </remarks>
    /// <value>The symbolic name for the node.</value>
    string SymbolicName { set; get; }

    /// <summary>
    /// Sets the release status of the node.
    /// </summary>
    /// <remarks>
    /// It is not exposed in the address space.
    /// Added in the Rel 1.04 to the specification.
    /// </remarks>
    /// <value>The release status.</value>
    ReleaseStatus ReleaseStatus { set; get; }

    /// <summary>
    /// Sets the category. A list of identifiers used to group related UANodes together for use by tools that create/edit UANodeSet files.
    /// </summary>
    /// <value>The category.</value>
    string[] Category { set; get; }

    string Documentation { get; set; }

    void RemoveInheritedValues(IUANode baseNode);
  }

  /// <summary>
  /// This part is defined according to Part3 5.2 Base NodeClass
  /// </summary>
  public partial interface IUANode
  {
    /// <summary>
    /// Nodes are unambiguously identified using a constructed identifier called the <see cref="NodeId"/> . Some implementations may accept alternative NodeIds in addition to the canonical
    /// NodeId represented in this Attribute. An application shall persist the identifierType and identifier NodeId elements of a Node as well as the Namespace Uri which the
    /// namespaceIndex NodeId element references. An application may change the namespaceIndex NodeId element of a Node with future address space instantiation and therefore a user shall
    /// not assume the namespaceIndex will not change.
    /// </summary>
    NodeId NodeId { get; }

    /// <summary>
    /// The NodeClass identifies the NodeClass of a Node.
    /// </summary>
    /// <value>Returns NodeClassEnum</value>
    NodeClassEnum NodeClass { get; }

    /// <summary>
    /// It holds the value of the BrowseName attribute of modes in the Address Space. The BrowseName is the name used in the information model.
    /// The BrowseName is qualified by the namespace used for the SymbolicName
    ///
    /// Nodes have a BrowseName Attribute that is used as a non-localized human-readable name when browsing the AddressSpace to create paths out of BrowseNames.
    /// The TranslateBrowsePathsToNodeIds Service defined in OPC 10000-4 can be used to follow a path constructed of BrowseNames.
    ///
    /// A BrowseName should never be used to display the name of a Node.The DisplayName should be used instead for this purpose.
    ///
    /// Unlike NodeIds, the BrowseName cannot be used to unambiguously identify a Node. Different Nodes may have the same BrowseName.
    /// Section 8.3 defines the structure of the BrowseName.It contains a namespace and a string. The namespace is provided to make the BrowseName unique in some cases in the context
    /// of a Node (e.g.Properties of a Node) although not unique in the context of the Server.If different organizations define BrowseNames for Properties, the namespace of the BrowseName
    /// provided by the organization makes the BrowseName unique, although different organizations may use the same string having a slightly different meaning.
    ///
    /// Applications may often choose to use the same namespace for the NodeId and the BrowseName.However, if they want to provide a standard Property, its BrowseName shall have the namespace
    /// of the standards body although the namespace of the NodeId reflects something else, for example the local Server.
    ///
    /// Standards bodies defining standard type definitions shall use their namespace(s) for the NodeId of the TypeDefinitionNode as well as for the BrowseName of the TypeDefinitionNode.
    /// BrowseNames of TypeDefinitionNodes, ReferenceTypes, and DataTypes shall be unique. Any well-known instances used as entry points shall also be unique. For example, the Root Node defined in
    /// OPC 10000-5.
    /// The string-part of the BrowseName is case sensitive. That is, users shall consider them case sensitive.Servers are allowed to handle BrowseNames passed in Service requests as case
    /// insensitive. Examples are the TranslateBrowsePathsToNodeIds Service or Event filter. If a Server accepts a case insensitive BrowseName it needs to ensure that the uniqueness of the BrowseName
    /// does not depend on case.
    /// </summary>
    QualifiedName BrowseName { get; }

    /// <summary>
    /// The DisplayName Attribute contains the localized name of the Node. Users should use this property if they want to display the name of the Node. They should not use the BrowseName for this purpose.
    /// The application may maintain one or more localized representations for each DisplayName. The API user selects the locale to be returned when they open a session with the Server.
    /// Refer to OPC 10000-4 for a description of session establishment and locales. Section 8.5 defines the structure of the DisplayName.
    /// The string part of the DisplayName is restricted to 512 characters.
    /// </summary>
    LocalizedText[] DisplayName { get; }

    /// <summary>
    /// The optional Description Attribute shall explain the meaning of the Node in a localised text using the same mechanisms for localization as described for the DisplayName
    /// </summary>
    LocalizedText[] Description { get; }

    /// <summary>
    /// The optional WriteMask Attribute exposes the possibilities of a client to write the Attributes of the Node. The WriteMask Attribute does not take any user access rights into account,
    /// that is, although an Attribute is writable this may be restricted to a certain user/user group.
    ///
    /// If the OPC UA Server does not have the ability to get the WriteMask information for a specific Attribute from the underlying system, it should state that it is writable.If a write
    /// operation is called on the Attribute, the Server should transfer this request and return the corresponding StatusCode if such a request is rejected.StatusCodes are defined in OPC 10000-4.
    ///
    /// The AttributeWriteMask DataType is defined in 8.60.
    /// </summary>
    AttributeWriteMask WriteMask { set; get; }

    /// <summary>
    /// The optional UserWriteMask Attribute exposes the possibilities of a client to write the Attributes of the Node taking user access rights into account. It uses the AttributeWriteMask
    /// DataType which is defined in 8.60.
    ///
    /// The UserWriteMask Attribute can only further restrict the WriteMask Attribute, when it is set to not writable in the general case that applies for every user.
    /// Clients cannot assume an Attribute can be written based on the UserWriteMask Attribute.It is possible that the Server may return an access denied error due to some server
    /// specific change which was not reflected in the state of this Attribute at the time the Client accessed it.
    /// </summary>
    AttributeWriteMask UserWriteMask { set; get; }

    /// <summary>
    /// The optional RolePermissions Attribute specifies the Permissions that apply to a Node for all Roles which have access to the Node. The value of the Attribute is an array of
    /// RolePermissionType Structures
    /// </summary>
    IRolePermission[] RolePermissions { get; set; }

    /// <summary>
    /// The optional UserRolePermissions attribute specifies the permissions that apply to a node for all roles granted to current Session. The value of the Attribute is an array of
    /// RolePermissionType Structures (see Table 8).
    ///
    /// Clients may determine their effective permissions by performing a logical OR of permissions for each role in the array.
    ///
    /// The value of this Attribute is derived from the rules used by the hosting application to map sessions to roles. This mapping may be vendor specific or it may use the standard
    /// role model defined in Part 3 Section 4.8.
    ///
    /// This Attribute shall not be writable.
    ///
    /// If not specified, the value of DefaultUserRolePermissions property from the Namespace Metadata Object associated with the node is used instead. If the NamespaceMetadata Object does not
    /// define the Property or does not exist, then the hosting application does not publish any information about roles mapped to the current Session.
    /// </summary>
    IRolePermission[] UserRolePermissions { get; set; }

    /// <summary>
    /// Sets or gets the access restrictions. See also Part 3 section 5.2.11
    /// </summary>
    /// <remarks>
    /// The optional AccessRestrictions attribute specifies the AccessRestrictions that apply to a node. If a hosting application supports AccessRestrictions
    /// for a particular namespace it adds the DefaultAccessRestrictions Property to the NamespaceMetadata Object for that Namespace. If a particular node in the
    /// Namespace needs to override the default value the hosting application adds the AccessRestrictions attribute to the node.
    ///
    /// If a Server implements a vendor specific access restriction model for a Namespace, it does not add the DefaultAccessRestrictions Property to the NamespaceMetadata Object.
    /// </remarks>
    /// <value>The access restrictions.</value>
    AccessRestrictions AccessRestrictions { set; get; }

    /// <summary>
    /// Reference of the node.
    /// </summary>
    IReference[] References { get; }
  }
}