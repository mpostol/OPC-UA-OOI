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

namespace ObjectTypeTest
{
    #region Method Identifiers
    /// <summary>
    /// A class that declares constants for all Methods in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Methods
    {
        /// <summary>
        /// The identifier for the ComplexObjectType_ChildMethod Method.
        /// </summary>
        public const uint ComplexObjectType_ChildMethod = 295;

        /// <summary>
        /// The identifier for the ComplexObjectType_NonExecutableMethod Method.
        /// </summary>
        public const uint ComplexObjectType_NonExecutableMethod = 306;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ChildMethod Method.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ChildMethod = 409;

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_ChildMethod Method.
        /// </summary>
        public const uint InstanceOfDerivedFromComplexObjectType_ChildMethod = 525;

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_NonExecutableMethod Method.
        /// </summary>
        public const uint InstanceOfDerivedFromComplexObjectType_NonExecutableMethod = 528;
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
        /// The identifier for the ComplexObjectType_ChildObject Object.
        /// </summary>
        public const uint ComplexObjectType_ChildObject = 308;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities Object.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities = 214;

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType Object.
        /// </summary>
        public const uint InstanceOfDerivedFromComplexObjectType = 516;

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_ServerCapabilities Object.
        /// </summary>
        public const uint InstanceOfDerivedFromComplexObjectType_ServerCapabilities = 529;
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
        /// The identifier for the ComplexObjectType ObjectType.
        /// </summary>
        public const uint ComplexObjectType = 196;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType ObjectType.
        /// </summary>
        public const uint DerivedFromComplexObjectType = 310;
    }
    #endregion

    #region ReferenceType Identifiers
    /// <summary>
    /// A class that declares constants for all ReferenceTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ReferenceTypes
    {
        /// <summary>
        /// The identifier for the FlowTo ReferenceType.
        /// </summary>
        public const uint FlowTo = 195;
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
        /// The identifier for the ComplexObjectType_BrowseName4node66 Variable.
        /// </summary>
        public const uint ComplexObjectType_BrowseName4node66 = 309;

        /// <summary>
        /// The identifier for the ComplexObjectType_NameNotSet1109 Variable.
        /// </summary>
        public const uint ComplexObjectType_NameNotSet1109 = 289;

        /// <summary>
        /// The identifier for the ComplexObjectType_NameNotSet1109_EURange Variable.
        /// </summary>
        public const uint ComplexObjectType_NameNotSet1109_EURange = 293;

        /// <summary>
        /// The identifier for the ComplexObjectType_ChildMethod_InputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_ChildMethod_InputArguments = 299;

        /// <summary>
        /// The identifier for the ComplexObjectType_ChildMethod_OutputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_ChildMethod_OutputArguments = 300;

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_BrowseName4node66 Variable.
        /// </summary>
        public const uint InstanceOfDerivedFromComplexObjectType_BrowseName4node66 = 518;

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_NameNotSet1109 Variable.
        /// </summary>
        public const uint InstanceOfDerivedFromComplexObjectType_NameNotSet1109 = 519;

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_NameNotSet1109_EURange Variable.
        /// </summary>
        public const uint InstanceOfDerivedFromComplexObjectType_NameNotSet1109_EURange = 523;
    }
    #endregion

