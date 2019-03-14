//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.InformationModelFactory
{
  /// <summary>
  /// Interface IObjectInstanceFactory - encapsulates definition of the Object NodeClass.
  /// </summary>
  public interface IObjectInstanceFactory : IInstanceFactory
  {

    /// <summary>
    /// Sets a value indicating whether the node supports events.
    /// </summary>
    /// <value><c>null</c> if supports events contains no value, <c>true</c> if [supports events]; otherwise, <c>false</c>.</value>
    bool? SupportsEvents { set; }


  }
}
