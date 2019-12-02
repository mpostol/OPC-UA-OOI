//____________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//____________________________________________________________________________

using System;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.Common;
using UAOOI.Networking.SemanticData.Diagnostics;
using UAOOI.Networking.SemanticData.MessageHandling;

namespace UAOOI.Networking.SemanticData
{

  /// <summary>
  /// Class Association - provides basic implementation of the association between the data set and message centric communication infrastructure.
  /// It represents configuration and bindings to the external resources.
  /// </summary>
  internal abstract class Association : IComparable
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="Association" /> class.
    /// The class captures all bindings between the message content and local resources.
    /// </summary>
    /// <param name="data">The UA Semantic Data triple representation. It id not used by current implementation.</param>
    /// <param name="aliasName">A readable alias name for this instance to be used on User Interface.
    /// Depending on the implementation this name is used to filter packets against the destination.</param>
    /// <exception cref="NullReferenceException">data argument must not be null
    /// or
    /// aliasName argument must not be null</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">data argument must not be null
    /// or
    /// aliasName argument must not be null</exception>
    /// <remarks>The DataSet has the following identifiers <see cref="Association.DataDescriptor"/></remarks>
    internal Association(ISemanticData data, string aliasName)
    {
      if (data == null)
        throw new NullReferenceException("data argument must not be null");
      DataDescriptor = data;
      if (string.IsNullOrEmpty(aliasName))
        throw new NullReferenceException("aliasName argument must not be null");
      m_AliasName = aliasName;
      p_State = new AssociationStateNoConfiguration(this);
    }
    #endregion

    #region API
    /// <summary>
    /// Occurs when state of this instance changed.
    /// </summary>
    internal event EventHandler<AssociationStateChangedEventArgs> StateChangedEventHandler;
    /// <summary>
    /// Gets the data descriptor captured by an <see cref="ISemanticData"/> instance.
    /// </summary>
    /// <value>The <see cref="ISemanticData"/> instance representing UA Semantic Data triple https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticData/README.MD. </value>
    internal ISemanticData DataDescriptor
    {
      get;
      private set;
    }
    /// <summary>
    /// Gets the current operational state of this instance
    /// </summary>
    /// <value>The state <see cref="IAssociationState"/> of this instance .</value>
    internal IAssociationState State
    {
      get => p_State;
      private set
      {
        p_State = value;
        RaiseStateChangedEventHandler(new AssociationStateChangedEventArgs(value.State));
      }
    }
    /// <summary>
    /// Initializes this instance.
    /// </summary>
    internal void Initialize()
    {
      try
      {
        InitializeCommunication();
        State = new AssociationStateDisabled(this);
      }
      catch (Exception _ex)
      {
        Diagnostics.ReactiveNetworkingEventSource.Log.LogException(nameof(Association), nameof(Initialize), _ex);
        State = new AssociationStateError(this);
      }
    }
    /// <summary>
    /// Adds the message handler. It must initialize binding between the <see cref="IMessageHandler" /> and the local data resources.
    /// </summary>
    /// <remarks>
    /// The Subscriber may have configured filters (like a PublisherId, DataSetWriterId or a DataSetClassId) so that it can drop all messages that do not match the filter
    /// </remarks>
    /// <param name="messageHandler">The message handler.</param>
    /// <param name="configuration">The configuration.</param>
    protected internal virtual void AddMessageHandler(IMessageHandler messageHandler, AssociationConfiguration configuration)
    {
      //TODO How to configure ProducerId #148
      DataSetId = new DataSelector() { DataSetWriterId = configuration.DataSetWriterId, PublisherId = configuration.PublisherId };
    }
    #endregion

    #region IComparable
    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.</returns>
    public int CompareTo(object obj)
    {
      return DataDescriptor.Guid.CompareTo(((Association)obj).DataDescriptor.Guid);
    }
    #endregion

    #region override Object
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this object alias name.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance alias name.</returns>
    public override string ToString()
    {
      return m_AliasName;
    }
    #endregion

    #region private
    //class
    private abstract class AssociationStateBase : IAssociationState
    {
      public AssociationStateBase(Association host)
      {
        m_Host = host;
      }
      public abstract HandlerState State { get; }
      public virtual void Enable()
      {
        m_Host.OnEnabling();
        m_Host.State = new AssociationStateOperational(m_Host);
      }
      public virtual void Disable()
      {
        m_Host.OnDisabling();
        m_Host.State = new AssociationStateDisabled(m_Host);
      }
      protected Association m_Host { get; private set; }
    }
    private class AssociationStateDisabled : AssociationStateBase
    {
      public AssociationStateDisabled(Association host)
        : base(host)
      { }
      public override HandlerState State => HandlerState.Disabled;
      public override void Enable()
      {
        base.Enable();
      }
      public override void Disable()
      {
        throw new InvalidOperationException("Disable call is not allowed in the Disabled state");
      }
    }
    private class AssociationStateOperational : AssociationStateBase
    {
      public AssociationStateOperational(Association host)
        : base(host)
      { }
      public override HandlerState State => HandlerState.Operational;
      public override void Enable()
      {
        throw new InvalidOperationException("Enable call is not allowed in the Operational state.");
      }
      public override void Disable()
      {
        base.Disable();
      }
    }
    private class AssociationStateNoConfiguration : AssociationStateBase
    {
      public AssociationStateNoConfiguration(Association host)
        : base(host)
      { }
      public override HandlerState State => HandlerState.NoConfiguration;
      public override void Enable()
      {
        throw new InvalidOperationException("Enable call is not allowed in the NoConfiguration state.");
      }
      public override void Disable()
      {
        throw new InvalidOperationException("Disable call is not allowed in the NoConfiguration state.");
      }
    }
    private class AssociationStateError : AssociationStateBase
    {
      public AssociationStateError(Association host)
        : base(host)
      { }
      public override HandlerState State => HandlerState.Error;
      public override void Enable()
      {
        throw new InvalidOperationException("Enable call is not allowed in the Error state.");
      }
      public override void Disable()
      {
        throw new InvalidOperationException("Disable call is not allowed in the Error state.");
      }
    }
    //var
    private IAssociationState p_State = null;
    private readonly string m_AliasName = string.Empty;
    #endregion

    #region protected
    /// <summary>
    /// Gets the data set identifier.
    /// </summary>
    /// <value>The data set identifier.</value>
    protected DataSelector DataSetId { get; private set; }
    /// <summary>
    /// Raises the state changed event handler.
    /// </summary>
    /// <param name="args">The <see cref="AssociationStateChangedEventArgs"/> instance containing the event data.</param>
    protected void RaiseStateChangedEventHandler(AssociationStateChangedEventArgs args)
    {
      EventHandler<AssociationStateChangedEventArgs> _locEven = StateChangedEventHandler;
      if (_locEven == null)
        return;
      _locEven(this, args);
    }
    /// <summary>
    /// Initializes the communication.
    /// </summary>
    protected abstract void InitializeCommunication();
    /// <summary>
    /// Called when the association is enabling.
    /// </summary>
    protected abstract void OnEnabling();
    /// <summary>
    /// Called when the association is disabling.
    /// </summary>
    protected abstract void OnDisabling();
    #endregion

  }

}
