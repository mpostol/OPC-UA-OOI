//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using TraceMessage = UAOOI.SemanticData.UANodeSetValidation.TraceMessage;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  internal class DataTypeDefinitionFactoryBase : IDataTypeDefinitionFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTypeDefinitionFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public DataTypeDefinitionFactoryBase(Action<TraceMessage> traceEvent)
    {
      Debug.Assert(traceEvent != null);
      m_TraceEvent = traceEvent;
    }

    #region IDataTypeDefinitionFactory
    public IDataTypeFieldFactory NewField()
    {
      DataTypeFieldFactoryBase _new = new DataTypeFieldFactoryBase(m_TraceEvent);
      m_ListOfDataTypeFieldFactoryBase.Add(_new);
      return _new;
    }
    public XmlQualifiedName Name
    {
      set { }
    }
    public XmlQualifiedName BaseType
    {
      set { }
    }
    public string SymbolicName
    {
      set { }
    }
    #endregion

    #region internal API
    internal IEnumerable<XML.Parameter> Export()
    {
      return m_ListOfDataTypeFieldFactoryBase.Select<DataTypeFieldFactoryBase, XML.Parameter>(x => x.Export());
    }
    #endregion

    #region private
    private Action<TraceMessage> m_TraceEvent = null;
    private List<DataTypeFieldFactoryBase> m_ListOfDataTypeFieldFactoryBase = new List<DataTypeFieldFactoryBase>();
    #endregion

  }

}
