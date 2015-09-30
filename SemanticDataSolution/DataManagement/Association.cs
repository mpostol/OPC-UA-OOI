
using System;
using System.Collections;
using System.Collections.Generic;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class Association - provides basic implementation od the <see cref="IAssociation"/>
  /// </summary>
  public abstract class Association : IAssociation
  {

    #region constructor
    public Association(ISemanticData data, string aliasName)
    {
      if (data == null)
        throw new NullReferenceException("data");
      DataDescriptor = data;
      if (String.IsNullOrEmpty(aliasName))
        throw new NullReferenceException("aliasName");
      if (m_AssociationsDictionary.ContainsKey(aliasName))
        throw new ArgumentOutOfRangeException("aliasName", "aliasName must be unique");
      m_AssociationsDictionary.Add(aliasName, this);
      State = GetDefaultState();
    }
    #endregion

    #region IAssociation
    public event EventHandler<AssociationStateChangedEventArgs> StateChangedEventHandler;
    public ISemanticData DataDescriptor
    {
      get;
      private set;
    }
    public IAssociationState State
    {
      get;
      private set;
    }
    public IEndPointConfiguration Address
    {
      get;
      private set;
    }
    public ISemanticDataItemConfiguration DefaultConfiguration
    {
      get { throw new NotImplementedException(); }
    }
    public ISemanticDataItemConfiguration this[string SymbolicName]
    {
      get
      {
        return m_ConfigurationDictionary[SymbolicName];
      }
      set
      {
        m_ConfigurationDictionary[SymbolicName] = value;
      }
    }
    public int CompareTo(object obj)
    {
      return ((Association)obj).m_AliasName.CompareTo(m_AliasName);
    }
    #endregion

    #region private
    private string m_AliasName = string.Empty;
    private Dictionary<string, ISemanticDataItemConfiguration> m_ConfigurationDictionary = new Dictionary<string, ISemanticDataItemConfiguration>();
    private static Dictionary<string, Association> m_AssociationsDictionary = new Dictionary<string, Association>();
    protected abstract IAssociationState GetDefaultState();
    protected void RaiseStateChangedEventHandler (AssociationStateChangedEventArgs args)
    {
      EventHandler<AssociationStateChangedEventArgs> _locEven = StateChangedEventHandler;
      if (_locEven == null)
        return;
      _locEven(this, args);
    }
    #endregion

  }
}