    #region Method Node Identifiers
    /// <summary>
    /// A class that declares constants for all Methods in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class MethodIds
    {
        /// <summary>
        /// The identifier for the ComplexObjectType_ChildMethod Method.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ChildMethod = new ExpandedNodeId(ObjectTypeTest.Methods.ComplexObjectType_ChildMethod, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_NonExecutableMethod Method.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_NonExecutableMethod = new ExpandedNodeId(ObjectTypeTest.Methods.ComplexObjectType_NonExecutableMethod, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ChildMethod Method.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ChildMethod = new ExpandedNodeId(ObjectTypeTest.Methods.DerivedFromComplexObjectType_ChildMethod, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_ChildMethod Method.
        /// </summary>
        public static readonly ExpandedNodeId InstanceOfDerivedFromComplexObjectType_ChildMethod = new ExpandedNodeId(ObjectTypeTest.Methods.InstanceOfDerivedFromComplexObjectType_ChildMethod, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_NonExecutableMethod Method.
        /// </summary>
        public static readonly ExpandedNodeId InstanceOfDerivedFromComplexObjectType_NonExecutableMethod = new ExpandedNodeId(ObjectTypeTest.Methods.InstanceOfDerivedFromComplexObjectType_NonExecutableMethod, ObjectTypeTest.Namespaces.cas);
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
        /// The identifier for the ComplexObjectType_ChildObject Object.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ChildObject = new ExpandedNodeId(ObjectTypeTest.Objects.ComplexObjectType_ChildObject, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities Object.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities = new ExpandedNodeId(ObjectTypeTest.Objects.ComplexObjectType_ServerCapabilities, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType Object.
        /// </summary>
        public static readonly ExpandedNodeId InstanceOfDerivedFromComplexObjectType = new ExpandedNodeId(ObjectTypeTest.Objects.InstanceOfDerivedFromComplexObjectType, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_ServerCapabilities Object.
        /// </summary>
        public static readonly ExpandedNodeId InstanceOfDerivedFromComplexObjectType_ServerCapabilities = new ExpandedNodeId(ObjectTypeTest.Objects.InstanceOfDerivedFromComplexObjectType_ServerCapabilities, ObjectTypeTest.Namespaces.cas);
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
        /// The identifier for the ComplexObjectType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType = new ExpandedNodeId(ObjectTypeTest.ObjectTypes.ComplexObjectType, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType = new ExpandedNodeId(ObjectTypeTest.ObjectTypes.DerivedFromComplexObjectType, ObjectTypeTest.Namespaces.cas);
    }
    #endregion

    #region ReferenceType Node Identifiers
    /// <summary>
    /// A class that declares constants for all ReferenceTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ReferenceTypeIds
    {
        /// <summary>
        /// The identifier for the FlowTo ReferenceType.
        /// </summary>
        public static readonly ExpandedNodeId FlowTo = new ExpandedNodeId(ObjectTypeTest.ReferenceTypes.FlowTo, ObjectTypeTest.Namespaces.cas);
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
        /// The identifier for the ComplexObjectType_BrowseName4node66 Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_BrowseName4node66 = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_BrowseName4node66, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_NameNotSet1109 Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_NameNotSet1109 = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_NameNotSet1109, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_NameNotSet1109_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_NameNotSet1109_EURange = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_NameNotSet1109_EURange, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ChildMethod_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ChildMethod_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ChildMethod_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ChildMethod_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ChildMethod_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ChildMethod_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_BrowseName4node66 Variable.
        /// </summary>
        public static readonly ExpandedNodeId InstanceOfDerivedFromComplexObjectType_BrowseName4node66 = new ExpandedNodeId(ObjectTypeTest.Variables.InstanceOfDerivedFromComplexObjectType_BrowseName4node66, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_NameNotSet1109 Variable.
        /// </summary>
        public static readonly ExpandedNodeId InstanceOfDerivedFromComplexObjectType_NameNotSet1109 = new ExpandedNodeId(ObjectTypeTest.Variables.InstanceOfDerivedFromComplexObjectType_NameNotSet1109, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the InstanceOfDerivedFromComplexObjectType_NameNotSet1109_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId InstanceOfDerivedFromComplexObjectType_NameNotSet1109_EURange = new ExpandedNodeId(ObjectTypeTest.Variables.InstanceOfDerivedFromComplexObjectType_NameNotSet1109_EURange, ObjectTypeTest.Namespaces.cas);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the BrowseName4node66 component.
        /// </summary>
        public const string BrowseName4node66 = "ChildProperty";

        /// <summary>
        /// The BrowseName for the ChildMethod component.
        /// </summary>
        public const string ChildMethod = "ChildMethod";

        /// <summary>
        /// The BrowseName for the ChildObject component.
        /// </summary>
        public const string ChildObject = "ChildObject";

        /// <summary>
        /// The BrowseName for the ComplexObjectType component.
        /// </summary>
        public const string ComplexObjectType = "ComplexObjectType";

        /// <summary>
        /// The BrowseName for the DerivedFromComplexObjectType component.
        /// </summary>
        public const string DerivedFromComplexObjectType = "DerivedFromComplexObjectType";

        /// <summary>
        /// The BrowseName for the FlowTo component.
        /// </summary>
        public const string FlowTo = "FlowTo";

        /// <summary>
        /// The BrowseName for the InstanceOfDerivedFromComplexObjectType component.
        /// </summary>
        public const string InstanceOfDerivedFromComplexObjectType = "InstanceOfDerivedFromComplexObjectType";

        /// <summary>
        /// The BrowseName for the NameNotSet1109 component.
        /// </summary>
        public const string NameNotSet1109 = "BrowseName4node1109";

        /// <summary>
        /// The BrowseName for the NonExecutableMethod component.
        /// </summary>
        public const string NonExecutableMethod = "NonExecutableMethod";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the cas namespace (.NET code namespace is 'ObjectTypeTest').
        /// </summary>
        public const string cas = "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest";

        /// <summary>
        /// The URI for the ua namespace (.NET code namespace is '').
        /// </summary>
        public const string ua = "http://opcfoundation.org/UA/";
    }
    #endregion

    #region ComplexObjectState Class
    #if (!OPCUA_EXCLUDE_ComplexObjectState)
    /// <summary>
    /// Stores an instance of the ComplexObjectType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class ComplexObjectState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public ComplexObjectState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(ObjectTypeTest.ObjectTypes.ComplexObjectType, ObjectTypeTest.Namespaces.cas, namespaceUris);
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
           "AQAAADQAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIvVW5pdFRlc3RzL09iamVjdFR5cGVUZXN0" +
           "/////wRggAABAAAAAQAZAAAAQ29tcGxleE9iamVjdFR5cGVJbnN0YW5jZQEBxAABAcQA/////wUAAAAV" +
           "YMkKAgAAABEAAABCcm93c2VOYW1lNG5vZGU2NgEADQAAAENoaWxkUHJvcGVydHkBATUBAC4ARDUBAAAA" +
           "Ff////8BAQEAAAABAcMAAQEBIQEAAAAAFWDJCgIAAAAOAAAATmFtZU5vdFNldDExMDkBABMAAABCcm93" +
           "c2VOYW1lNG5vZGUxMTA5AQEhAQAvAQBACSEBAAAAGv////8BAQEAAAABAcMAAAEBNQEBAAAAFWCJCgIA" +
           "AAAAAAcAAABFVVJhbmdlAQElAQAuAEQlAQAAAQB0A/////8BAf////8AAAAABGGCCgQAAAABAAsAAABD" +
           "aGlsZE1ldGhvZAEBJwEALwEBJwEnAQAAAQH/////AAAAAARhggoEAAAAAQATAAAATm9uRXhlY3V0YWJs" +
           "ZU1ldGhvZAEBMgEALwEBMgEyAQAAAQH/////AAAAAARggAoBAAAAAAASAAAAU2VydmVyQ2FwYWJpbGl0" +
           "aWVzAQHWAAAvADrWAAAA/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the ChildProperty Property.
        /// </summary>
        public PropertyState<LocalizedText> BrowseName4node66
        {
            get
            {
                return m_browseName4node66;
            }

            set
            {
                if (!Object.ReferenceEquals(m_browseName4node66, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_browseName4node66 = value;
            }
        }

        /// <summary>
        /// A description for the BrowseName4node1109 Variable.
        /// </summary>
        public AnalogItemState NameNotSet1109
        {
            get
            {
                return m_nameNotSet1109;
            }

            set
            {
                if (!Object.ReferenceEquals(m_nameNotSet1109, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_nameNotSet1109 = value;
            }
        }

        /// <summary>
        /// A description for the ChildMethod Method.
        /// </summary>
        public ChildMethodMethodState ChildMethod
        {
            get
            {
                return m_childMethodMethod;
            }

            set
            {
                if (!Object.ReferenceEquals(m_childMethodMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_childMethodMethod = value;
            }
        }

        /// <summary>
        /// A description for the NonExecutableMethod Method.
        /// </summary>
        public MethodState NonExecutableMethod
        {
            get
            {
                return m_nonExecutableMethodMethod;
            }

            set
            {
                if (!Object.ReferenceEquals(m_nonExecutableMethodMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_nonExecutableMethodMethod = value;
            }
        }

        /// <summary>
        /// A description for the ServerCapabilities Object.
        /// </summary>
        public BaseObjectState ServerCapabilities
        {
            get
            {
                return m_serverCapabilities;
            }

            set
            {
                if (!Object.ReferenceEquals(m_serverCapabilities, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_serverCapabilities = value;
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
            if (m_browseName4node66 != null)
            {
                children.Add(m_browseName4node66);
            }

            if (m_nameNotSet1109 != null)
            {
                children.Add(m_nameNotSet1109);
            }

            if (m_childMethodMethod != null)
            {
                children.Add(m_childMethodMethod);
            }

            if (m_nonExecutableMethodMethod != null)
            {
                children.Add(m_nonExecutableMethodMethod);
            }

            if (m_serverCapabilities != null)
            {
                children.Add(m_serverCapabilities);
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
                case ObjectTypeTest.BrowseNames.BrowseName4node66:
                {
                    if (createOrReplace)
                    {
                        if (BrowseName4node66 == null)
                        {
                            if (replacement == null)
                            {
                                BrowseName4node66 = new PropertyState<LocalizedText>(this);
                            }
                            else
                            {
                                BrowseName4node66 = (PropertyState<LocalizedText>)replacement;
                            }
                        }
                    }

                    instance = BrowseName4node66;
                    break;
                }

                case ObjectTypeTest.BrowseNames.NameNotSet1109:
                {
                    if (createOrReplace)
                    {
                        if (NameNotSet1109 == null)
                        {
                            if (replacement == null)
                            {
                                NameNotSet1109 = new AnalogItemState(this);
                            }
                            else
                            {
                                NameNotSet1109 = (AnalogItemState)replacement;
                            }
                        }
                    }

                    instance = NameNotSet1109;
                    break;
                }

                case ObjectTypeTest.BrowseNames.ChildMethod:
                {
                    if (createOrReplace)
                    {
                        if (ChildMethod == null)
                        {
                            if (replacement == null)
                            {
                                ChildMethod = new ChildMethodMethodState(this);
                            }
                            else
                            {
                                ChildMethod = (ChildMethodMethodState)replacement;
                            }
                        }
                    }

                    instance = ChildMethod;
                    break;
                }

                case ObjectTypeTest.BrowseNames.NonExecutableMethod:
                {
                    if (createOrReplace)
                    {
                        if (NonExecutableMethod == null)
                        {
                            if (replacement == null)
                            {
                                NonExecutableMethod = new MethodState(this);
                            }
                            else
                            {
                                NonExecutableMethod = (MethodState)replacement;
                            }
                        }
                    }

                    instance = NonExecutableMethod;
                    break;
                }

                case .BrowseNames.ServerCapabilities:
                {
                    if (createOrReplace)
                    {
                        if (ServerCapabilities == null)
                        {
                            if (replacement == null)
                            {
                                ServerCapabilities = new BaseObjectState(this);
                            }
                            else
                            {
                                ServerCapabilities = (BaseObjectState)replacement;
                            }
                        }
                    }

                    instance = ServerCapabilities;
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
        private PropertyState<LocalizedText> m_browseName4node66;
        private AnalogItemState m_nameNotSet1109;
        private ChildMethodMethodState m_childMethodMethod;
        private MethodState m_nonExecutableMethodMethod;
        private BaseObjectState m_serverCapabilities;
        #endregion
    }
    #endif
    #endregion

    #region DerivedFromComplexObjectState Class
    #if (!OPCUA_EXCLUDE_DerivedFromComplexObjectState)
    /// <summary>
    /// Stores an instance of the DerivedFromComplexObjectType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class DerivedFromComplexObjectState : ComplexObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public DerivedFromComplexObjectState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(ObjectTypeTest.ObjectTypes.DerivedFromComplexObjectType, ObjectTypeTest.Namespaces.cas, namespaceUris);
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
           "AQAAADQAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIvVW5pdFRlc3RzL09iamVjdFR5cGVUZXN0" +
           "/////wRggAABAAAAAQAkAAAARGVyaXZlZEZyb21Db21wbGV4T2JqZWN0VHlwZUluc3RhbmNlAQE2AQEB" +
           "NgH/////BQAAABVgyQoCAAAAEQAAAEJyb3dzZU5hbWU0bm9kZTY2AQANAAAAQ2hpbGRQcm9wZXJ0eQEB" +
           "kgEALgBEkgEAAAAV/////wEBAQAAAAEBwwABAQGTAQAAAAAVYMkKAgAAAA4AAABOYW1lTm90U2V0MTEw" +
           "OQEAEwAAAEJyb3dzZU5hbWU0bm9kZTExMDkBAZMBAC8BAEAJkwEAAAAa/////wEBAQAAAAEBwwAAAQGS" +
           "AQEAAAAVYIkKAgAAAAAABwAAAEVVUmFuZ2UBAZcBAC4ARJcBAAABAHQD/////wEB/////wAAAABEYYIK" +
           "BAAAAAEACwAAAENoaWxkTWV0aG9kAQGZAQMAAAAAEgAAAENoaWxkTWV0aG9kTmV3TmFtZQAvAQEnAZkB" +
           "AAABAf////8AAAAABGGCCgQAAAABABMAAABOb25FeGVjdXRhYmxlTWV0aG9kAQGcAQAvAQEyAZwBAAAB" +
           "Af////8AAAAABGCACgEAAAAAABIAAABTZXJ2ZXJDYXBhYmlsaXRpZXMBAUgBAC8AOkgBAAD/////AAAA" +
           "AA==";
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