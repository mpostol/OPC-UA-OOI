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
  public interface IUAView : IUAInstance
  {
    /// <summary>
    /// Sets or gets a value indicating whether the part of the Address Space represented by View contains no loops.
    /// The mandatory ContainsNoLoops attribute is set to false if the server is not able to identify if the view contains loops or not.
    /// </summary>
    /// <value><c>true</c> if the part of the Address Space represented by View contains no loops; otherwise, <c>false</c>.</value>
    bool ContainsNoLoops { get; set; }

    //TODO Description
    /// <summary>
    /// Sets a value indicating whether the events are supported.
    /// </summary>
    byte EventNotifier { get; set; }
  }
}