
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

  /// <summary>
  /// Interface IEndPointConfiguration - Represents the current network attachment parameters.
  /// Depending on the role of the <see cref="IAssociation"/> it describes: remote or local end point.
  /// </summary>
  public interface IEndPointConfiguration
  {
  }

  internal interface IMessageWriter : IMessageHandler
  {

  }
  internal interface IMessageReader : IMessageHandler
  {

  }
  internal interface IMessageHandler
  {
    event EventHandler<MessageEventArg> messageHandlerStatusChanged;
    /// <summary>
    /// The property provides the current operational state of the <see cref="IMessageHandler"/> Object.
    /// </summary>
    /// <value>The handler state <see cref="HandlerState"/>.</value>
    HandlerState HandlerState { get; }
    /// <summary>
    /// This method is used to enable a configured <see cref="IMessageHandler"/> object. If a normal operation is possible, the state changes into <see cref="HandlerState.Operational"/> state. 
    /// In the case of an error situation, the state changes into <see cref="HandlerState.Error"/>. The operation is rejected if the current <paramref name="HandlerState"/>  is not <see cref="HandlerState.Disabled"/>.
    /// </summary>
    void Enable();
    /// <summary>
    /// This method is used to disable a PubSub Object.
    /// This method call shall be rejected if the current State is <see cref="HandlerState.Disabled"/> or <see cref="HandlerState.NoConfiguration"/>.
    /// </summary>
    void Disable();
  }

}
