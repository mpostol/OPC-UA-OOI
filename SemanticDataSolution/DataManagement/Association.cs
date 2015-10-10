
using System;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class Association - provides basic implementation od the <see cref="IAssociation"/>
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
    /// Depending on the implementation this name is used to filter packages against the destination.</param>
    /// <param name="id">The identifier of the DataSet selected to be processes by this instance.</param>
    /// <exception cref="System.NullReferenceException">
    /// data argument must not be null
    /// or
    /// aliasName argument must not be null
    /// </exception>
    /// <remarks>
    /// The DataSet has the following identifiers
    ///  - <see cref="Association.DataDescriptor"/>: not used by current implementation
    ///  - <see cref="Association.AssociationId"/>: <see cref="Guid"/> generated for the 
    ///  - <see cref="Association.DataDescriptor"/>: not used by current implementation
    /// </remarks>
    /// <exception cref="System.ArgumentOutOfRangeException">id; id must not be empty</exception>
    internal Association(ISemanticData data, string aliasName)
    {
      if (data == null)
        throw new NullReferenceException("data argument must not be null");
      DataDescriptor = data;
      if (String.IsNullOrEmpty(aliasName))
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
    /// <value>The <see cref="ISemanticData"/> instance representing UA Semantic Data triple https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticDataSolution/README.MD. </value>
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
      get { return p_State; }
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
      catch (Exception)
      {
        State = new AssociationStateError(this);
      }
    }
    /// <summary>
    /// Adds the message handler. It must initialize binding between the <see cref="IMessageHandler"/> and the local data resources.
    /// </summary>
    /// <param name="messageHandler">The message handler.</param>
    internal protected abstract void AddMessageHandler(IMessageHandler messageHandler);
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
    private abstract class AssociationStateBaseBase : IAssociationState
    {
      public AssociationStateBaseBase(Association host)
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
    private class AssociationStateDisabled : AssociationStateBaseBase
    {
      public AssociationStateDisabled(Association host)
        : base(host)
      { }
      public override HandlerState State { get { return HandlerState.Disabled; } }
      public override void Enable()
      {
        base.Enable();
      }
      public override void Disable()
      {
        throw new InvalidOperationException("Disable call is not allowed in the Disabled state");
      }
    }
    private class AssociationStateOperational : AssociationStateBaseBase
    {
      public AssociationStateOperational(Association host)
        : base(host)
      { }
      public override HandlerState State { get { return HandlerState.Operational; } }
      public override void Enable()
      {
        throw new InvalidOperationException("Enable call is not allowed in the Operational state.");
      }
      public override void Disable()
      {
        base.Disable();
      }
    }
    private class AssociationStateNoConfiguration : AssociationStateBaseBase
    {
      public AssociationStateNoConfiguration(Association host)
        : base(host)
      { }
      public override HandlerState State { get { return HandlerState.NoConfiguration; } }
      public override void Enable()
      {
        throw new InvalidOperationException("Enable call is not allowed in the NoConfiguration state.");
      }
      public override void Disable()
      {
        throw new InvalidOperationException("Disable call is not allowed in the NoConfiguration state.");
      }
    }
    private class AssociationStateError : AssociationStateBaseBase
    {
      public AssociationStateError(Association host)
        : base(host)
      { }
      public override HandlerState State { get { return HandlerState.Error; } }
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
    private string m_AliasName = string.Empty;
    #endregion

    #region protected
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
