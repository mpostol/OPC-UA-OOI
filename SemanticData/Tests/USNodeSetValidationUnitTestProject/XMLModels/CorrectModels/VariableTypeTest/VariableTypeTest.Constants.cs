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

namespace VariableTypeTest
{
    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <summary>
        /// The identifier for the ComplexVariableType_EURange Variable.
        /// </summary>
        public const uint ComplexVariableType_EURange = 12;

        /// <summary>
        /// The identifier for the ComplexVariableType_VariableChild Variable.
        /// </summary>
        public const uint ComplexVariableType_VariableChild = 14;
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
        /// The identifier for the ComplexVariableType VariableType.
        /// </summary>
        public const uint ComplexVariableType = 8;

        /// <summary>
        /// The identifier for the ArrayVariableType VariableType.
        /// </summary>
        public const uint ArrayVariableType = 15;

        /// <summary>
        /// The identifier for the SimpleVariableWithValueType VariableType.
        /// </summary>
        public const uint SimpleVariableWithValueType = 16;
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
        /// The identifier for the ComplexVariableType_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexVariableType_EURange = new ExpandedNodeId(VariableTypeTest.Variables.ComplexVariableType_EURange, VariableTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexVariableType_VariableChild Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexVariableType_VariableChild = new ExpandedNodeId(VariableTypeTest.Variables.ComplexVariableType_VariableChild, VariableTypeTest.Namespaces.cas);
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
        /// The identifier for the ComplexVariableType VariableType.
        /// </summary>
        public static readonly ExpandedNodeId ComplexVariableType = new ExpandedNodeId(VariableTypeTest.VariableTypes.ComplexVariableType, VariableTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ArrayVariableType VariableType.
        /// </summary>
        public static readonly ExpandedNodeId ArrayVariableType = new ExpandedNodeId(VariableTypeTest.VariableTypes.ArrayVariableType, VariableTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the SimpleVariableWithValueType VariableType.
        /// </summary>
        public static readonly ExpandedNodeId SimpleVariableWithValueType = new ExpandedNodeId(VariableTypeTest.VariableTypes.SimpleVariableWithValueType, VariableTypeTest.Namespaces.cas);
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
        /// The BrowseName for the ArrayVariableType component.
        /// </summary>
        public const string ArrayVariableType = "ArrayVariableType";

        /// <summary>
        /// The BrowseName for the ComplexVariableType component.
        /// </summary>
        public const string ComplexVariableType = "ComplexVariableType";

        /// <summary>
        /// The BrowseName for the SimpleVariableWithValueType component.
        /// </summary>
        public const string SimpleVariableWithValueType = "SimpleVariableWithValueType";

        /// <summary>
        /// The BrowseName for the VariableChild component.
        /// </summary>
        public const string VariableChild = "VariableChild";
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
        /// The URI for the cas namespace (.NET code namespace is 'VariableTypeTest').
        /// </summary>
        public const string cas = "http://cas.eu/UA/CommServer/UnitTests/VariableTypeTest";

        /// <summary>
        /// The URI for the ua namespace (.NET code namespace is '').
        /// </summary>
        public const string ua = "http://opcfoundation.org/UA/";
    }
    #endregion
}