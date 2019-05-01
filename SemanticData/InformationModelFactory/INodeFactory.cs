//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface INodeFactory -  a base type that defines a set of fields representing attributes and references of any node in the Address Space.
  /// </summary>
  public interface INodeFactory : INodeContainer
  {

    /// <summary>
    /// It holds the value of the BrowseName attribute of modes in the Address Space. The BrowseName is the name used in the information model. 
    /// The BrowseName is qualified by the namespace used for the SymbolicName
    /// </summary>
    /// <value>The BrowseName of the node.</value>
  string BrowseName { set; }
    /// <summary>
    /// Adds new value for the Description. The optional Description element shall explain the meaning of the node in a localized text using the same mechanisms 
    /// for localization as described for the DisplayName.
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    void AddDescription(string localeField, string valueField);
    /// <summary>
    /// Adds new value for the DisplayName. The DisplayName attribute contains the localized name of the node. 
    /// Clients should use this attribute if they want to display the name of the node to the user. They should not use 
    /// the BrowseName for this purpose. The server may maintain one or more localized representations for each DisplayName. 
    /// Clients negotiate the locale to be returned when they open a session with the server. The section DisplayName defines the structure of the DisplayName. 
    /// The string part of the DisplayName is restricted to 512 characters.
    /// </summary>
    /// <param name="localeField">The locale field.</param>
    /// <param name="valueField">The value field.</param>
    void AddDisplayName(string localeField, string valueField);
    /// <summary>
    /// Add new reference to the references collection of the node. This collection represents all the references defined by the selected Information Model including 
    /// references to the instance declarations nodes. The References list specifies references that must be created for the node during Address Space instantiation. 
    /// The reference can be forward or inverse.
    /// </summary>
    /// <returns>IReferenceFactory.</returns>
    IReferenceFactory NewReference();
    /// <summary>
    /// Sets the a symbolic name for the node that can be used as a class/field name by a design tools to enhance auto-generated code. 
    /// It should only be specified if the BrowseName cannot be used for this purpose. This field is not used directly to instantiate 
    /// Address Space and is intended for use by design tools. Only letters, digits or the underscore (‘_’) are permitted. 
    /// This attribute is not exposed in the Address Space.
    /// </summary>
    /// <value>The symbolic name for the node.</value>
    XmlQualifiedName SymbolicName { set; }
    /// <summary>
    /// Sets the write mask. The optional WriteMask attribute represents the WriteMask attribute of the Basic NodeClass, which exposes the possibilities of a client 
    /// to write the attributes of the node. The WriteMask attribute does not take any user access rights into account, that is, although an attribute is writable 
    /// this may be restricted to a certain user/user group.
    /// </summary>
    /// <remarks>Default Value "0"</remarks>
    /// <value>The write access.</value>
    uint WriteAccess { set; }
    /// <summary>
    /// Sets the access restrictions.
    /// </summary>
    /// <remarks>
    /// Part 6 Table F.1 – UANodeSet The default AccessRestrictions that apply to all Nodes in the model.
    /// </remarks>
    /// <value>The access restrictions.</value>
    AccessRestrictions AccessRestrictions { set; }
    /// <summary>
    /// Sets the release status of the node.
    /// </summary>
    /// <remarks>
    /// It is not exposed in the address space.
    /// Added in the Rel 1.04 to the specification.
    /// </remarks>
    /// <value>The release status.</value>
    ReleaseStatus ReleaseStatus { set; }
    //TODO Mantis - report error 
    /// <summary>
    /// Sets the data type purpose.
    /// </summary>
    /// <remarks>
    /// Not defined in the specification Part 2, 5, 6 and Errata Release 1.04.2 September 25, 2018
    /// This field is defined in the UADataType in the <c>UADataType</c> but in UA Model Design in the <c>NodeDesign</c>
    /// </remarks>
    /// <value>The data type purpose.</value>
    DataTypePurpose DataTypePurpose { set; }
    /// <summary>
    /// Sets the category. A list of identifiers used to group related UANodes together for use by tools that create/edit UANodeSet files.
    /// </summary>
    /// <value>The category.</value>
    string[] Category { set; }

  }
}
