//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.ModelFactoryTestingFixture
{
  /// <summary>
  /// Class ObjectInstanceFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.InstanceFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IObjectInstanceFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory.InstanceFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IObjectInstanceFactory" />
  internal class ObjectInstanceFactoryBase : InstanceFactoryBase, IObjectInstanceFactory
  {
    /// <summary>
    /// Sets a value indicating whether the node supports events.
    /// </summary>
    /// <value><c>null</c> if supports events contains no value, <c>true</c> if [supports events]; otherwise, <c>false</c>.</value>
    public bool? SupportsEvents
    {
      set { }
    }
  }
}