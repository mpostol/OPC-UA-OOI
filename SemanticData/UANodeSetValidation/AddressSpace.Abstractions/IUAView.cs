//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  /// <summary>
  /// Interface IUAView - encapsulates definition of a View NodeClass
  /// </summary>
  public interface IUAView : IUANode
  {
    /// <summary>
    /// If set to “TRUE” this Attribute indicates that by following the References in the context of the View there are no loops, i.e. starting from a Node “A” contained in the View and following the forward References in the context of the View Node “A” will not be reached again. It does not specify that there is only one path starting from the View Node to reach a Node contained in the View.If set to FALSE this Attribute indicates that following References in the context of the View may lead to loops.
    /// Sets or gets a value indicating whether the part of the Address Space represented by View contains no loops.
    /// The mandatory ContainsNoLoops attribute is set to false if the server is not able to identify if the view contains loops or not.
    /// </summary>
    /// <value><c>true</c> if the part of the Address Space represented by View contains no loops; otherwise, <c>false</c>.</value>
    bool ContainsNoLoops { get; set; }

    /// <summary>
    /// Sets a value indicating whether the events are supported.
    /// </summary>
    /// <remarks>
    /// The EventNotifier is used to indicate if the node can be used to subscribe to events or the read/write historic events.
    /// Must return EventNotifierType defined in the P 3 8.59
    /// </remarks>
    //TODO UANodeSetValidation - Define independent Address Space API #672 - replace byte by the EventNotifierType according to the definition in the spec
    byte EventNotifier { get; set; }
  }
}