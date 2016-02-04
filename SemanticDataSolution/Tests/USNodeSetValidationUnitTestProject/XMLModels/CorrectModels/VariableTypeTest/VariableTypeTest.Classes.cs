/* ========================================================================
 * Copyright (c) 2005-2011 The OPC Foundation, Inc. All rights reserved.
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

    #region ComplexVariableState Class
    #if (!OPCUA_EXCLUDE_ComplexVariableState)
    /// <summary>
    /// Stores an instance of the ComplexVariableType VariableType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class ComplexVariableState : AnalogItemState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public ComplexVariableState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(VariableTypeTest.VariableTypes.ComplexVariableType, VariableTypeTest.Namespaces.cas, namespaceUris);
        }

        /// <summary>
        /// Returns the id of the default data type node for the instance.
        /// </summary>
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(.DataTypes.Number, .Namespaces.ua, namespaceUris);
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

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAADYAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIvVW5pdFRlc3RzL1ZhcmlhYmxlVHlwZVRl" +
           "c3T/////FWCJAAIAAAABABsAAABDb21wbGV4VmFyaWFibGVUeXBlSW5zdGFuY2UBAQgAAQEIAAAa////" +
           "/wEB/////wIAAAAVYIkKAgAAAAAABwAAAEVVUmFuZ2UBAQwAAC4ARAwAAAABAHQD/////wEB/////wAA" +
           "AAAVYIkKAgAAAAEADQAAAFZhcmlhYmxlQ2hpbGQBAQ4AAC4ARA4AAAAABv////8CAv////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the VariableChild Property.
        /// </summary>
        public PropertyState<int> VariableChild
        {
            get
            {
                return m_variableChild;
            }

            set
            {
                if (!Object.ReferenceEquals(m_variableChild, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_variableChild = value;
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
            if (m_variableChild != null)
            {
                children.Add(m_variableChild);
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
                case VariableTypeTest.BrowseNames.VariableChild:
                {
                    if (createOrReplace)
                    {
                        if (VariableChild == null)
                        {
                            if (replacement == null)
                            {
                                VariableChild = new PropertyState<int>(this);
                            }
                            else
                            {
                                VariableChild = (PropertyState<int>)replacement;
                            }
                        }
                    }

                    instance = VariableChild;
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
        private PropertyState<int> m_variableChild;
        #endregion
    }

    #region ComplexVariableState<T> Class
    /// <summary>
    /// A typed version of the ComplexVariableType variable.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public class ComplexVariableState<T> : ComplexVariableState
    {
        #region Constructors
        /// <summary>
        /// Initializes the instance with its defalt attribute values.
        /// </summary>
        public ComplexVariableState(NodeState parent) : base(parent)
        {
            Value = default(T);
        }

        /// <summary>
        /// Initializes the instance with the default values.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);

            Value = default(T);
            DataType = TypeInfo.GetDataTypeId(typeof(T));
            ValueRank = TypeInfo.GetValueRank(typeof(T));
        }
        #endregion

        #region Public Members
        /// <summary>
        /// The value of the variable.
        /// </summary>
        public new T Value
        {
            get
            {
                return CheckTypeBeforeCast<T>(base.Value, true);
            }

            set
            {
                base.Value = value;
            }
        }
        #endregion
    }
    #endregion
    #endif
    #endregion

    #region ArrayVariableState Class
    #if (!OPCUA_EXCLUDE_ArrayVariableState)
    /// <summary>
    /// Stores an instance of the ArrayVariableType VariableType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class ArrayVariableState : BaseDataVariableState<int[]>
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public ArrayVariableState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(VariableTypeTest.VariableTypes.ArrayVariableType, VariableTypeTest.Namespaces.cas, namespaceUris);
        }

        /// <summary>
        /// Returns the id of the default data type node for the instance.
        /// </summary>
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(.DataTypes.Int32, .Namespaces.ua, namespaceUris);
        }

        /// <summary>
        /// Returns the id of the default value rank for the instance.
        /// </summary>
        protected override int GetDefaultValueRank()
        {
            return 4;
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

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAADYAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIvVW5pdFRlc3RzL1ZhcmlhYmxlVHlwZVRl" +
           "c3T/////F2CJAAIAAAABABkAAABBcnJheVZhcmlhYmxlVHlwZUluc3RhbmNlAQEPAAEBDwAABgMAAAAD" +
           "AAAAAAAAAAAAAAAAAAAAAQH/////AAAAAA==";
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

    #region SimpleVariableWithValueState Class
    #if (!OPCUA_EXCLUDE_SimpleVariableWithValueState)
    /// <summary>
    /// Stores an instance of the SimpleVariableWithValueType VariableType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class SimpleVariableWithValueState : BaseDataVariableState<int>
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public SimpleVariableWithValueState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(VariableTypeTest.VariableTypes.SimpleVariableWithValueType, VariableTypeTest.Namespaces.cas, namespaceUris);
        }

        /// <summary>
        /// Returns the id of the default data type node for the instance.
        /// </summary>
        protected override NodeId GetDefaultDataTypeId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(.DataTypes.Int32, .Namespaces.ua, namespaceUris);
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

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAADYAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIvVW5pdFRlc3RzL1ZhcmlhYmxlVHlwZVRl" +
           "c3T/////FWCpAAIAAAABACMAAABTaW1wbGVWYXJpYWJsZVdpdGhWYWx1ZVR5cGVJbnN0YW5jZQEBEAAB" +
           "ARAABgEAAAAABv////8BAf////8AAAAA";
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
}