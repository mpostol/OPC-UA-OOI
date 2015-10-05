
using System;
using System.Collections.Generic;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Interface IAssociation - it is represents PubSubConnectionType and contains all information related to communication protocols.
  /// </summary>
  public interface IAssociation : IComparable
  {
    //ISemanticData DataDescriptor { get; }
    ///// <summary>
    ///// Gets the current operational state of the association <see cref="IAssociationState"/>
    ///// </summary>
    ///// <value>The state.</value>
    //IAssociationState State { get; }
    ///// <summary>
    ///// Occurs when state of this instance changed.
    ///// </summary>
    //event EventHandler<AssociationStateChangedEventArgs> StateChangedEventHandler;
    ///// <summary>
    ///// Gets the address as the end point description.
    ///// </summary>
    ///// <value>The network end point description.</value>
    //IEndPointConfiguration Address { get; set; }
    //ISemanticDataItemConfiguration DefaultConfiguration { get; }
    //ISemanticDataItemConfiguration this[string SymbolicName] { get; set; }
  }

  public interface IConfiguration { }
  public interface IConsumerConfiguration : IConfiguration
  {

  }



}
