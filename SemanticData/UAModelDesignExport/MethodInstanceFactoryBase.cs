//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  internal class MethodInstanceFactoryBase : InstanceFactoryBase, IMethodInstanceFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="InstanceFactoryBase" /> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public MethodInstanceFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    #region IMethodInstanceFactory
    /// <summary>
    /// Sets a value indicating whether the Method node is executable (“False” means not executable, “True” means executable), not taking user access rights into account.
    /// If the server cannot get the executable information from the underlying system, it should state that it is executable. If a Method is called, the server should transfer
    /// this request and return the corresponding StatusCode if such a request is rejected.
    /// </summary>
    /// <value><c>true</c> if executable; otherwise, <c>false</c>. Default value is <c>true</c></value>
    public bool? Executable
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets a value indicating whether the Method is currently executable taking user access rights into account (“False” means not executable, “True” means executable).
    /// </summary>
    /// <value><c>true</c> if executable by current user; otherwise, <c>false</c>. Default value is <c>true</c></value>
    public bool? UserExecutable
    {
      set;
      private get;
    }
    /// <summary>
    /// Adds the input arguments. The InputArgument specify the input argument of the Method. The Method contains an array of the Argument data type.
    /// An empty array indicates that there are no input arguments for the Method.
    /// </summary>
    /// <param name="argument">Encapsulates a method used to convert Argument represented as <see cref="T:System.Xml.XmlElement" />.</param>
    public void AddInputArguments(Func<System.Xml.XmlElement, Parameter[]> argument)
    {
      m_InputArguments = RemoveArguments(BrowseNames.InputArguments, argument);
    }
    /// <summary>
    /// Adds the output argument. The OutputArgument specifies the output argument of the Method. The Method contains an array of the Argument data type.
    /// An empty array indicates that there are no output arguments for the Method.
    /// </summary>
    /// <param name="argument">Encapsulates a method used to convert Argument represented as <see cref="T:System.Xml.XmlElement" />.</param>
    public void AddOutputArguments(Func<System.Xml.XmlElement, Parameter[]> argument)
    {
      m_OutputArguments = RemoveArguments(BrowseNames.OutputArguments, argument);
    }
    /// <summary>
    /// Adds the argument description.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="locale">The locale.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="NotImplementedException"></exception>
    public void AddArgumentDescription(string name, string locale, string value)
    {
      //TODO to be removed in UANodeSet.xsd - synchronize with current OPCF Release #207
      throw new NotImplementedException();
    }
    #endregion

    //internal API
    /// <summary>
    /// Exports the an instance of <see cref="XML.NodeDesign"/>.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="createInstanceType">Type of the create instance.</param>
    /// <returns>XML.NodeDesign.</returns>
    internal override XML.NodeDesign Export(List<string> path, Action<XML.InstanceDesign, List<string>> createInstanceType)
    {
      XML.MethodDesign _new = new XML.MethodDesign()
      {
        InputArguments = GetArguments(m_InputArguments),
        OutputArguments = GetArguments(m_OutputArguments),
        NonExecutable = Executable.GetValueOrDefault(false),
        NonExecutableSpecified = Executable.HasValue
      };
      base.UpdateInstance(_new, path, TraceEvent, createInstanceType);
      createInstanceType(_new, path);
      return _new;
    }

    #region private
    private XML.Parameter[] GetArguments(IEnumerable<Parameter> parameter)
    {
      return parameter?.Select<Parameter, XML.Parameter>(x => x.ExportArgument(TraceEvent)).ToArray<XML.Parameter>();
    }
    private IEnumerable<Parameter> m_InputArguments = null;
    private IEnumerable<Parameter> m_OutputArguments = null;
    private IEnumerable<Parameter> RemoveArguments(string parameterKind, Func<XmlElement, Parameter[]> getParameters)
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
      return _parameters == null || _parameters.Length == 0 ? null : _parameters.AsEnumerable<Parameter>();
    }

    #endregion
  }

}
