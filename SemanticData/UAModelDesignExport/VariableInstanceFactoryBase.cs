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

  internal class VariableInstanceFactoryBase : InstanceFactoryBase, IVariableInstanceFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="InstanceFactoryBase" /> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    public VariableInstanceFactoryBase(System.Action<TraceMessage> traceEvent)
      : base(traceEvent)
    { }

    #region IVariableInstanceFactory
    /// <summary>
    /// Sets the access level. The AccessLevel attribute indicates the accessibility of the Value of a Variable node not taking user access rights into account and
    /// applies only to a UAVariable element. The AccessLevel attribute is used to indicate how the Value of a Variable node can be accessed (read/write) and
    /// if it contains current and/or historic data. The AccessLevel does not take any user access rights into account, i.e. although the Variable is writable this
    /// may be restricted to a certain user / user group.
    /// </summary>
    /// <value>The access level.</value>
    public byte? AccessLevel
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
    /// Sets the default value. The value of the Variable node that the server assigns while instantiating the node. Its data type is defined by the DataType field.
    /// </summary>
    /// <value>The default value.</value>
    public XmlElement DefaultValue
    {
      set;
      internal get;
    }
    /// <summary>
    /// Sets a value indicating whether this <see cref="T:UAOOI.SemanticData.InformationModelFactory.IVariableInstanceFactory" /> is historizing. The Historizing attribute indicates whether the server is actively
    /// collecting data for the history of the Variable node. This differs from the AccessLevel which identifies if the Variable has any historical data. A value of <c>true</c>
    /// indicates that the server is actively collecting data. A value of <c>false</c> indicates that  the server is not actively collecting data. Default value is <c>false</c>.
    /// </summary>
    /// <value><c>true</c> if historizing; otherwise, <c>false</c>.</value>
    public bool? Historizing
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the minimum sampling interval. The MinimumSamplingInterval attribute indicates how “current” the Value of the Variable NodeClass will be kept.
    /// It specifies (in milliseconds) how fast the server can reasonably sample the value for changes. The accuracy of this value (the ability of the server to attain
    /// “best case” performance) can be greatly affected by the system load and other factors. A MinimumSamplingInterval of 0 indicates that the server is to monitor the
    /// item continuously. A MinimumSamplingInterval of -1 means indeterminate value.
    /// </summary>
    /// <value>The minimum sampling interval.</value>
    public int? MinimumSamplingInterval
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the user access level. The UserAccessLevel attribute is used to indicate how the Value attribute of a Variable NodeClass can be accessed (read/write)
    /// and if it contains current or historic data taking user access rights into account. It applies only to a UAVariable element. If the OPC UA Server does not
    /// have the ability to get any user access rights related information from the underlying system it should use the same bit mask as used in the AccessLevel attribute.
    /// The UserAccessLevel attribute can restrict the accessibility indicated by the AccessLevel, but not exceed it.
    /// </summary>
    /// <value>The user access level.</value>
    public byte? UserAccessLevel
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
    #endregion

    //internal API
    internal override NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      VariableDesign _ret = new VariableDesign() { };
      Update(_ret, path, createInstanceType);
      return _ret;
    }

    //private
    protected void Update(VariableDesign node, List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
    {
      node.AccessLevel = this.AccessLevel.GetAccessLevel(x => node.AccessLevelSpecified = x, TraceEvent);
      node.ValueRank = this.ValueRank.GetValueRank(x => node.ValueRankSpecified = x, TraceEvent);
      node.ArrayDimensions = this.ArrayDimensions;
      node.DataType = this.DataType; //TODO must be DataType, must not be abstract
      node.DefaultValue = this.DefaultValue; //TODO must be of type defined by DataType
      node.Historizing = this.Historizing.GetValueOrDefault();
      node.HistorizingSpecified = this.Historizing.HasValue;
      node.MinimumSamplingInterval = this.MinimumSamplingInterval.GetValueOrDefault();
      node.MinimumSamplingIntervalSpecified = this.MinimumSamplingInterval.HasValue;
      base.UpdateInstance(node, path, TraceEvent, createInstanceType);
    }

  }

}
