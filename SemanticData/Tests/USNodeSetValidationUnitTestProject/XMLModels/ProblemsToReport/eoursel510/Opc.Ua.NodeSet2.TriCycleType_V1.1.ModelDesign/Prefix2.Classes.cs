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
using Prefix0;

namespace Prefix2
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
        /// The identifier for the TireEnum DataType.
        /// </summary>
        public const uint TireEnum = 1;

        /// <summary>
        /// The identifier for the TriCycleDataType DataType.
        /// </summary>
        public const uint TriCycleDataType = 3;

        /// <summary>
        /// The identifier for the WheelDataType DataType.
        /// </summary>
        public const uint WheelDataType = 4;
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
        /// The identifier for the VehicleType_S_Owner_ Object.
        /// </summary>
        public const uint VehicleType_S_Owner_ = 47;

        /// <summary>
        /// The identifier for the TrailerType_S_Owner_ Object.
        /// </summary>
        public const uint TrailerType_S_Owner_ = 52;

        /// <summary>
        /// The identifier for the TriCycleType_S_Owner_ Object.
        /// </summary>
        public const uint TriCycleType_S_Owner_ = 57;

        /// <summary>
        /// The identifier for the TriCycleDataType_Encoding_DefaultXml Object.
        /// </summary>
        public const uint TriCycleDataType_Encoding_DefaultXml = 24;

        /// <summary>
        /// The identifier for the WheelDataType_Encoding_DefaultXml Object.
        /// </summary>
        public const uint WheelDataType_Encoding_DefaultXml = 25;

        /// <summary>
        /// The identifier for the TriCycleDataType_Encoding_DefaultBinary Object.
        /// </summary>
        public const uint TriCycleDataType_Encoding_DefaultBinary = 35;

        /// <summary>
        /// The identifier for the WheelDataType_Encoding_DefaultBinary Object.
        /// </summary>
        public const uint WheelDataType_Encoding_DefaultBinary = 36;
    }
    #endregion

    #region ObjectType Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypes
    {
        /// <summary>
        /// The identifier for the HumanType ObjectType.
        /// </summary>
        public const uint HumanType = 5;

        /// <summary>
        /// The identifier for the VehicleType ObjectType.
        /// </summary>
        public const uint VehicleType = 10;

        /// <summary>
        /// The identifier for the TrailerType ObjectType.
        /// </summary>
        public const uint TrailerType = 9;

        /// <summary>
        /// The identifier for the TriCycleType ObjectType.
        /// </summary>
        public const uint TriCycleType = 11;
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
        /// The identifier for the TireEnum_EnumStrings Variable.
        /// </summary>
        public const uint TireEnum_EnumStrings = 2;

        /// <summary>
        /// The identifier for the HumanType_Name Variable.
        /// </summary>
        public const uint HumanType_Name = 6;

        /// <summary>
        /// The identifier for the HumanType_Age Variable.
        /// </summary>
        public const uint HumanType_Age = 7;

        /// <summary>
        /// The identifier for the HumanType_Gender Variable.
        /// </summary>
        public const uint HumanType_Gender = 8;

        /// <summary>
        /// The identifier for the VehicleType_buildDate Variable.
        /// </summary>
        public const uint VehicleType_buildDate = 46;

        /// <summary>
        /// The identifier for the VehicleType_S_Owner__Name Variable.
        /// </summary>
        public const uint VehicleType_S_Owner__Name = 48;

        /// <summary>
        /// The identifier for the VehicleType_S_Owner__Age Variable.
        /// </summary>
        public const uint VehicleType_S_Owner__Age = 49;

        /// <summary>
        /// The identifier for the VehicleType_S_Owner__Gender Variable.
        /// </summary>
        public const uint VehicleType_S_Owner__Gender = 50;

        /// <summary>
        /// The identifier for the TrailerType_S_Owner__Name Variable.
        /// </summary>
        public const uint TrailerType_S_Owner__Name = 53;

        /// <summary>
        /// The identifier for the TrailerType_S_Owner__Age Variable.
        /// </summary>
        public const uint TrailerType_S_Owner__Age = 54;

        /// <summary>
        /// The identifier for the TrailerType_S_Owner__Gender Variable.
        /// </summary>
        public const uint TrailerType_S_Owner__Gender = 55;

        /// <summary>
        /// The identifier for the TriCycleType_S_Owner__Name Variable.
        /// </summary>
        public const uint TriCycleType_S_Owner__Name = 58;

        /// <summary>
        /// The identifier for the TriCycleType_S_Owner__Age Variable.
        /// </summary>
        public const uint TriCycleType_S_Owner__Age = 59;

        /// <summary>
        /// The identifier for the TriCycleType_S_Owner__Gender Variable.
        /// </summary>
        public const uint TriCycleType_S_Owner__Gender = 60;

        /// <summary>
        /// The identifier for the TriCycleType_weight Variable.
        /// </summary>
        public const uint TriCycleType_weight = 12;

        /// <summary>
        /// The identifier for the TriCycleType_wheels Variable.
        /// </summary>
        public const uint TriCycleType_wheels = 13;

        /// <summary>
        /// The identifier for the TriCycleType_wheels_tickness Variable.
        /// </summary>
        public const uint TriCycleType_wheels_tickness = 14;

        /// <summary>
        /// The identifier for the TriCycleType_wheels_diameter Variable.
        /// </summary>
        public const uint TriCycleType_wheels_diameter = 15;

        /// <summary>
        /// The identifier for the TriCycleType_wheels_pressure Variable.
        /// </summary>
        public const uint TriCycleType_wheels_pressure = 16;

        /// <summary>
        /// The identifier for the TriCycleType_wheels_tiretype Variable.
        /// </summary>
        public const uint TriCycleType_wheels_tiretype = 17;

        /// <summary>
        /// The identifier for the TriCycleType_Model Variable.
        /// </summary>
        public const uint TriCycleType_Model = 18;

        /// <summary>
        /// The identifier for the WheelVariableType_tickness Variable.
        /// </summary>
        public const uint WheelVariableType_tickness = 20;

        /// <summary>
        /// The identifier for the WheelVariableType_diameter Variable.
        /// </summary>
        public const uint WheelVariableType_diameter = 21;

        /// <summary>
        /// The identifier for the WheelVariableType_pressure Variable.
        /// </summary>
        public const uint WheelVariableType_pressure = 22;

        /// <summary>
        /// The identifier for the WheelVariableType_tiretype Variable.
        /// </summary>
        public const uint WheelVariableType_tiretype = 23;

        /// <summary>
        /// The identifier for the Name2_XmlSchema Variable.
        /// </summary>
        public const uint Name2_XmlSchema = 26;

        /// <summary>
        /// The identifier for the Name2_XmlSchema_NamespaceUri Variable.
        /// </summary>
        public const uint Name2_XmlSchema_NamespaceUri = 28;

        /// <summary>
        /// The identifier for the Name2_XmlSchema_TriCycleDataType Variable.
        /// </summary>
        public const uint Name2_XmlSchema_TriCycleDataType = 29;

        /// <summary>
        /// The identifier for the Name2_XmlSchema_WheelDataType Variable.
        /// </summary>
        public const uint Name2_XmlSchema_WheelDataType = 32;

        /// <summary>
        /// The identifier for the Name2_BinarySchema Variable.
        /// </summary>
        public const uint Name2_BinarySchema = 37;

        /// <summary>
        /// The identifier for the Name2_BinarySchema_NamespaceUri Variable.
        /// </summary>
        public const uint Name2_BinarySchema_NamespaceUri = 39;

        /// <summary>
        /// The identifier for the Name2_BinarySchema_TriCycleDataType Variable.
        /// </summary>
        public const uint Name2_BinarySchema_TriCycleDataType = 40;

        /// <summary>
        /// The identifier for the Name2_BinarySchema_WheelDataType Variable.
        /// </summary>
        public const uint Name2_BinarySchema_WheelDataType = 43;
    }
    #endregion

    #region VariableType Identifiers
    /// <summary>
    /// A class that declares constants for all VariableTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableTypes
    {
        /// <summary>
        /// The identifier for the WheelVariableType VariableType.
        /// </summary>
        public const uint WheelVariableType = 19;
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
        /// The identifier for the TireEnum DataType.
        /// </summary>
        public static readonly ExpandedNodeId TireEnum = new ExpandedNodeId(Prefix2.DataTypes.TireEnum, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleDataType DataType.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleDataType = new ExpandedNodeId(Prefix2.DataTypes.TriCycleDataType, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the WheelDataType DataType.
        /// </summary>
        public static readonly ExpandedNodeId WheelDataType = new ExpandedNodeId(Prefix2.DataTypes.WheelDataType, Prefix2.Namespaces.Name2);
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
        /// The identifier for the VehicleType_S_Owner_ Object.
        /// </summary>
        public static readonly ExpandedNodeId VehicleType_S_Owner_ = new ExpandedNodeId(Prefix2.Objects.VehicleType_S_Owner_, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TrailerType_S_Owner_ Object.
        /// </summary>
        public static readonly ExpandedNodeId TrailerType_S_Owner_ = new ExpandedNodeId(Prefix2.Objects.TrailerType_S_Owner_, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_S_Owner_ Object.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_S_Owner_ = new ExpandedNodeId(Prefix2.Objects.TriCycleType_S_Owner_, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleDataType_Encoding_DefaultXml Object.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleDataType_Encoding_DefaultXml = new ExpandedNodeId(Prefix2.Objects.TriCycleDataType_Encoding_DefaultXml, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the WheelDataType_Encoding_DefaultXml Object.
        /// </summary>
        public static readonly ExpandedNodeId WheelDataType_Encoding_DefaultXml = new ExpandedNodeId(Prefix2.Objects.WheelDataType_Encoding_DefaultXml, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleDataType_Encoding_DefaultBinary Object.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleDataType_Encoding_DefaultBinary = new ExpandedNodeId(Prefix2.Objects.TriCycleDataType_Encoding_DefaultBinary, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the WheelDataType_Encoding_DefaultBinary Object.
        /// </summary>
        public static readonly ExpandedNodeId WheelDataType_Encoding_DefaultBinary = new ExpandedNodeId(Prefix2.Objects.WheelDataType_Encoding_DefaultBinary, Prefix2.Namespaces.Name2);
    }
    #endregion

    #region ObjectType Node Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypeIds
    {
        /// <summary>
        /// The identifier for the HumanType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId HumanType = new ExpandedNodeId(Prefix2.ObjectTypes.HumanType, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the VehicleType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId VehicleType = new ExpandedNodeId(Prefix2.ObjectTypes.VehicleType, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TrailerType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId TrailerType = new ExpandedNodeId(Prefix2.ObjectTypes.TrailerType, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType = new ExpandedNodeId(Prefix2.ObjectTypes.TriCycleType, Prefix2.Namespaces.Name2);
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
        /// The identifier for the TireEnum_EnumStrings Variable.
        /// </summary>
        public static readonly ExpandedNodeId TireEnum_EnumStrings = new ExpandedNodeId(Prefix2.Variables.TireEnum_EnumStrings, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the HumanType_Name Variable.
        /// </summary>
        public static readonly ExpandedNodeId HumanType_Name = new ExpandedNodeId(Prefix2.Variables.HumanType_Name, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the HumanType_Age Variable.
        /// </summary>
        public static readonly ExpandedNodeId HumanType_Age = new ExpandedNodeId(Prefix2.Variables.HumanType_Age, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the HumanType_Gender Variable.
        /// </summary>
        public static readonly ExpandedNodeId HumanType_Gender = new ExpandedNodeId(Prefix2.Variables.HumanType_Gender, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the VehicleType_buildDate Variable.
        /// </summary>
        public static readonly ExpandedNodeId VehicleType_buildDate = new ExpandedNodeId(Prefix2.Variables.VehicleType_buildDate, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the VehicleType_S_Owner__Name Variable.
        /// </summary>
        public static readonly ExpandedNodeId VehicleType_S_Owner__Name = new ExpandedNodeId(Prefix2.Variables.VehicleType_S_Owner__Name, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the VehicleType_S_Owner__Age Variable.
        /// </summary>
        public static readonly ExpandedNodeId VehicleType_S_Owner__Age = new ExpandedNodeId(Prefix2.Variables.VehicleType_S_Owner__Age, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the VehicleType_S_Owner__Gender Variable.
        /// </summary>
        public static readonly ExpandedNodeId VehicleType_S_Owner__Gender = new ExpandedNodeId(Prefix2.Variables.VehicleType_S_Owner__Gender, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TrailerType_S_Owner__Name Variable.
        /// </summary>
        public static readonly ExpandedNodeId TrailerType_S_Owner__Name = new ExpandedNodeId(Prefix2.Variables.TrailerType_S_Owner__Name, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TrailerType_S_Owner__Age Variable.
        /// </summary>
        public static readonly ExpandedNodeId TrailerType_S_Owner__Age = new ExpandedNodeId(Prefix2.Variables.TrailerType_S_Owner__Age, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TrailerType_S_Owner__Gender Variable.
        /// </summary>
        public static readonly ExpandedNodeId TrailerType_S_Owner__Gender = new ExpandedNodeId(Prefix2.Variables.TrailerType_S_Owner__Gender, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_S_Owner__Name Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_S_Owner__Name = new ExpandedNodeId(Prefix2.Variables.TriCycleType_S_Owner__Name, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_S_Owner__Age Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_S_Owner__Age = new ExpandedNodeId(Prefix2.Variables.TriCycleType_S_Owner__Age, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_S_Owner__Gender Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_S_Owner__Gender = new ExpandedNodeId(Prefix2.Variables.TriCycleType_S_Owner__Gender, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_weight Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_weight = new ExpandedNodeId(Prefix2.Variables.TriCycleType_weight, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_wheels Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_wheels = new ExpandedNodeId(Prefix2.Variables.TriCycleType_wheels, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_wheels_tickness Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_wheels_tickness = new ExpandedNodeId(Prefix2.Variables.TriCycleType_wheels_tickness, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_wheels_diameter Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_wheels_diameter = new ExpandedNodeId(Prefix2.Variables.TriCycleType_wheels_diameter, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_wheels_pressure Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_wheels_pressure = new ExpandedNodeId(Prefix2.Variables.TriCycleType_wheels_pressure, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_wheels_tiretype Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_wheels_tiretype = new ExpandedNodeId(Prefix2.Variables.TriCycleType_wheels_tiretype, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the TriCycleType_Model Variable.
        /// </summary>
        public static readonly ExpandedNodeId TriCycleType_Model = new ExpandedNodeId(Prefix2.Variables.TriCycleType_Model, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the WheelVariableType_tickness Variable.
        /// </summary>
        public static readonly ExpandedNodeId WheelVariableType_tickness = new ExpandedNodeId(Prefix2.Variables.WheelVariableType_tickness, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the WheelVariableType_diameter Variable.
        /// </summary>
        public static readonly ExpandedNodeId WheelVariableType_diameter = new ExpandedNodeId(Prefix2.Variables.WheelVariableType_diameter, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the WheelVariableType_pressure Variable.
        /// </summary>
        public static readonly ExpandedNodeId WheelVariableType_pressure = new ExpandedNodeId(Prefix2.Variables.WheelVariableType_pressure, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the WheelVariableType_tiretype Variable.
        /// </summary>
        public static readonly ExpandedNodeId WheelVariableType_tiretype = new ExpandedNodeId(Prefix2.Variables.WheelVariableType_tiretype, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the Name2_XmlSchema Variable.
        /// </summary>
        public static readonly ExpandedNodeId Name2_XmlSchema = new ExpandedNodeId(Prefix2.Variables.Name2_XmlSchema, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the Name2_XmlSchema_NamespaceUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId Name2_XmlSchema_NamespaceUri = new ExpandedNodeId(Prefix2.Variables.Name2_XmlSchema_NamespaceUri, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the Name2_XmlSchema_TriCycleDataType Variable.
        /// </summary>
        public static readonly ExpandedNodeId Name2_XmlSchema_TriCycleDataType = new ExpandedNodeId(Prefix2.Variables.Name2_XmlSchema_TriCycleDataType, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the Name2_XmlSchema_WheelDataType Variable.
        /// </summary>
        public static readonly ExpandedNodeId Name2_XmlSchema_WheelDataType = new ExpandedNodeId(Prefix2.Variables.Name2_XmlSchema_WheelDataType, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the Name2_BinarySchema Variable.
        /// </summary>
        public static readonly ExpandedNodeId Name2_BinarySchema = new ExpandedNodeId(Prefix2.Variables.Name2_BinarySchema, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the Name2_BinarySchema_NamespaceUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId Name2_BinarySchema_NamespaceUri = new ExpandedNodeId(Prefix2.Variables.Name2_BinarySchema_NamespaceUri, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the Name2_BinarySchema_TriCycleDataType Variable.
        /// </summary>
        public static readonly ExpandedNodeId Name2_BinarySchema_TriCycleDataType = new ExpandedNodeId(Prefix2.Variables.Name2_BinarySchema_TriCycleDataType, Prefix2.Namespaces.Name2);

        /// <summary>
        /// The identifier for the Name2_BinarySchema_WheelDataType Variable.
        /// </summary>
        public static readonly ExpandedNodeId Name2_BinarySchema_WheelDataType = new ExpandedNodeId(Prefix2.Variables.Name2_BinarySchema_WheelDataType, Prefix2.Namespaces.Name2);
    }
    #endregion

    #region VariableType Node Identifiers
    /// <summary>
    /// A class that declares constants for all VariableTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableTypeIds
    {
        /// <summary>
        /// The identifier for the WheelVariableType VariableType.
        /// </summary>
        public static readonly ExpandedNodeId WheelVariableType = new ExpandedNodeId(Prefix2.VariableTypes.WheelVariableType, Prefix2.Namespaces.Name2);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the HumanType component.
        /// </summary>
        public const string HumanType = "HumanType";

        /// <summary>
        /// The BrowseName for the Name2_BinarySchema component.
        /// </summary>
        public const string Name2_BinarySchema = "Prefix2";

        /// <summary>
        /// The BrowseName for the Name2_XmlSchema component.
        /// </summary>
        public const string Name2_XmlSchema = "Prefix2";

        /// <summary>
        /// The BrowseName for the TireEnum component.
        /// </summary>
        public const string TireEnum = "TireEnum";

        /// <summary>
        /// The BrowseName for the TrailerType component.
        /// </summary>
        public const string TrailerType = "TrailerType";

        /// <summary>
        /// The BrowseName for the TriCycleDataType component.
        /// </summary>
        public const string TriCycleDataType = "TriCycleDataType";

        /// <summary>
        /// The BrowseName for the TriCycleType component.
        /// </summary>
        public const string TriCycleType = "TriCycleType";

        /// <summary>
        /// The BrowseName for the VehicleType component.
        /// </summary>
        public const string VehicleType = "VehicleType";

        /// <summary>
        /// The BrowseName for the WheelDataType component.
        /// </summary>
        public const string WheelDataType = "WheelDataType";

        /// <summary>
        /// The BrowseName for the WheelVariableType component.
        /// </summary>
        public const string WheelVariableType = "WheelVariableType";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the Name0Xsd namespace (.NET code namespace is 'Prefix0').
        /// </summary>
        public const string Name0Xsd = "http://opcfoundation.org/UA/";

        /// <summary>
        /// The URI for the Name0Xsd namespace (.NET code namespace is 'Prefix0').
        /// </summary>
        public const string Name0Xsd = "http://opcfoundation.org/UA/";

        /// <summary>
        /// The URI for the Name2Xsd namespace (.NET code namespace is 'Prefix2').
        /// </summary>
        public const string Name2Xsd = "http://tricycletypev1/";

        /// <summary>
        /// The URI for the Name2Xsd namespace (.NET code namespace is 'Prefix2').
        /// </summary>
        public const string Name2Xsd = "http://tricycletypev1/";
    }
    #endregion

    #region TireEnum Enumeration
    #if (!OPCUA_EXCLUDE_TireEnum)
    /// <summary>
    /// A description for the TireEnum DataType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = Prefix2.Namespaces.Name2Xsd)]
    public enum TireEnum
    {
        /// <summary>
        /// A description for the Mud field.
        /// </summary>
        [EnumMember(Value = "Mud_0")]
        Mud = 0,

        /// <summary>
        /// A description for the Ice field.
        /// </summary>
        [EnumMember(Value = "Ice_1")]
        Ice = 1,

        /// <summary>
        /// A description for the Sand field.
        /// </summary>
        [EnumMember(Value = "Sand_2")]
        Sand = 2,
    }

    #region TireEnumCollection Class
    /// <summary>
    /// A collection of TireEnum objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfTireEnum", Namespace = Prefix2.Namespaces.Name2Xsd, ItemName = "TireEnum")]
    #if !NET_STANDARD
    public partial class TireEnumCollection : List<TireEnum>, ICloneable
    #else
    public partial class TireEnumCollection : List<TireEnum>
    #endif
    {
        #region Constructors
        /// <summary>
        /// Initializes the collection with default values.
        /// </summary>
        public TireEnumCollection() {}

        /// <summary>
        /// Initializes the collection with an initial capacity.
        /// </summary>
        public TireEnumCollection(int capacity) : base(capacity) {}

        /// <summary>
        /// Initializes the collection with another collection.
        /// </summary>
        public TireEnumCollection(IEnumerable<TireEnum> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator TireEnumCollection(TireEnum[] values)
        {
            if (values != null)
            {
                return new TireEnumCollection(values);
            }

            return new TireEnumCollection();
        }

        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator TireEnum[](TireEnumCollection values)
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
            return (TireEnumCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            TireEnumCollection clone = new TireEnumCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((TireEnum)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region TriCycleDataType Class
    #if (!OPCUA_EXCLUDE_TriCycleDataType)
    /// <summary>
    /// A description for the TriCycleDataType DataType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = Prefix2.Namespaces.Name2Xsd)]
    public partial class TriCycleDataType : IEncodeable
    {
        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public TriCycleDataType()
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
            m_weight = (float)0;
            m_wheel = new WheelDataType();
            m_model = null;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the weight field.
        /// </summary>
        [DataMember(Name = "weight", IsRequired = false, Order = 1)]
        public float weight
        {
            get { return m_weight;  }
            set { m_weight = value; }
        }

        /// <summary>
        /// A description for the wheel field.
        /// </summary>
        [DataMember(Name = "wheel", IsRequired = false, Order = 2)]
        public WheelDataType wheel
        {
            get
            {
                return m_wheel;
            }

            set
            {
                m_wheel = value;

                if (value == null)
                {
                    m_wheel = new WheelDataType();
                }
            }
        }

        /// <summary>
        /// A description for the Model field.
        /// </summary>
        [DataMember(Name = "Model", IsRequired = false, Order = 3)]
        public string Model
        {
            get { return m_model;  }
            set { m_model = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId
        {
            get { return DataTypeIds.TriCycleDataType; }
        }

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId
        {
            get { return ObjectIds.TriCycleDataType_Encoding_DefaultBinary; }
        }

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId
        {
            get { return ObjectIds.TriCycleDataType_Encoding_DefaultXml; }
        }

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(Prefix2.Namespaces.Name2Xsd);

            encoder.WriteFloat("weight", weight);
            encoder.WriteEncodeable("wheel", wheel, typeof(WheelDataType));
            encoder.WriteString("Model", Model);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(Prefix2.Namespaces.Name2Xsd);

            weight = decoder.ReadFloat("weight");
            wheel = (WheelDataType)decoder.ReadEncodeable("wheel", typeof(WheelDataType));
            Model = decoder.ReadString("Model");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            TriCycleDataType value = encodeable as TriCycleDataType;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_weight, value.m_weight)) return false;
            if (!Utils.IsEqual(m_wheel, value.m_wheel)) return false;
            if (!Utils.IsEqual(m_model, value.m_model)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (TriCycleDataType)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            TriCycleDataType clone = (TriCycleDataType)base.MemberwiseClone();

            clone.m_weight = (float)Utils.Clone(this.m_weight);
            clone.m_wheel = (WheelDataType)Utils.Clone(this.m_wheel);
            clone.m_model = (string)Utils.Clone(this.m_model);

            return clone;
        }
        #endregion

        #region Private Fields
        private float m_weight;
        private WheelDataType m_wheel;
        private string m_model;
        #endregion
    }

    #region TriCycleDataTypeCollection Class
    /// <summary>
    /// A collection of TriCycleDataType objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfTriCycleDataType", Namespace = Prefix2.Namespaces.Name2Xsd, ItemName = "TriCycleDataType")]
    #if !NET_STANDARD
    public partial class TriCycleDataTypeCollection : List<TriCycleDataType>, ICloneable
    #else
    public partial class TriCycleDataTypeCollection : List<TriCycleDataType>
    #endif
    {
        #region Constructors
        /// <summary>
        /// Initializes the collection with default values.
        /// </summary>
        public TriCycleDataTypeCollection() {}

        /// <summary>
        /// Initializes the collection with an initial capacity.
        /// </summary>
        public TriCycleDataTypeCollection(int capacity) : base(capacity) {}

        /// <summary>
        /// Initializes the collection with another collection.
        /// </summary>
        public TriCycleDataTypeCollection(IEnumerable<TriCycleDataType> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator TriCycleDataTypeCollection(TriCycleDataType[] values)
        {
            if (values != null)
            {
                return new TriCycleDataTypeCollection(values);
            }

            return new TriCycleDataTypeCollection();
        }

        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator TriCycleDataType[](TriCycleDataTypeCollection values)
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
            return (TriCycleDataTypeCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            TriCycleDataTypeCollection clone = new TriCycleDataTypeCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((TriCycleDataType)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region WheelDataType Class
    #if (!OPCUA_EXCLUDE_WheelDataType)
    /// <summary>
    /// Wheel datatype.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = Prefix2.Namespaces.Name2Xsd)]
    public partial class WheelDataType : IEncodeable
    {
        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public WheelDataType()
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
            m_tickness = (float)0;
            m_diameter = (float)0;
            m_pressure = (float)0;
            m_tireType = TireEnum.Mud;
            m_raysLen = (int)0;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the tickness field.
        /// </summary>
        [DataMember(Name = "tickness", IsRequired = false, Order = 1)]
        public float tickness
        {
            get { return m_tickness;  }
            set { m_tickness = value; }
        }

        /// <summary>
        /// A description for the diameter field.
        /// </summary>
        [DataMember(Name = "diameter", IsRequired = false, Order = 2)]
        public float diameter
        {
            get { return m_diameter;  }
            set { m_diameter = value; }
        }

        /// <summary>
        /// A description for the pressure field.
        /// </summary>
        [DataMember(Name = "pressure", IsRequired = false, Order = 3)]
        public float pressure
        {
            get { return m_pressure;  }
            set { m_pressure = value; }
        }

        /// <summary>
        /// A description for the TireType field.
        /// </summary>
        [DataMember(Name = "TireType", IsRequired = false, Order = 4)]
        public TireEnum TireType
        {
            get { return m_tireType;  }
            set { m_tireType = value; }
        }

        /// <summary>
        /// A description for the raysLen field.
        /// </summary>
        [DataMember(Name = "raysLen", IsRequired = false, Order = 5)]
        public int raysLen
        {
            get { return m_raysLen;  }
            set { m_raysLen = value; }
        }
        #endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId
        {
            get { return DataTypeIds.WheelDataType; }
        }

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId
        {
            get { return ObjectIds.WheelDataType_Encoding_DefaultBinary; }
        }

        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId
        {
            get { return ObjectIds.WheelDataType_Encoding_DefaultXml; }
        }

        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(Prefix2.Namespaces.Name2Xsd);

            encoder.WriteFloat("tickness", tickness);
            encoder.WriteFloat("diameter", diameter);
            encoder.WriteFloat("pressure", pressure);
            encoder.WriteEnumerated("TireType", TireType);
            encoder.WriteInt32("raysLen", raysLen);

            encoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(Prefix2.Namespaces.Name2Xsd);

            tickness = decoder.ReadFloat("tickness");
            diameter = decoder.ReadFloat("diameter");
            pressure = decoder.ReadFloat("pressure");
            TireType = (TireEnum)decoder.ReadEnumerated("TireType", typeof(TireEnum));
            raysLen = decoder.ReadInt32("raysLen");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }

            WheelDataType value = encodeable as WheelDataType;

            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_tickness, value.m_tickness)) return false;
            if (!Utils.IsEqual(m_diameter, value.m_diameter)) return false;
            if (!Utils.IsEqual(m_pressure, value.m_pressure)) return false;
            if (!Utils.IsEqual(m_tireType, value.m_tireType)) return false;
            if (!Utils.IsEqual(m_raysLen, value.m_raysLen)) return false;

            return true;
        }

        #if !NET_STANDARD
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            return (WheelDataType)this.MemberwiseClone();
        }
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            WheelDataType clone = (WheelDataType)base.MemberwiseClone();

            clone.m_tickness = (float)Utils.Clone(this.m_tickness);
            clone.m_diameter = (float)Utils.Clone(this.m_diameter);
            clone.m_pressure = (float)Utils.Clone(this.m_pressure);
            clone.m_tireType = (TireEnum)Utils.Clone(this.m_tireType);
            clone.m_raysLen = (int)Utils.Clone(this.m_raysLen);

            return clone;
        }
        #endregion

        #region Private Fields
        private float m_tickness;
        private float m_diameter;
        private float m_pressure;
        private TireEnum m_tireType;
        private int m_raysLen;
        #endregion
    }

    #region WheelDataTypeCollection Class
    /// <summary>
    /// A collection of WheelDataType objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfWheelDataType", Namespace = Prefix2.Namespaces.Name2Xsd, ItemName = "WheelDataType")]
    #if !NET_STANDARD
    public partial class WheelDataTypeCollection : List<WheelDataType>, ICloneable
    #else
    public partial class WheelDataTypeCollection : List<WheelDataType>
    #endif
    {
        #region Constructors
        /// <summary>
        /// Initializes the collection with default values.
        /// </summary>
        public WheelDataTypeCollection() {}

        /// <summary>
        /// Initializes the collection with an initial capacity.
        /// </summary>
        public WheelDataTypeCollection(int capacity) : base(capacity) {}

        /// <summary>
        /// Initializes the collection with another collection.
        /// </summary>
        public WheelDataTypeCollection(IEnumerable<WheelDataType> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator WheelDataTypeCollection(WheelDataType[] values)
        {
            if (values != null)
            {
                return new WheelDataTypeCollection(values);
            }

            return new WheelDataTypeCollection();
        }

        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator WheelDataType[](WheelDataTypeCollection values)
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
            return (WheelDataTypeCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            WheelDataTypeCollection clone = new WheelDataTypeCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((WheelDataType)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region HumanState Class
    #if (!OPCUA_EXCLUDE_HumanState)
    /// <summary>
    /// Stores an instance of the HumanType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class HumanState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public HumanState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Prefix2.ObjectTypes.HumanType, Prefix2.Namespaces.Name2, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABYAAABodHRwOi8vdHJpY3ljbGV0eXBldjEv/////wRggAABAAAAAQARAAAASHVtYW5UeXBlSW5z" +
           "dGFuY2UBAQUAAQEFAP////8DAAAAFWCJCgIAAAAAAAQAAABOYW1lAQEGAAAvAD8GAAAAAAz/////AQH/" +
           "////AAAAABVgiQoCAAAAAAADAAAAQWdlAQEHAAAvAD8HAAAAAAb/////AQH/////AAAAABVgiQoCAAAA" +
           "AAAGAAAAR2VuZGVyAQEIAAAuAEQIAAAAAAz/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the Name Variable.
        /// </summary>
        public BaseDataVariableState<string> Name
        {
            get
            {
                return m_name;
            }

            set
            {
                if (!Object.ReferenceEquals(m_name, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_name = value;
            }
        }

        /// <summary>
        /// A description for the Age Variable.
        /// </summary>
        public BaseDataVariableState<int> Age
        {
            get
            {
                return m_age;
            }

            set
            {
                if (!Object.ReferenceEquals(m_age, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_age = value;
            }
        }

        /// <summary>
        /// A description for the Gender Property.
        /// </summary>
        public PropertyState<string> Gender
        {
            get
            {
                return m_gender;
            }

            set
            {
                if (!Object.ReferenceEquals(m_gender, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_gender = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_name != null)
            {
                children.Add(m_name);
            }

            if (m_age != null)
            {
                children.Add(m_age);
            }

            if (m_gender != null)
            {
                children.Add(m_gender);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Prefix0.BrowseNames.Name:
                {
                    if (createOrReplace)
                    {
                        if (Name == null)
                        {
                            if (replacement == null)
                            {
                                Name = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                Name = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = Name;
                    break;
                }

                case Prefix0.BrowseNames.Age:
                {
                    if (createOrReplace)
                    {
                        if (Age == null)
                        {
                            if (replacement == null)
                            {
                                Age = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                Age = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = Age;
                    break;
                }

                case Prefix0.BrowseNames.Gender:
                {
                    if (createOrReplace)
                    {
                        if (Gender == null)
                        {
                            if (replacement == null)
                            {
                                Gender = new PropertyState<string>(this);
                            }
                            else
                            {
                                Gender = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = Gender;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<string> m_name;
        private BaseDataVariableState<int> m_age;
        private PropertyState<string> m_gender;
        #endregion
    }
    #endif
    #endregion

    #region VehicleState Class
    #if (!OPCUA_EXCLUDE_VehicleState)
    /// <summary>
    /// Stores an instance of the VehicleType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class VehicleState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public VehicleState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Prefix2.ObjectTypes.VehicleType, Prefix2.Namespaces.Name2, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABYAAABodHRwOi8vdHJpY3ljbGV0eXBldjEv/////wRggAABAAAAAQATAAAAVmVoaWNsZVR5cGVJ" +
           "bnN0YW5jZQEBCgABAQoA/////wIAAAAVYIkKAgAAAAAACQAAAGJ1aWxkRGF0ZQEBLgAALgBELgAAAAAN" +
           "/////wEB/////wAAAABEYMAKAQAAAAgAAABTX093bmVyXwAABgAAAE93bmVyPgEBLwADAAAAAAcAAAA8" +
           "T3duZXI+AC8BAQUALwAAAP////8DAAAAFWCJCgIAAAAAAAQAAABOYW1lAQEwAAAvAD8wAAAAAAz/////" +
           "AQH/////AAAAABVgiQoCAAAAAAADAAAAQWdlAQExAAAvAD8xAAAAAAb/////AQH/////AAAAABVgiQoC" +
           "AAAAAAAGAAAAR2VuZGVyAQEyAAAuAEQyAAAAAAz/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the buildDate Property.
        /// </summary>
        public PropertyState<DateTime> buildDate
        {
            get
            {
                return m_buildDate;
            }

            set
            {
                if (!Object.ReferenceEquals(m_buildDate, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_buildDate = value;
            }
        }

        /// <summary>
        /// A description for the Owner> Object.
        /// </summary>
        public HumanState S_Owner_
        {
            get
            {
                return m_s_Owner_;
            }

            set
            {
                if (!Object.ReferenceEquals(m_s_Owner_, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_s_Owner_ = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_buildDate != null)
            {
                children.Add(m_buildDate);
            }

            if (m_s_Owner_ != null)
            {
                children.Add(m_s_Owner_);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Prefix0.BrowseNames.buildDate:
                {
                    if (createOrReplace)
                    {
                        if (buildDate == null)
                        {
                            if (replacement == null)
                            {
                                buildDate = new PropertyState<DateTime>(this);
                            }
                            else
                            {
                                buildDate = (PropertyState<DateTime>)replacement;
                            }
                        }
                    }

                    instance = buildDate;
                    break;
                }

                case Prefix0.BrowseNames.S_Owner_:
                {
                    if (createOrReplace)
                    {
                        if (S_Owner_ == null)
                        {
                            if (replacement == null)
                            {
                                S_Owner_ = new HumanState(this);
                            }
                            else
                            {
                                S_Owner_ = (HumanState)replacement;
                            }
                        }
                    }

                    instance = S_Owner_;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<DateTime> m_buildDate;
        private HumanState m_s_Owner_;
        #endregion
    }
    #endif
    #endregion

    #region TrailerState Class
    #if (!OPCUA_EXCLUDE_TrailerState)
    /// <summary>
    /// Stores an instance of the TrailerType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class TrailerState : VehicleState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public TrailerState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Prefix2.ObjectTypes.TrailerType, Prefix2.Namespaces.Name2, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABYAAABodHRwOi8vdHJpY3ljbGV0eXBldjEv/////wRggAABAAAAAQATAAAAVHJhaWxlclR5cGVJ" +
           "bnN0YW5jZQEBCQABAQkAAQAAAAAxAQEBCQACAAAAFWCJCgIAAAAAAAkAAABidWlsZERhdGUBATMAAC4A" +
           "RDMAAAAADf////8BAf////8AAAAARGDACgEAAAAIAAAAU19Pd25lcl8AAAYAAABPd25lcj4BATQAAwAA" +
           "AAAHAAAAPE93bmVyPgAvAQEFADQAAAD/////AwAAABVgiQoCAAAAAAAEAAAATmFtZQEBNQAALwA/NQAA" +
           "AAAM/////wEB/////wAAAAAVYIkKAgAAAAAAAwAAAEFnZQEBNgAALwA/NgAAAAAG/////wEB/////wAA" +
           "AAAVYIkKAgAAAAAABgAAAEdlbmRlcgEBNwAALgBENwAAAAAM/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region TriCycleState Class
    #if (!OPCUA_EXCLUDE_TriCycleState)
    /// <summary>
    /// Stores an instance of the TriCycleType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class TriCycleState : VehicleState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public TriCycleState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Prefix2.ObjectTypes.TriCycleType, Prefix2.Namespaces.Name2, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABYAAABodHRwOi8vdHJpY3ljbGV0eXBldjEv/////wRggAABAAAAAQAUAAAAVHJpQ3ljbGVUeXBl" +
           "SW5zdGFuY2UBAQsAAQELAP////8FAAAAFWCJCgIAAAAAAAkAAABidWlsZERhdGUBATgAAC4ARDgAAAAA" +
           "Df////8BAf////8AAAAARGDACgEAAAAIAAAAU19Pd25lcl8AAAYAAABPd25lcj4BATkAAwAAAAAHAAAA" +
           "PE93bmVyPgAvAQEFADkAAAD/////AwAAABVgiQoCAAAAAAAEAAAATmFtZQEBOgAALwA/OgAAAAAM////" +
           "/wEB/////wAAAAAVYIkKAgAAAAAAAwAAAEFnZQEBOwAALwA/OwAAAAAG/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAABgAAAEdlbmRlcgEBPAAALgBEPAAAAAAM/////wEB/////wAAAAAVYIkKAgAAAAAABgAAAHdl" +
           "aWdodAEBDAAALgBEDAAAAAAK/////wEB/////wAAAAAXYIkKAgAAAAAABgAAAHdoZWVscwEBDQAALgEB" +
           "EwANAAAAAQEEAAEAAAABAAAAAwAAAAEB/////wQAAAAVYIkKAgAAAAAACAAAAHRpY2tuZXNzAQEOAAAv" +
           "AD8OAAAAAAr/////AQH/////AAAAABVgiQoCAAAAAAAIAAAAZGlhbWV0ZXIBAQ8AAC8APw8AAAAACv//" +
           "//8BAf////8AAAAAFWCJCgIAAAAAAAgAAABwcmVzc3VyZQEBEAAALwA/EAAAAAAK/////wEB/////wAA" +
           "AAAVYIkKAgAAAAAACAAAAHRpcmV0eXBlAQERAAAvAD8RAAAAAQEBAP////8BAf////8AAAAAFWCJCgIA" +
           "AAAAAAUAAABNb2RlbAEBEgAALwA/EgAAAAAM/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the weight Property.
        /// </summary>
        public PropertyState<float> weight
        {
            get
            {
                return m_weight;
            }

            set
            {
                if (!Object.ReferenceEquals(m_weight, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_weight = value;
            }
        }

        /// <summary>
        /// A description for the wheels Property.
        /// </summary>
        public WheelVariableState wheels
        {
            get
            {
                return m_wheels;
            }

            set
            {
                if (!Object.ReferenceEquals(m_wheels, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_wheels = value;
            }
        }

        /// <summary>
        /// A description for the Model Variable.
        /// </summary>
        public BaseDataVariableState<string> Model
        {
            get
            {
                return m_model;
            }

            set
            {
                if (!Object.ReferenceEquals(m_model, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_model = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_weight != null)
            {
                children.Add(m_weight);
            }

            if (m_wheels != null)
            {
                children.Add(m_wheels);
            }

            if (m_model != null)
            {
                children.Add(m_model);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Prefix0.BrowseNames.weight:
                {
                    if (createOrReplace)
                    {
                        if (weight == null)
                        {
                            if (replacement == null)
                            {
                                weight = new PropertyState<float>(this);
                            }
                            else
                            {
                                weight = (PropertyState<float>)replacement;
                            }
                        }
                    }

                    instance = weight;
                    break;
                }

                case Prefix0.BrowseNames.wheels:
                {
                    if (createOrReplace)
                    {
                        if (wheels == null)
                        {
                            if (replacement == null)
                            {
                                wheels = new WheelVariableState(this);
                            }
                            else
                            {
                                wheels = (WheelVariableState)replacement;
                            }
                        }
                    }

                    instance = wheels;
                    break;
                }

                case Prefix0.BrowseNames.Model:
                {
                    if (createOrReplace)
                    {
                        if (Model == null)
                        {
                            if (replacement == null)
                            {
                                Model = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                Model = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = Model;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<float> m_weight;
        private WheelVariableState m_wheels;
        private BaseDataVariableState<string> m_model;
        #endregion
    }
    #endif
    #endregion

    #region WheelVariableState Class
    #if (!OPCUA_EXCLUDE_WheelVariableState)
    /// <summary>
    /// Stores an instance of the WheelVariableType VariableType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class WheelVariableState : BaseDataVariableState<WheelDataType>
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public WheelVariableState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Prefix2.VariableTypes.WheelVariableType, Prefix2.Namespaces.Name2, namespaceUris);
        }

        /// <summary>
        /// Returns the id of the default data type node for the instance.
        /// </summary>
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Prefix2.DataTypes.WheelDataType, Prefix2.Namespaces.Name2, namespaceUris);
        }

        /// <summary>
        /// Returns the id of the default value rank for the instance.
        /// </summary>
        protected override int GetDefaultValueRank()
        {
            return ValueRanks.Scalar;
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABYAAABodHRwOi8vdHJpY3ljbGV0eXBldjEv/////xVggQACAAAAAQAZAAAAV2hlZWxWYXJpYWJs" +
           "ZVR5cGVJbnN0YW5jZQEBEwABARMAAQEEAAEB/////wQAAAAVYIkKAgAAAAAACAAAAHRpY2tuZXNzAQEU" +
           "AAAvAD8UAAAAAAr/////AQH/////AAAAABVgiQoCAAAAAAAIAAAAZGlhbWV0ZXIBARUAAC8APxUAAAAA" +
           "Cv////8BAf////8AAAAAFWCJCgIAAAAAAAgAAABwcmVzc3VyZQEBFgAALwA/FgAAAAAK/////wEB////" +
           "/wAAAAAVYIkKAgAAAAAACAAAAHRpcmV0eXBlAQEXAAAvAD8XAAAAAQEBAP////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the tickness Variable.
        /// </summary>
        public BaseDataVariableState<float> tickness
        {
            get
            {
                return m_tickness;
            }

            set
            {
                if (!Object.ReferenceEquals(m_tickness, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_tickness = value;
            }
        }

        /// <summary>
        /// A description for the diameter Variable.
        /// </summary>
        public BaseDataVariableState<float> diameter
        {
            get
            {
                return m_diameter;
            }

            set
            {
                if (!Object.ReferenceEquals(m_diameter, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_diameter = value;
            }
        }

        /// <summary>
        /// A description for the pressure Variable.
        /// </summary>
        public BaseDataVariableState<float> pressure
        {
            get
            {
                return m_pressure;
            }

            set
            {
                if (!Object.ReferenceEquals(m_pressure, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_pressure = value;
            }
        }

        /// <summary>
        /// A description for the tiretype Variable.
        /// </summary>
        public BaseDataVariableState<TireEnum> tiretype
        {
            get
            {
                return m_tiretype;
            }

            set
            {
                if (!Object.ReferenceEquals(m_tiretype, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_tiretype = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_tickness != null)
            {
                children.Add(m_tickness);
            }

            if (m_diameter != null)
            {
                children.Add(m_diameter);
            }

            if (m_pressure != null)
            {
                children.Add(m_pressure);
            }

            if (m_tiretype != null)
            {
                children.Add(m_tiretype);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Prefix0.BrowseNames.tickness:
                {
                    if (createOrReplace)
                    {
                        if (tickness == null)
                        {
                            if (replacement == null)
                            {
                                tickness = new BaseDataVariableState<float>(this);
                            }
                            else
                            {
                                tickness = (BaseDataVariableState<float>)replacement;
                            }
                        }
                    }

                    instance = tickness;
                    break;
                }

                case Prefix0.BrowseNames.diameter:
                {
                    if (createOrReplace)
                    {
                        if (diameter == null)
                        {
                            if (replacement == null)
                            {
                                diameter = new BaseDataVariableState<float>(this);
                            }
                            else
                            {
                                diameter = (BaseDataVariableState<float>)replacement;
                            }
                        }
                    }

                    instance = diameter;
                    break;
                }

                case Prefix0.BrowseNames.pressure:
                {
                    if (createOrReplace)
                    {
                        if (pressure == null)
                        {
                            if (replacement == null)
                            {
                                pressure = new BaseDataVariableState<float>(this);
                            }
                            else
                            {
                                pressure = (BaseDataVariableState<float>)replacement;
                            }
                        }
                    }

                    instance = pressure;
                    break;
                }

                case Prefix0.BrowseNames.tiretype:
                {
                    if (createOrReplace)
                    {
                        if (tiretype == null)
                        {
                            if (replacement == null)
                            {
                                tiretype = new BaseDataVariableState<TireEnum>(this);
                            }
                            else
                            {
                                tiretype = (BaseDataVariableState<TireEnum>)replacement;
                            }
                        }
                    }

                    instance = tiretype;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<float> m_tickness;
        private BaseDataVariableState<float> m_diameter;
        private BaseDataVariableState<float> m_pressure;
        private BaseDataVariableState<TireEnum> m_tiretype;
        #endregion
    }

    #region WheelVariableValue Class
    /// <summary>
    /// A typed version of the _BrowseName_ variable.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class WheelVariableValue : BaseVariableValue
    {
        #region Constructors
        /// <summary>
        /// Initializes the instance with its defalt attribute values.
        /// </summary>
        public WheelVariableValue(WheelVariableState variable, WheelDataType value, object dataLock) : base(dataLock)
        {
            m_value = value;

            if (m_value == null)
            {
                m_value = new WheelDataType();
            }

            Initialize(variable);
        }
        #endregion

        #region Public Members
        /// <summary>
        /// The variable that the value belongs to.
        /// </summary>
        public WheelVariableState Variable
        {
            get { return m_variable; }
        }

        /// <summary>
        /// The value of the variable.
        /// </summary>
        public WheelDataType Value
        {
            get { return m_value;  }
            set { m_value = value; }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Initialize(WheelVariableState variable)
        {
            lock (Lock)
            {
                m_variable = variable;

                variable.Value = m_value;

                variable.OnReadValue = OnReadValue;
                variable.OnSimpleWriteValue = OnWriteValue;

                BaseVariableState instance = null;
                List<BaseInstanceState> updateList = new List<BaseInstanceState>();
                updateList.Add(variable);

                instance = m_variable.tickness;
                instance.OnReadValue = OnRead_tickness;
                instance.OnSimpleWriteValue = OnWrite_tickness;
                updateList.Add(instance);
                instance = m_variable.diameter;
                instance.OnReadValue = OnRead_diameter;
                instance.OnSimpleWriteValue = OnWrite_diameter;
                updateList.Add(instance);
                instance = m_variable.pressure;
                instance.OnReadValue = OnRead_pressure;
                instance.OnSimpleWriteValue = OnWrite_pressure;
                updateList.Add(instance);

                SetUpdateList(updateList);
            }
        }

        /// <summary>
        /// Reads the value of the variable.
        /// </summary>
        protected ServiceResult OnReadValue(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <summary>
        /// Writes the value of the variable.
        /// </summary>
        private ServiceResult OnWriteValue(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value = (WheelDataType)Write(value);
            }

            return ServiceResult.Good;
        }

        #region tickness Access Methods
        /// <summary>
        /// Reads the value of the variable child.
        /// </summary>
        private ServiceResult OnRead_tickness(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.tickness;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <summary>
        /// Writes the value of the variable child.
        /// </summary>
        private ServiceResult OnWrite_tickness(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.tickness = (float)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region diameter Access Methods
        /// <summary>
        /// Reads the value of the variable child.
        /// </summary>
        private ServiceResult OnRead_diameter(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.diameter;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <summary>
        /// Writes the value of the variable child.
        /// </summary>
        private ServiceResult OnWrite_diameter(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.diameter = (float)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion

        #region pressure Access Methods
        /// <summary>
        /// Reads the value of the variable child.
        /// </summary>
        private ServiceResult OnRead_pressure(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            lock (Lock)
            {
                DoBeforeReadProcessing(context, node);

                if (m_value != null)
                {
                    value = m_value.pressure;
                }

                return Read(context, node, indexRange, dataEncoding, ref value, ref statusCode, ref timestamp);
            }
        }

        /// <summary>
        /// Writes the value of the variable child.
        /// </summary>
        private ServiceResult OnWrite_pressure(ISystemContext context, NodeState node, ref object value)
        {
            lock (Lock)
            {
                m_value.pressure = (float)Write(value);
            }

            return ServiceResult.Good;
        }
        #endregion
        #endregion

        #region Private Fields
        private WheelDataType m_value;
        private WheelVariableState m_variable;
        #endregion
    }
    #endregion
    #endif
    #endregion
}