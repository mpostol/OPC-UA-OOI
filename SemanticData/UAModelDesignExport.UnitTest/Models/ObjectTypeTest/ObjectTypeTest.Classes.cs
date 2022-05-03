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
using System.Xml;
using System.Runtime.Serialization;
using ;

namespace ObjectTypeTest
{
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
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the instance with a node.
        /// </summary>
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
           "AQAAADQAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIvVW5pdFRlc3RzL09iamVjdFR5cGVUZXN0" +
           "/////wRggAIBAAAAAQAZAAAAQ29tcGxleE9iamVjdFR5cGVJbnN0YW5jZQEBAQABAQEAAQAAAP////8D" +
           "AAAAFWDJCgIAAAARAAAAQnJvd3NlTmFtZTRub2RlNjYBAA0AAABDaGlsZFByb3BlcnR5AQEDAAAuAEQD" +
           "AAAAABX/////AQH/////AAAAABVgiQoCAAAAAQANAAAAQ2hpbGRWYXJpYWJsZQEBKwAALwEAQAkrAAAA" +
           "ABr/////AQH/////AQAAABVgiQoCAAAAAAAHAAAARVVSYW5nZQEBLwAALgBELwAAAAEAdAP/////AQH/" +
           "////AAAAAARhggoEAAAAAQALAAAAQ2hpbGRNZXRob2QBAQoAAC8BAQoACgAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
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

        /// <remarks />
        public AnalogItemState ChildVariable
        {
            get
            {
                return m_childVariable;
            }

            set
            {
                if (!Object.ReferenceEquals(m_childVariable, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_childVariable = value;
            }
        }

        /// <remarks />
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

            if (m_childVariable != null)
            {
                children.Add(m_childVariable);
            }

            if (m_childMethodMethod != null)
            {
                children.Add(m_childMethodMethod);
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

                case ObjectTypeTest.BrowseNames.ChildVariable:
                {
                    if (createOrReplace)
                    {
                        if (ChildVariable == null)
                        {
                            if (replacement == null)
                            {
                                ChildVariable = new AnalogItemState(this);
                            }
                            else
                            {
                                ChildVariable = (AnalogItemState)replacement;
                            }
                        }
                    }

                    instance = ChildVariable;
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
        private AnalogItemState m_childVariable;
        private ChildMethodMethodState m_childMethodMethod;
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
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the instance with a node.
        /// </summary>
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
           "AQAAADQAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIvVW5pdFRlc3RzL09iamVjdFR5cGVUZXN0" +
           "/////wRggAIBAAAAAQAkAAAARGVyaXZlZEZyb21Db21wbGV4T2JqZWN0VHlwZUluc3RhbmNlAQEQAAEB" +
           "EAAQAAAA/////wMAAAAVYMkKAgAAABEAAABCcm93c2VOYW1lNG5vZGU2NgEADQAAAENoaWxkUHJvcGVy" +
           "dHkBARIAAC4ARBIAAAAAFf////8BAf////8AAAAAFWCJCgIAAAABAA0AAABDaGlsZFZhcmlhYmxlAQEx" +
           "AAAvAQBACTEAAAAAGv////8BAf////8BAAAAFWCJCgIAAAAAAAcAAABFVVJhbmdlAQE1AAAuAEQ1AAAA" +
           "AQB0A/////8BAf////8AAAAARGGCCgQAAAABAAsAAABDaGlsZE1ldGhvZAEBGQADAAAAABIAAABDaGls" +
           "ZE1ldGhvZE5ld05hbWUALwEBCgAZAAAAAQH/////AAAAAA==";
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