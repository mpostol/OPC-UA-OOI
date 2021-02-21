/* ========================================================================
 * Copyright (c) 2005-2016 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using ;

namespace DataTypeTest
{
    #region DataType Identifiers
    /// <summary>
    /// A class that declares constants for all DataTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class DataTypes
    {
        /// <summary>
        /// The identifier for the AbstractEnumerationDataType DataType.
        /// </summary>
        public const uint AbstractEnumerationDataType = 9;

        /// <summary>
        /// The identifier for the EnumerationDataType DataType.
        /// </summary>
        public const uint EnumerationDataType = 11;

        /// <summary>
        /// The identifier for the AbstractStructure DataType.
        /// </summary>
        public const uint AbstractStructure = 13;

        /// <summary>
        /// The identifier for the DerivedStructure DataType.
        /// </summary>
        public const uint DerivedStructure = 22;
    }
    #endregion

    #region Object Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Objects
    {
        /// <summary>
        /// The identifier for the AbstractStructure_Encoding_DefaultXml Object.
        /// </summary>
        public const uint AbstractStructure_Encoding_DefaultXml = 14;

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultXml Object.
        /// </summary>
        public const uint DerivedStructure_Encoding_DefaultXml = 23;

        /// <summary>
        /// The identifier for the AbstractStructure_Encoding_DefaultBinary Object.
        /// </summary>
        public const uint AbstractStructure_Encoding_DefaultBinary = 18;

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultBinary Object.
        /// </summary>
        public const uint DerivedStructure_Encoding_DefaultBinary = 27;
    }
    #endregion

    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <summary>
        /// The identifier for the AbstractEnumerationDataType_EnumStrings Variable.
        /// </summary>
        public const uint AbstractEnumerationDataType_EnumStrings = 10;

        /// <summary>
        /// The identifier for the EnumerationDataType_EnumValues Variable.
        /// </summary>
        public const uint EnumerationDataType_EnumValues = 12;

        /// <summary>
        /// The identifier for the cas_XmlSchema Variable.
        /// </summary>
        public const uint cas_XmlSchema = 2;

        /// <summary>
        /// The identifier for the cas_XmlSchema_NamespaceUri Variable.
        /// </summary>
        public const uint cas_XmlSchema_NamespaceUri = 4;

        /// <summary>
        /// The identifier for the cas_XmlSchema_AbstractStructure Variable.
        /// </summary>
        public const uint cas_XmlSchema_AbstractStructure = 15;

        /// <summary>
        /// The identifier for the cas_XmlSchema_DerivedStructure Variable.
        /// </summary>
        public const uint cas_XmlSchema_DerivedStructure = 24;

        /// <summary>
        /// The identifier for the cas_BinarySchema Variable.
        /// </summary>
        public const uint cas_BinarySchema = 5;

        /// <summary>
        /// The identifier for the cas_BinarySchema_NamespaceUri Variable.
        /// </summary>
        public const uint cas_BinarySchema_NamespaceUri = 7;

        /// <summary>
        /// The identifier for the cas_BinarySchema_AbstractStructure Variable.
        /// </summary>
        public const uint cas_BinarySchema_AbstractStructure = 19;

        /// <summary>
        /// The identifier for the cas_BinarySchema_DerivedStructure Variable.
        /// </summary>
        public const uint cas_BinarySchema_DerivedStructure = 28;
    }
    #endregion

    #region DataType Node Identifiers
    /// <summary>
    /// A class that declares constants for all DataTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class DataTypeIds
    {
        /// <summary>
        /// The identifier for the AbstractEnumerationDataType DataType.
        /// </summary>
        public static readonly ExpandedNodeId AbstractEnumerationDataType = new ExpandedNodeId(DataTypeTest.DataTypes.AbstractEnumerationDataType, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the EnumerationDataType DataType.
        /// </summary>
        public static readonly ExpandedNodeId EnumerationDataType = new ExpandedNodeId(DataTypeTest.DataTypes.EnumerationDataType, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the AbstractStructure DataType.
        /// </summary>
        public static readonly ExpandedNodeId AbstractStructure = new ExpandedNodeId(DataTypeTest.DataTypes.AbstractStructure, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedStructure DataType.
        /// </summary>
        public static readonly ExpandedNodeId DerivedStructure = new ExpandedNodeId(DataTypeTest.DataTypes.DerivedStructure, DataTypeTest.Namespaces.cas);
    }
    #endregion

    #region Object Node Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectIds
    {
        /// <summary>
        /// The identifier for the AbstractStructure_Encoding_DefaultXml Object.
        /// </summary>
        public static readonly ExpandedNodeId AbstractStructure_Encoding_DefaultXml = new ExpandedNodeId(DataTypeTest.Objects.AbstractStructure_Encoding_DefaultXml, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultXml Object.
        /// </summary>
        public static readonly ExpandedNodeId DerivedStructure_Encoding_DefaultXml = new ExpandedNodeId(DataTypeTest.Objects.DerivedStructure_Encoding_DefaultXml, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the AbstractStructure_Encoding_DefaultBinary Object.
        /// </summary>
        public static readonly ExpandedNodeId AbstractStructure_Encoding_DefaultBinary = new ExpandedNodeId(DataTypeTest.Objects.AbstractStructure_Encoding_DefaultBinary, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultBinary Object.
        /// </summary>
        public static readonly ExpandedNodeId DerivedStructure_Encoding_DefaultBinary = new ExpandedNodeId(DataTypeTest.Objects.DerivedStructure_Encoding_DefaultBinary, DataTypeTest.Namespaces.cas);
    }
    #endregion

    #region Variable Node Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableIds
    {
        /// <summary>
        /// The identifier for the AbstractEnumerationDataType_EnumStrings Variable.
        /// </summary>
        public static readonly ExpandedNodeId AbstractEnumerationDataType_EnumStrings = new ExpandedNodeId(DataTypeTest.Variables.AbstractEnumerationDataType_EnumStrings, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the EnumerationDataType_EnumValues Variable.
        /// </summary>
        public static readonly ExpandedNodeId EnumerationDataType_EnumValues = new ExpandedNodeId(DataTypeTest.Variables.EnumerationDataType_EnumValues, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema = new ExpandedNodeId(DataTypeTest.Variables.cas_XmlSchema, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema_NamespaceUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema_NamespaceUri = new ExpandedNodeId(DataTypeTest.Variables.cas_XmlSchema_NamespaceUri, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema_AbstractStructure Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema_AbstractStructure = new ExpandedNodeId(DataTypeTest.Variables.cas_XmlSchema_AbstractStructure, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema_DerivedStructure Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema_DerivedStructure = new ExpandedNodeId(DataTypeTest.Variables.cas_XmlSchema_DerivedStructure, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema = new ExpandedNodeId(DataTypeTest.Variables.cas_BinarySchema, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema_NamespaceUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema_NamespaceUri = new ExpandedNodeId(DataTypeTest.Variables.cas_BinarySchema_NamespaceUri, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema_AbstractStructure Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema_AbstractStructure = new ExpandedNodeId(DataTypeTest.Variables.cas_BinarySchema_AbstractStructure, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema_DerivedStructure Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema_DerivedStructure = new ExpandedNodeId(DataTypeTest.Variables.cas_BinarySchema_DerivedStructure, DataTypeTest.Namespaces.cas);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the AbstractEnumerationDataType component.
        /// </summary>
        public const string AbstractEnumerationDataType = "AbstractEnumerationDataType";

        /// <summary>
        /// The BrowseName for the AbstractStructure component.
        /// </summary>
        public const string AbstractStructure = "AbstractStructure";

        /// <summary>
        /// The BrowseName for the cas_BinarySchema component.
        /// </summary>
        public const string cas_BinarySchema = "DataTypeTest";

        /// <summary>
        /// The BrowseName for the cas_XmlSchema component.
        /// </summary>
        public const string cas_XmlSchema = "DataTypeTest";

        /// <summary>
        /// The BrowseName for the DerivedStructure component.
        /// </summary>
        public const string DerivedStructure = "DerivedStructure";

        /// <summary>
        /// The BrowseName for the EnumerationDataType component.
        /// </summary>
        public const string EnumerationDataType = "EnumerationDataType";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the cas namespace (.NET code namespace is 'DataTypeTest').
        /// </summary>
        public const string cas = "http://cas.eu/UA/CommServer/UnitTests/DataTypeTest";

        /// <summary>
        /// The URI for the ua namespace (.NET code namespace is '').
        /// </summary>
        public const string ua = "http://opcfoundation.org/UA/";
    }
    #endregion

    #region AbstractEnumerationDataType Enumeration
    #if (!OPCUA_EXCLUDE_AbstractEnumerationDataType)
    /// <summary>
    /// A description for the AbstractEnumerationDataType DataType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = DataTypeTest.Namespaces.cas)]
    public enum AbstractEnumerationDataType
    {
        /// <summary>
        /// A description for the Field1 field.
        /// </summary>
        [EnumMember(Value = "Field1_0")]
        Field1 = 0,

        /// <summary>
        /// A description for the Field2 field.
        /// </summary>
        [EnumMember(Value = "Field2_1")]
        Field2 = 1,
    }

    #region AbstractEnumerationDataTypeCollection Class
    /// <summary>
    /// A collection of AbstractEnumerationDataType objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfAbstractEnumerationDataType", Namespace = DataTypeTest.Namespaces.cas, ItemName = "AbstractEnumerationDataType")]
    #if !NET_STANDARD
    public partial class AbstractEnumerationDataTypeCollection : List<AbstractEnumerationDataType>, ICloneable
    #else
    public partial class AbstractEnumerationDataTypeCollection : List<AbstractEnumerationDataType>
    #endif
    {
        #region Constructors
        /// <summary>
        /// Initializes the collection with default values.
        /// </summary>
        public AbstractEnumerationDataTypeCollection() {}

        /// <summary>
        /// Initializes the collection with an initial capacity.
        /// </summary>
        public AbstractEnumerationDataTypeCollection(int capacity) : base(capacity) {}

        /// <summary>
        /// Initializes the collection with another collection.
        /// </summary>
        public AbstractEnumerationDataTypeCollection(IEnumerable<AbstractEnumerationDataType> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator AbstractEnumerationDataTypeCollection(AbstractEnumerationDataType[] values)
        {
            if (values != null)
            {
                return new AbstractEnumerationDataTypeCollection(values);
            }

            return new AbstractEnumerationDataTypeCollection();
        }

        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator AbstractEnumerationDataType[](AbstractEnumerationDataTypeCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <summary>
        /// Creates a deep copy of the collection.
        /// </summary>
        public object Clone()
        {
            return (AbstractEnumerationDataTypeCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            AbstractEnumerationDataTypeCollection clone = new AbstractEnumerationDataTypeCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((AbstractEnumerationDataType)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region EnumerationDataType Enumeration
    #if (!OPCUA_EXCLUDE_EnumerationDataType)
    /// <summary>
    /// Enumeration DataType derived from abstract type
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = DataTypeTest.Namespaces.cas)]
    public enum EnumerationDataType
    {
        /// <summary>
        /// A description for the Field3 field.
        /// </summary>
        [EnumMember(Value = "Field3_1")]
        Field3 = 1,
    }

    #region EnumerationDataTypeCollection Class
    /// <summary>
    /// A collection of EnumerationDataType objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfEnumerationDataType", Namespace = DataTypeTest.Namespaces.cas, ItemName = "EnumerationDataType")]
    #if !NET_STANDARD
    public partial class EnumerationDataTypeCollection : List<EnumerationDataType>, ICloneable
    #else
    public partial class EnumerationDataTypeCollection : List<EnumerationDataType>
    #endif
    {
        #region Constructors
        /// <summary>
        /// Initializes the collection with default values.
        /// </summary>
        public EnumerationDataTypeCollection() {}

        /// <summary>
        /// Initializes the collection with an initial capacity.
        /// </summary>
        public EnumerationDataTypeCollection(int capacity) : base(capacity) {}

        /// <summary>
        /// Initializes the collection with another collection.
        /// </summary>
        public EnumerationDataTypeCollection(IEnumerable<EnumerationDataType> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator EnumerationDataTypeCollection(EnumerationDataType[] values)
        {
            if (values != null)
            {
                return new EnumerationDataTypeCollection(values);
            }

            return new EnumerationDataTypeCollection();
        }

        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator EnumerationDataType[](EnumerationDataTypeCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <summary>
        /// Creates a deep copy of the collection.
        /// </summary>
        public object Clone()
        {
            return (EnumerationDataTypeCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            EnumerationDataTypeCollection clone = new EnumerationDataTypeCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((EnumerationDataType)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region AbstractStructure Class
    #if (!OPCUA_EXCLUDE_AbstractStructure)
    /// <summary>
    /// Abstract structure
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = DataTypeTest.Namespaces.cas)]
    public partial class AbstractStructure : IEncodeable
    {
        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public AbstractStructure()
        {
            Initialize();
        }

        /// <summary>
        /// Called by the .NET framework during deserialization.
        /// </summary>
        [OnDeserializing]
        private void Initialize(StreamingContext context)
        {
            Initialize();
        }

        /// <summary>
        /// Sets private members to default values.
        /// </summary>
        private void Initialize()
        {
            m_number = (double)0;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the Number field.
        /// </summary>
        [DataMember(Name = "Number", IsRequired = false, Order = 1)]
        public Variant Number
        {
            get { return m_number;  }
            set { m_number = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId
        {
            get { return DataTypeIds.AbstractStructure; }
        }

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId
        {
            get { return ObjectIds.AbstractStructure_Encoding_DefaultBinary; }
        }

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId
        {
            get { return ObjectIds.AbstractStructure_Encoding_DefaultXml; }
        }

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(DataTypeTest.Namespaces.cas);

            encoder.WriteVariant("Number", Number);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(DataTypeTest.Namespaces.cas);

            Number = decoder.ReadVariant("Number");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            AbstractStructure value = encodeable as AbstractStructure;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_number, value.m_number)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (AbstractStructure)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            AbstractStructure clone = (AbstractStructure)base.MemberwiseClone();

            clone.m_number = (Variant)Utils.Clone(this.m_number);

            return clone;
        }
        #endregion

        #region Private Fields
        private Variant m_number;
        #endregion
    }

    #region AbstractStructureCollection Class
    /// <summary>
    /// A collection of AbstractStructure objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfAbstractStructure", Namespace = DataTypeTest.Namespaces.cas, ItemName = "AbstractStructure")]
    #if !NET_STANDARD
    public partial class AbstractStructureCollection : List<AbstractStructure>, ICloneable
    #else
    public partial class AbstractStructureCollection : List<AbstractStructure>
    #endif
    {
        #region Constructors
        /// <summary>
        /// Initializes the collection with default values.
        /// </summary>
        public AbstractStructureCollection() {}

        /// <summary>
        /// Initializes the collection with an initial capacity.
        /// </summary>
        public AbstractStructureCollection(int capacity) : base(capacity) {}

        /// <summary>
        /// Initializes the collection with another collection.
        /// </summary>
        public AbstractStructureCollection(IEnumerable<AbstractStructure> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator AbstractStructureCollection(AbstractStructure[] values)
        {
            if (values != null)
            {
                return new AbstractStructureCollection(values);
            }

            return new AbstractStructureCollection();
        }

        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator AbstractStructure[](AbstractStructureCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <summary>
        /// Creates a deep copy of the collection.
        /// </summary>
        public object Clone()
        {
            return (AbstractStructureCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            AbstractStructureCollection clone = new AbstractStructureCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((AbstractStructure)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region DerivedStructure Class
    #if (!OPCUA_EXCLUDE_DerivedStructure)
    /// <summary>
    /// A description for the DerivedStructure DataType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = DataTypeTest.Namespaces.cas)]
    public partial class DerivedStructure : AbstractStructure
    {
        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public DerivedStructure()
        {
            Initialize();
        }

        /// <summary>
        /// Called by the .NET framework during deserialization.
        /// </summary>
        [OnDeserializing]
        private void Initialize(StreamingContext context)
        {
            Initialize();
        }

        /// <summary>
        /// Sets private members to default values.
        /// </summary>
        private void Initialize()
        {
            m_number = (int)0;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Derived from Number
        /// </summary>
        [DataMember(Name = "Number", IsRequired = false, Order = 1)]
        public int Number
        {
            get { return m_number;  }
            set { m_number = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public override ExpandedNodeId TypeId
        {
            get { return DataTypeIds.DerivedStructure; }
        }

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public override ExpandedNodeId BinaryEncodingId
        {
            get { return ObjectIds.DerivedStructure_Encoding_DefaultBinary; }
        }

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public override ExpandedNodeId XmlEncodingId
        {
            get { return ObjectIds.DerivedStructure_Encoding_DefaultXml; }
        }

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public override void Encode(IEncoder encoder)
        {
            base.Encode(encoder);

            encoder.PushNamespace(DataTypeTest.Namespaces.cas);

            encoder.WriteInt32("Number", Number);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public override void Decode(IDecoder decoder)
        {
            base.Decode(decoder);

            decoder.PushNamespace(DataTypeTest.Namespaces.cas);

            Number = decoder.ReadInt32("Number");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public override bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            DerivedStructure value = encodeable as DerivedStructure;

            if (value == null)
            {
                return false;
            }

            if (!base.IsEqual(encodeable)) return false;
            if (!Utils.IsEqual(m_number, value.m_number)) return false;

            return true;
        }    

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public override object Clone()
        {
            return (DerivedStructure)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            DerivedStructure clone = (DerivedStructure)base.MemberwiseClone();

            clone.m_number = (int)Utils.Clone(this.m_number);

            return clone;
        }
        #endregion

        #region Private Fields
        private int m_number;
        #endregion
    }

    #region DerivedStructureCollection Class
    /// <summary>
    /// A collection of DerivedStructure objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfDerivedStructure", Namespace = DataTypeTest.Namespaces.cas, ItemName = "DerivedStructure")]
    #if !NET_STANDARD
    public partial class DerivedStructureCollection : List<DerivedStructure>, ICloneable
    #else
    public partial class DerivedStructureCollection : List<DerivedStructure>
    #endif
    {
        #region Constructors
        /// <summary>
        /// Initializes the collection with default values.
        /// </summary>
        public DerivedStructureCollection() {}

        /// <summary>
        /// Initializes the collection with an initial capacity.
        /// </summary>
        public DerivedStructureCollection(int capacity) : base(capacity) {}

        /// <summary>
        /// Initializes the collection with another collection.
        /// </summary>
        public DerivedStructureCollection(IEnumerable<DerivedStructure> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator DerivedStructureCollection(DerivedStructure[] values)
        {
            if (values != null)
            {
                return new DerivedStructureCollection(values);
            }

            return new DerivedStructureCollection();
        }

        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator DerivedStructure[](DerivedStructureCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <summary>
        /// Creates a deep copy of the collection.
        /// </summary>
        public object Clone()
        {
            return (DerivedStructureCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            DerivedStructureCollection clone = new DerivedStructureCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((DerivedStructure)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion
}