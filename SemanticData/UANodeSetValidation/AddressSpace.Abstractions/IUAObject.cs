//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  /// <summary>
  /// Objects are used to represent systems, system components, real-world objects and software objects. Objects are defined using the Object NodeClass.
  /// </summary>
  public interface IUAObject : IUANode
  {
    /// <summary>
    /// The EventNotifier is used to indicate if the node can be used to subscribe to events or the read/write historic Events.
    /// The EventNotifierType is defined in P 3 - 8.59.
    /// </summary>
    // TODO UANodeSetValidation - Define independent Address Space API replace byte by the EventNotifierType according to the definition in the spec.
    byte EventNotifier { get; set; }
  }
}