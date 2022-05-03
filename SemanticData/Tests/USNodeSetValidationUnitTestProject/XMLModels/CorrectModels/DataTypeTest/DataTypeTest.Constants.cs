/* ========================================================================
 * Copyright (c) 2005-2021 The OPC Foundation, Inc. All rights reserved.
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
using System.Reflection;
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
        /// The identifier for the AbstractStructure_Encoding_DefaultBinary Object.
        /// </summary>
        public const uint AbstractStructure_Encoding_DefaultBinary = 18;

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultBinary Object.
        /// </summary>
        public const uint DerivedStructure_Encoding_DefaultBinary = 27;

        /// <summary>
        /// The identifier for the AbstractStructure_Encoding_DefaultXml Object.
        /// </summary>
        public const uint AbstractStructure_Encoding_DefaultXml = 14;

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultXml Object.
        /// </summary>
        public const uint DerivedStructure_Encoding_DefaultXml = 23;

        /// <summary>
        /// The identifier for the AbstractStructure_Encoding_DefaultJson Object.
        /// </summary>
        public const uint AbstractStructure_Encoding_DefaultJson = 31;

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultJson Object.
        /// </summary>
        public const uint DerivedStructure_Encoding_DefaultJson = 32;
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
        /// The identifier for the cas_BinarySchema Variable.
        /// </summary>
        public const uint cas_BinarySchema = 5;

        /// <summary>
        /// The identifier for the cas_BinarySchema_NamespaceUri Variable.
        /// </summary>
        public const uint cas_BinarySchema_NamespaceUri = 7;

        /// <summary>
        /// The identifier for the cas_BinarySchema_Deprecated Variable.
        /// </summary>
        public const uint cas_BinarySchema_Deprecated = 1;

        /// <summary>
        /// The identifier for the cas_BinarySchema_AbstractStructure Variable.
        /// </summary>
        public const uint cas_BinarySchema_AbstractStructure = 19;

        /// <summary>
        /// The identifier for the cas_BinarySchema_DerivedStructure Variable.
        /// </summary>
        public const uint cas_BinarySchema_DerivedStructure = 28;

        /// <summary>
        /// The identifier for the cas_XmlSchema Variable.
        /// </summary>
        public const uint cas_XmlSchema = 2;

        /// <summary>
        /// The identifier for the cas_XmlSchema_NamespaceUri Variable.
        /// </summary>
        public const uint cas_XmlSchema_NamespaceUri = 4;

        /// <summary>
        /// The identifier for the cas_XmlSchema_Deprecated Variable.
        /// </summary>
        public const uint cas_XmlSchema_Deprecated = 8;

        /// <summary>
        /// The identifier for the cas_XmlSchema_AbstractStructure Variable.
        /// </summary>
        public const uint cas_XmlSchema_AbstractStructure = 15;

        /// <summary>
        /// The identifier for the cas_XmlSchema_DerivedStructure Variable.
        /// </summary>
        public const uint cas_XmlSchema_DerivedStructure = 24;
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
        /// The identifier for the AbstractStructure_Encoding_DefaultBinary Object.
        /// </summary>
        public static readonly ExpandedNodeId AbstractStructure_Encoding_DefaultBinary = new ExpandedNodeId(DataTypeTest.Objects.AbstractStructure_Encoding_DefaultBinary, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultBinary Object.
        /// </summary>
        public static readonly ExpandedNodeId DerivedStructure_Encoding_DefaultBinary = new ExpandedNodeId(DataTypeTest.Objects.DerivedStructure_Encoding_DefaultBinary, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the AbstractStructure_Encoding_DefaultXml Object.
        /// </summary>
        public static readonly ExpandedNodeId AbstractStructure_Encoding_DefaultXml = new ExpandedNodeId(DataTypeTest.Objects.AbstractStructure_Encoding_DefaultXml, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultXml Object.
        /// </summary>
        public static readonly ExpandedNodeId DerivedStructure_Encoding_DefaultXml = new ExpandedNodeId(DataTypeTest.Objects.DerivedStructure_Encoding_DefaultXml, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the AbstractStructure_Encoding_DefaultJson Object.
        /// </summary>
        public static readonly ExpandedNodeId AbstractStructure_Encoding_DefaultJson = new ExpandedNodeId(DataTypeTest.Objects.AbstractStructure_Encoding_DefaultJson, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedStructure_Encoding_DefaultJson Object.
        /// </summary>
        public static readonly ExpandedNodeId DerivedStructure_Encoding_DefaultJson = new ExpandedNodeId(DataTypeTest.Objects.DerivedStructure_Encoding_DefaultJson, DataTypeTest.Namespaces.cas);
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
        /// The identifier for the cas_BinarySchema Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema = new ExpandedNodeId(DataTypeTest.Variables.cas_BinarySchema, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema_NamespaceUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema_NamespaceUri = new ExpandedNodeId(DataTypeTest.Variables.cas_BinarySchema_NamespaceUri, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema_Deprecated Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema_Deprecated = new ExpandedNodeId(DataTypeTest.Variables.cas_BinarySchema_Deprecated, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema_AbstractStructure Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema_AbstractStructure = new ExpandedNodeId(DataTypeTest.Variables.cas_BinarySchema_AbstractStructure, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema_DerivedStructure Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema_DerivedStructure = new ExpandedNodeId(DataTypeTest.Variables.cas_BinarySchema_DerivedStructure, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema = new ExpandedNodeId(DataTypeTest.Variables.cas_XmlSchema, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema_NamespaceUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema_NamespaceUri = new ExpandedNodeId(DataTypeTest.Variables.cas_XmlSchema_NamespaceUri, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema_Deprecated Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema_Deprecated = new ExpandedNodeId(DataTypeTest.Variables.cas_XmlSchema_Deprecated, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema_AbstractStructure Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema_AbstractStructure = new ExpandedNodeId(DataTypeTest.Variables.cas_XmlSchema_AbstractStructure, DataTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema_DerivedStructure Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema_DerivedStructure = new ExpandedNodeId(DataTypeTest.Variables.cas_XmlSchema_DerivedStructure, DataTypeTest.Namespaces.cas);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
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
}