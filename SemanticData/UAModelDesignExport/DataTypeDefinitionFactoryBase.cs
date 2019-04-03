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
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  /// <summary>
  /// Class DataTypeDefinitionFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" />
  internal class DataTypeDefinitionFactoryBase : IDataTypeDefinitionFactory
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="DataTypeDefinitionFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public DataTypeDefinitionFactoryBase(Action<TraceMessage> traceEvent)
    {
      Debug.Assert(traceEvent != null);
      m_TraceEvent = traceEvent;
    }
    #endregion

    #region IDataTypeDefinitionFactory
    /// <summary>
    /// Creates new field and provides an object of <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" /> type encapsulating 
    /// information about the field data type. It is assumed that the structure has a sequential layout.For enumerations, the fields are simply a list of values.
    /// </summary>
    /// <returns>Returns new instance of the <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" />.</returns>
    public IDataTypeFieldFactory NewField()
    {
      DataTypeFieldFactoryBase _new = new DataTypeFieldFactoryBase(m_TraceEvent);
      m_ListOfDataTypeFieldFactoryBase.Add(_new);
      return _new;
    }
    /// <summary>
    /// Sets a unique name of the DataType. This field is only specified for nested DataTypeDefinitions.
    /// The BrowseName of the DataType Node is used otherwise.
    /// This field is only specified for nested DataTypeDefinitions. The SymbolicName of the DataType Node is used otherwise.
    /// </summary>
    /// <remarks>
    /// TODO Mantis Not supported by the UA Model Design
    /// </remarks>
    /// <value>The name represented as <see cref="T:System.Xml.XmlQualifiedName" />.</value>
    public XmlQualifiedName Name { set; private get; }
    /// <summary>
    /// A symbolic name for the data type. It should only be specified if the Name cannot be used for this purpose.
    /// Only letters, digits or the underscore (‘_’) are permitted.
    /// </summary>
    /// <remarks>
    /// TODO Mantis Not supported by the UA Model Design
    /// </remarks>
    /// <value>The symbolic name of thi entity.</value>
    public string SymbolicName { set; private get; }
    /// <summary>
    /// Sets a value indicating whether this instance is option set. This flag indicates that the data type defines the OptionSetValues Property.
    /// This field is optional.The default value is false.
    /// </summary>
    /// <value><c>true</c> if this instance is option set; otherwise, <c>false</c>.</value>
    public bool IsOptionSet { set; private get; }
    /// <summary>
    /// Sets a value indicating whether this instance is union.
    /// Only one of the Fields defined for the data type is encoded into a value.
    /// This field is optional.The default value is false. If this value is true, the first field is the switch value.
    /// </summary>
    /// <remarks>
    /// TODO Mantis Not supported by the UA Model Design
    /// </remarks>
    /// <value><c>true</c> if this instance is union; otherwise, <c>false</c>.</value>
    public bool IsUnion { set; private get; } 
    #endregion

    #region internal API
    internal XML.DataTypeDesign Export()
    {
      return new XML.DataTypeDesign()
      {
        Fields = m_ListOfDataTypeFieldFactoryBase.Select<DataTypeFieldFactoryBase, XML.Parameter>(x => x.Export()).ToArray<XML.Parameter>(),
        Encodings = null, //Not supported
        NoArraysAllowed = false, //Not supported
        IsOptionSet = IsOptionSet,
      };
    }
    #endregion

    #region private
    private readonly Action<TraceMessage> m_TraceEvent = null;
    private List<DataTypeFieldFactoryBase> m_ListOfDataTypeFieldFactoryBase = new List<DataTypeFieldFactoryBase>();
    #endregion

  }

}
