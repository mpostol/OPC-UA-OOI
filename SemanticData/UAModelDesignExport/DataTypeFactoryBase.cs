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
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  internal class DataTypeFactoryBase : TypeFactoryBase, IDataTypeFactory
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NodeFactoryBase" /> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public DataTypeFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    //IDataTypeFactory
    public IDataTypeDefinitionFactory NewDefinition()
    {
      Debug.Assert(m_DataTypeDefinitionFactoryBase == null);
      m_DataTypeDefinitionFactoryBase = new DataTypeDefinitionFactoryBase(TraceEvent);
      return m_DataTypeDefinitionFactoryBase;
    }

    //internal  API
    internal override NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      DataTypeDesign _new = new DataTypeDesign()
      {
        Fields = m_DataTypeDefinitionFactoryBase.Export().ToArray<XML.Parameter>(),
        Encodings = null, //Not supported
        NoArraysAllowed = false, //Not supported
        NotInAddressSpace = false //Not supported
      };
      base.Update(_new, path, createInstanceType);
      return _new;
    }

    //private
    private DataTypeDefinitionFactoryBase m_DataTypeDefinitionFactoryBase = null;

  }
}
