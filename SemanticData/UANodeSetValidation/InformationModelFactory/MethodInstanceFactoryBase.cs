//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  /// <summary>
  /// Class MethodInstanceFactoryBase - basic implementation of the <see cref="IMethodInstanceFactory"/>.
  /// </summary>
  internal class MethodInstanceFactoryBase : InstanceFactoryBase, IMethodInstanceFactory
  {

    #region IMethodInstanceFactory
    /// <summary>
    /// Sets a value indicating whether the Method node is executable (“False” means not executable, “True” means executable), not taking user access rights into account.
    /// If the server cannot get the executable information from the underlying system, it should state that it is executable. If a Method is called, the server should transfer
    /// this request and return the corresponding StatusCode if such a request is rejected.
    /// </summary>
    /// <value><c>true</c> if executable; otherwise, <c>false</c>. Default value is <c>true</c></value>
    public bool? Executable
    {
      set { }
    }
    /// <summary>
    /// Sets a value indicating whether the Method is currently executable taking user access rights into account (“False” means not executable, “True” means executable).
    /// </summary>
    /// <value><c>true</c> if executable by current user; otherwise, <c>false</c>. Default value is <c>true</c></value>
    public bool? UserExecutable
    {
      set { }
    }
    /// <summary>
    /// Adds the input arguments. The InputArgument specify the input argument of the Method. The Method contains an array of the Argument data type.
    /// An empty array indicates that there are no input arguments for the Method.
    /// </summary>
    /// <param name="argument">Encapsulates a method used to convert Argument represented as <see cref="XmlElement" />.</param>
    public void AddInputArguments(System.Func<XmlElement, Parameter[]> argument)
    {
      RemoveArguments(BrowseNames.InputArguments, argument);
    }
    /// <summary>
    /// Adds the output argument. The OutputArgument specifies the output argument of the Method. The Method contains an array of the Argument data type.
    /// An empty array indicates that there are no output arguments for the Method.
    /// </summary>
    /// <param name="argument">Encapsulates a method used to convert Argument represented as <see cref="XmlElement" />.</param>
    public void AddOutputArguments(System.Func<XmlElement, Parameter[]> argument)
    {
      RemoveArguments(BrowseNames.OutputArguments, argument);
    }
    /// <summary>
    /// Adds the argument description.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="locale">The locale.</param>
    /// <param name="value">The value.</param>
    public void AddArgumentDescription(string name, string locale, string value) { }
    #endregion

    private Parameter[] RemoveArguments(string parameterKind, Func<XmlElement, Parameter[]> getParameters)
    {
      Parameter[] _parameters = null;
      List<NodeFactoryBase> _newChildrenCollection = new List<NodeFactoryBase>();
      foreach (NodeFactoryBase _item in m_Nodes)
      {
        if (_item.SymbolicName.Equals(new XmlQualifiedName(parameterKind, Namespaces.OpcUa)))
        {
          PropertyInstanceFactoryBase _arg = (PropertyInstanceFactoryBase)_item;
          _parameters = getParameters(_arg.DefaultValue);
        }
        else
          _newChildrenCollection.Add(_item);
      }
      m_Nodes = _newChildrenCollection;
      return _parameters;
    }

  }
}
