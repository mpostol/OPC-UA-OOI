//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;


namespace UAOOI.SemanticData.UAModelDesignExport
{
  /// <summary>
  /// Class VariableTypeFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.UAModelDesignExport.TypeFactoryBase" />
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IVariableTypeFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.UAModelDesignExport.TypeFactoryBase" />
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IVariableTypeFactory" />
  internal class VariableTypeFactoryBase : TypeFactoryBase, IVariableTypeFactory
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="VariableTypeFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public VariableTypeFactoryBase(Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    #region IVariableTypeFactory
    /// <summary>
    /// Sets the default value. The value of the Variable node that the server assigns while instantiating the node. Its data type is defined by the <see cref="P:UAOOI.SemanticData.InformationModelFactory.IDataDescriptor.DataType" />.
    /// </summary>
    /// <value>The default value.</value>
    public XmlElement DefaultValue
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the type of the data. <see cref="T:System.Xml.XmlQualifiedName" /> of the DataType definition for the Value. It is not required that the pointed out element is defined in the same document.
    /// If that is the case many documents must be combined to resolve and validate this reference.
    /// </summary>
    /// <value>The type of the data.</value>
    public XmlQualifiedName DataType
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the value rank. This property indicates whether the value is an array and how many dimensions the array has.
    /// It may have the following values:
    /// - n &gt; 1: the Value is an array with the specified number of dimensions.
    /// - <b>&gt;OneDimension (1)</b>: The value is an array with one dimension.
    /// - OneOrMoreDimensions (0): The value is an array with one or more dimensions.
    /// - Scalar (−1): The value is not an array.
    /// - Any (−2): The value can be a scalar or an array with any number of dimensions.
    /// - ScalarOrOneDimension (−3): The value can be a scalar or a one dimensional array.
    /// NOTE: All build in DataTypes are considered to be scalar, even if they have array-like semantics like ByteString and String.
    /// </summary>
    /// <value>The value rank.</value>
    public int? ValueRank
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the array dimensions. This property specifies the length of each dimension for an array value. It is intended to describe the capability of the Variable, not the current size.
    /// The number of elements shall be equal to the value defined by the ValueRank. It shall be null if ValueRank ≤ 0. The value of 0 for an individual dimension indicates that the dimension has
    /// a variable length. For example, if a Variable is defined by the following C array:
    /// Int32 myArray[346];
    /// then the DataType would point to an Int32, the ValueRank has the value 1 and the ArrayDimensions is an array with one entry having the value 346.
    /// Note that the maximum length of an array transferred on the wire is 2147483647 (max Int32) and a multidimensional array is encoded as a one dimensional array.
    /// </summary>
    /// <value>The array dimensions.</value>
    /// <remarks>ArrayDimensions is ignored if ValueRank is not equal to the OneOrMoreDimensions.</remarks>
    public string ArrayDimensions
    {
      set;
      private get;
    }
    #endregion

    //internal API
    internal override NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      VariableTypeDesign _ret = new VariableTypeDesign()
      {
        ArrayDimensions = this.ArrayDimensions,
        DataType = this.DataType,
        DefaultValue = this.DefaultValue,
        //Not supported by the VariableType NodeClass 
        ExposesItsChildren = false,
        AccessLevel =  AccessLevel.ReadWrite,
        AccessLevelSpecified = false,
        Historizing = false,
        HistorizingSpecified = false,
        MinimumSamplingInterval = 0,
        MinimumSamplingIntervalSpecified = false,
      };
      _ret.ValueRank = this.ValueRank.GetValueRank(x => _ret.ValueRankSpecified = x, TraceEvent);
      base.Update(_ret, path, createInstanceType);
      return _ret;
    }
  }
}
