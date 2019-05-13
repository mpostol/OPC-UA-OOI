//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.InformationModelFactory
{

  /// <summary>
  /// Interface IViewInstanceFactory - encapsulates definition of a View NodeClass
  /// </summary>
  public interface IViewInstanceFactory : IInstanceFactory
  {

    /// <summary>
    /// Sets a value indicating whether the events are supported.
    /// </summary>
    /// <value><c>null</c> if it contains no value, <c>true</c> if events are supported; otherwise, <c>false</c>.</value>
    bool? SupportsEvents
    {
      set;
    }
    /// <summary>
    /// Sets a value indicating whether the part of the Address Space represented by View contains no loops. 
    /// The mandatory ContainsNoLoops attribute is set to false if the server is not able to identify if the view contains loops or not.
    /// </summary>
    /// <value><c>true</c> if the part of the Address Space represented by View contains no loops; otherwise, <c>false</c>.</value>
    bool ContainsNoLoops
    {
      set;
    }

  }
}
