//___________________________________________________________________________________
//
//  Copyright (C) Year of Copyright, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections;
using System.Collections.Generic;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  /// <summary>
  /// Class SemanticDataSetSource - captures the enumerator of a set of variables representing the semantic data source expressed as the root object of the <see cref="BaseInstanceState"/> type.
  /// Each data entity has to have the parent relationship to the root <see cref="BaseVariableState"></see> instance.
  /// </summary>
  /// <seealso cref="ISemanticDataSetSource" />
  public class SemanticDataSetSource : ISemanticDataSetSource
  {
    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="SemanticDataSetSource"/> class.
    /// </summary>
    /// <param name="parent">The parent collecting all variables to be captured by this instance.</param>
    /// <param name="rootBrowseName">The name of the semantic data set source.</param>
    public SemanticDataSetSource(BaseInstanceState parent)
    {
      SemanticDataSetRootBrowseName = parent.BrowseName.ToString();
      List<BaseInstanceState> _myComponents = new List<BaseInstanceState>();
      parent.GetChildren(_myComponents);
      for (int ii = 0; ii < _myComponents.Count; ii++)
      {
        List<BaseInstanceState> _hasComponentPath = new List<BaseInstanceState>();
        _myComponents[ii].RegisterVariable(_hasComponentPath, (x, y) => { if (x is IVariable) m_Variables.Add(string.Join(m_JoiningChar, y), (IVariable)x); });
      }
    }

    #endregion constructor

    #region ISemanticDataSetSource

    public string SemanticDataSetRootBrowseName { get; private set; }
    public IEnumerable<string> Keys => m_Variables.Keys;
    public int Count => m_Variables.Count;
    public IVariable this[string[] key] => m_Variables[string.Join(m_JoiningChar, key)];

    /// <summary>
    /// Determines whether this set contains key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns><c>true</c> if this set contains key; otherwise, <c>false</c>.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool ContainsKey(string key)
    {
      return m_Variables.ContainsKey(key);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<KeyValuePair<string, IVariable>> GetEnumerator()
    {
      return m_Variables.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return m_Variables.GetEnumerator();
    }

    #endregion ISemanticDataSetSource

    //internal void CreateConfiguration
    //  (XmlQualifiedName instanceType, string _associationName, XmlQualifiedName instanceSymbolicName, string fileName, Tuple<string, ushort, Guid> writerNameDataSetWriterIdPublisherId, ITraceSource _traceSource)
    //{
    //  List<FieldMetaData> _lf = new List<FieldMetaData>();
    //  foreach (KeyValuePair<string, IVariable> _item in this)
    //  {
    //    if (_item.Value.ValueType.BuiltInType == BuiltInType.Null)
    //      continue;
    //    FieldMetaData _field = new FieldMetaData()
    //    {
    //      ProcessValueName = _item.Key,
    //      SymbolicName = _item.Key,
    //      TypeInformation = _item.Value.ValueType
    //    };
    //    _lf.Add(_field);
    //  }
    //  DataSetConfiguration _newDataSetConfiguration = new DataSetConfiguration()
    //  {
    //    AssociationName = _associationName,
    //    AssociationRole = AssociationRole.Producer,
    //    ConfigurationGuid = System.Guid.NewGuid(),
    //    ConfigurationVersion = new ConfigurationVersionDataType() { MajorVersion = 1, MinorVersion = 0 },
    //    Id = System.Guid.NewGuid(),
    //    InformationModelURI = instanceSymbolicName.Namespace,
    //    DataSet = _lf.ToArray(),
    //    DataSymbolicName = _associationName,
    //    MaxBufferTime = 1000,
    //    PublishingInterval = 100,
    //    RepositoryGroup = _associationName,
    //    Root = new NodeDescriptor()
    //    {
    //      BindingDescription = "Binding Description",
    //      DataType = instanceType,
    //      InstanceDeclaration = false,
    //      NodeClass = InstanceNodeClassesEnum.Object,
    //      NodeIdentifier = instanceSymbolicName
    //    }
    //  };
    //  ConfigurationManagement.AddDataSetConfiguration(_newDataSetConfiguration, writerNameDataSetWriterIdPublisherId, fileName, fileName, _traceSource);
    //}

    #region private

    private const string m_JoiningChar = "_";
    private Dictionary<string, IVariable> m_Variables = new Dictionary<string, IVariable>();

    #endregion private
  }
}