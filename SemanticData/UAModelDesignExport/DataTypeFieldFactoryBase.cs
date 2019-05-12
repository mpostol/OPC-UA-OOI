//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using TraceMessage = UAOOI.SemanticData.BuildingErrorsHandling.TraceMessage;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  /// <summary>
  /// Class DataTypeFieldFactoryBase.
  /// Implements the <see cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" />
  /// </summary>
  /// <seealso cref="UAOOI.SemanticData.InformationModelFactory.IDataTypeFieldFactory" />
  internal class DataTypeFieldFactoryBase : IDataTypeFieldFactory
  {

    #region constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="DataTypeFieldFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    internal DataTypeFieldFactoryBase(Action<TraceMessage> traceEvent)
    {
      Debug.Assert(traceEvent != null);
      TraceEvent = traceEvent;
    }
    #endregion

    #region IDataTypeFieldFactory
    /// <summary>
    /// Sets the DataType name.
    /// </summary>
    /// <value>The type of the data.</value>
    /// <remarks>This field is not specified for subtypes of Enumeration.</remarks>
    public XmlQualifiedName DataType
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the name for the field that is unique within the <see cref="T:UAOOI.SemanticData.InformationModelFactory.IDataTypeDefinitionFactory" />.
    /// </summary>
    /// <value>The name for the field.</value>
    public string Name { set; private get; }
    /// <summary>
    /// Sets the value rank. It shall be Scalar (-1) or a fixed rank Array (&gt;=1). This field is not specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The value rank.</value>
    public int? ValueRank { set; private get; }
    /// <summary>
    /// The value associated with the field. This field is only specified for subtypes of Enumeration.
    /// For OptionSets the value is the number of the bit associated with the field.
    /// </summary>
    /// <value>The value.</value>
    public int Value { set; private get; }
    /// <summary>
    /// Sets the symbolic name of the field. A symbolic name for the field that can be used in auto-generated code. It should only be
    /// specified if the Name cannot be used for this purpose. Only letters, digits or the underscore (‘_’) are permitted.
    /// This value is not exposed in the OPC UA Address Space
    /// </summary>
    /// <value>The symbolic name to be used by the tool.</value>
    public string SymbolicName { set; private get; }
    /// <summary>
    /// Adds the description for the field in multiple locales
    /// </summary>
    /// <param name="localeField">The locale field specified as a string that is composed of a language component and a country/region component as specified by RFC 3066.</param>
    /// <param name="valueField">The localized text.</param>
    public void AddDescription(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_Description, TraceEvent);
    }
    /// <summary>
    /// Adds the display name.
    /// </summary>
    /// <param name="localeField">The locale field specified as a string that is composed of a language component and a country/region component as specified by RFC 3066.</param>
    /// <param name="valueField">The localized text.</param>
    public void AddDisplayName(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_Description, TraceEvent);
    }
    /// <summary>
    /// Gets the array dimensions.
    /// </summary>
    /// <value>The array dimensions.</value>
    /// <remarks>The maximum length of an array. This field is a comma separated list of unsigned integer values.The list has a number of elements equal to the ValueRank.
    /// The value is 0 if the maximum is not known for a dimension. This field is not specified if the ValueRank less or equal 0.
    /// This field is not specified for subtypes of Enumeration or for DataTypes</remarks>
    public string ArrayDimensions { set; private get; }
    /// <summary>
    /// Sets the maximum length of the string.
    /// </summary>
    /// <value>The maximum length of the string.</value>
    /// <remarks>The maximum length of a String or ByteString value. If not known the value is 0. The value is 0 if the DataType is not String or ByteString.
    /// If the ValueRank &gt; 0 the maximum applies to each element in the array. This field is not specified for subtypes of Enumeration or for DataTypes with
    /// the OptionSetValues Property.</remarks>
    public uint MaxStringLength { set; private get; }
    /// <summary>
    /// Sets a value indicating whether this instance is optional.
    /// </summary>
    /// <value><c>true</c> if this instance is optional; otherwise, <c>false</c>.</value>
    /// <remarks>The field indicates if a data type field in a structure is optional. This field is optional.The default value is false. This field is not specified for subtypes of Enumeration and Union.</remarks>
    public bool IsOptional { set; private get; }
    #endregion

    #region internal API
    /// <summary>
    /// Exports this instance to <see cref="XML.Parameter "/>.
    /// </summary>
    /// <returns>XML.Parameter.</returns>
    internal XML.Parameter Export()
    {
      bool _ValueRankSpecified;
      XML.Parameter _newParameter = new XML.Parameter()
      {
        DataType = DataType,
        Description = m_Description,
        Identifier = Value,
        IdentifierSpecified = true,
        Name = Name,
        ValueRank = ValueRank.GetValueRank(x => _ValueRankSpecified = x, TraceEvent),
        ArrayDimensions = ArrayDimensions,
        BitMask = string.Empty,
      };
      return _newParameter;
    }
    #endregion

    #region private
    private Action<TraceMessage> TraceEvent { get; set; }
    private XML.LocalizedText m_Description = null;
    #endregion

  }

}
