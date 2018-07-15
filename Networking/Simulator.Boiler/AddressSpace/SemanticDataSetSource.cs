//___________________________________________________________________________________
//
//  Copyright (C) Year of Copyright, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  public class SemanticDataSetSource : ISemanticDataSetSource
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SemanticDataSetSource"/> class.
    /// </summary>
    /// <param name="parent">The parent collecting all variables to be captured by this instance.</param>
    /// <param name="name">The name of the semantic data set source.</param>
    public SemanticDataSetSource(BaseInstanceState parent, string name)
    {
      SemanticDataSetName = name;
      List<BaseInstanceState> _myComponents = new List<BaseInstanceState>();
      parent.GetChildren(_myComponents);
      for (int ii = 0; ii < _myComponents.Count; ii++)
      {
        List<BaseInstanceState> _hasComponentPath = new List<BaseInstanceState>();
        _myComponents[ii].RegisterVariable(_hasComponentPath, (x, y) => { if (x is IVariable) _variables.Add(String.Join(m_JoiningChar, y), (IVariable)x); });
      }
    }
    #endregion

    #region ISemanticDataSetSource
    public string SemanticDataSetName { get; private set; }
    public IEnumerable<string> Keys => _variables.Keys;
    public int Count => _variables.Count;
    public IVariable this[string[] key] => _variables[String.Join(m_JoiningChar, key)];
    /// <summary>
    /// Determines whether this set contains key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns><c>true</c> if this set contains key; otherwise, <c>false</c>.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool ContainsKey(string key)
    {
      return _variables.ContainsKey(key);
    }
    #endregion

    #region private
    private const string m_JoiningChar = "_";
    private Dictionary<string, IVariable> _variables = new Dictionary<string, IVariable>();
    #endregion

  }
}
