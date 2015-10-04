
using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class Association - provides basic implementation od the <see cref="IAssociation"/>
  /// </summary>
  public abstract class Association : IAssociation
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="Association" /> class.
    /// </summary>
    /// <param name="data">The UA Semantic Data triple representation.</param>
    /// <param name="members">The members. </param>
    /// <param name="aliasName">A readable alias name for this instance to be used on User Interface.</param>
    /// <exception cref="System.NullReferenceException">data argument must not be null
    /// or
    /// aliasName argument must not be null</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">aliasName;aliasName must be unique</exception>
    public Association(ISemanticData data, DataSetConfiguration members, string aliasName, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      if (data == null)
        throw new NullReferenceException("data argument must not be null");
      DataDescriptor = data;
      if (String.IsNullOrEmpty(aliasName))
        throw new NullReferenceException("aliasName argument must not be null");
      if (m_ISemanticDataDictionary.ContainsKey(data))
        throw new ArgumentOutOfRangeException("data", "data must be unique");
      m_ISemanticDataDictionary.Add(data, this);
      if (members == null)
        throw new NullReferenceException("members argument must not be null");
      if (m_AliasDictionary.ContainsKey(aliasName))
        throw new ArgumentOutOfRangeException("aliasName", "aliasName must be unique");
      m_AliasName = aliasName;
      m_AliasDictionary.Add(m_AliasName, data);
      p_State = new AssociationStateNoConfiguration(this);
      DefaultConfiguration = GetDefaultConfiguration();
      Address = null;
      m_ProcessDataBindings = members.Members.Select<DataMemberConfiguration, IBinding>(x => x.GetBinding4DataMember(members, bindingFactory, encodingFactory)).ToArray<IBinding>();
    }
    #endregion

    #region IAssociation
    /// <summary>
    /// Occurs when state of this instance changed.
    /// </summary>
    public event EventHandler<AssociationStateChangedEventArgs> StateChangedEventHandler;
    /// <summary>
    /// Gets the data descriptor captured by an <see cref="ISemanticData"/> instance.
    /// </summary>
    /// <value>The <see cref="ISemanticData"/> instance representing UA Semantic Data triple https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticDataSolution/README.MD. </value>
    public ISemanticData DataDescriptor
    {
      get;
      private set;
    }
    /// <summary>
    /// Gets the current operational state of this instance
    /// </summary>
    /// <value>The state <see cref="IAssociationState"/> of this instance .</value>
    public IAssociationState State
    {
      get { return p_State; }
      private set
      {
        p_State = value;
        RaiseStateChangedEventHandler(new AssociationStateChangedEventArgs(value));
      }
    }
    public abstract IEndPointConfiguration Address { get; set; }
    public ISemanticDataItemConfiguration DefaultConfiguration
    {
      get;
      private set;
    }
    public ISemanticDataItemConfiguration this[string SymbolicName]
    {
      get
      {
        return m_ItemConfigurationDictionary[SymbolicName];
      }
      set
      {
        m_ItemConfigurationDictionary[SymbolicName] = value;
      }
    }
    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.</returns>
    public int CompareTo(object obj)
    {
      return m_AliasName.CompareTo(((Association)obj).m_AliasName);
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

    #region interna API
    public void Initialize()
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
    private Dictionary<string, ISemanticDataItemConfiguration> m_ItemConfigurationDictionary = new Dictionary<string, ISemanticDataItemConfiguration>();
    private Dictionary<string, ISemanticData> m_AliasDictionary = new Dictionary<string, ISemanticData>();
    private IBinding[] m_ProcessDataBindings = null;
    private IAssociationState p_State = null;
    private string m_AliasName = string.Empty;
    private static Dictionary<ISemanticData, Association> m_ISemanticDataDictionary = new Dictionary<ISemanticData, Association>();
    //methods
    protected void RaiseStateChangedEventHandler(AssociationStateChangedEventArgs args)
    {
      EventHandler<AssociationStateChangedEventArgs> _locEven = StateChangedEventHandler;
      if (_locEven == null)
        return;
      _locEven(this, args);
    }
    protected abstract ISemanticDataItemConfiguration GetDefaultConfiguration();
    protected abstract void InitializeCommunication();
    protected abstract void OnEnabling();
    protected abstract void OnDisabling();
    #endregion

  }

}
