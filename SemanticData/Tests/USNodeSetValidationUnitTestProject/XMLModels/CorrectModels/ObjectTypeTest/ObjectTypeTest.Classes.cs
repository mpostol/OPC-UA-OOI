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
    public partial class ComplexObjectState : ServerObjectState
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
           "/////wRggAIBAAAAAQAZAAAAQ29tcGxleE9iamVjdFR5cGVJbnN0YW5jZQEBxAABAcQAxAAAAP////8N" +
           "AAAAF3CJCgIAAAAAAAsAAABTZXJ2ZXJBcnJheQEBxQAALgBExQAAAAAMAQAAAAEAAAAAAAAAAQEAAAAA" +
           "AECPQP////8AAAAAF3CJCgIAAAAAAA4AAABOYW1lc3BhY2VBcnJheQEBxgAALgBExgAAAAAMAQAAAAEA" +
           "AAAAAAAAAQEAAAAAAECPQP////8AAAAAFXCJCgIAAAAAAAwAAABTZXJ2ZXJTdGF0dXMBAccAAC8BAFoI" +
           "xwAAAAEAXgP/////AQEAAAAAAECPQP////8GAAAAFWCJCgIAAAAAAAkAAABTdGFydFRpbWUBAcgAAC8A" +
           "P8gAAAABACYB/////wEB/////wAAAAAVYIkKAgAAAAAACwAAAEN1cnJlbnRUaW1lAQHJAAAvAD/JAAAA" +
           "AQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAUAAABTdGF0ZQEBygAALwA/ygAAAAEAVAP/////AQH/" +
           "////AAAAABVgiQoCAAAAAAAJAAAAQnVpbGRJbmZvAQHLAAAvAQDrC8sAAAABAFIB/////wEB/////wYA" +
           "AAAVcIkKAgAAAAAACgAAAFByb2R1Y3RVcmkBAcwAAC8AP8wAAAAADP////8BAQAAAAAAQI9A/////wAA" +
           "AAAVcIkKAgAAAAAAEAAAAE1hbnVmYWN0dXJlck5hbWUBAc0AAC8AP80AAAAADP////8BAQAAAAAAQI9A" +
           "/////wAAAAAVcIkKAgAAAAAACwAAAFByb2R1Y3ROYW1lAQHOAAAvAD/OAAAAAAz/////AQEAAAAAAECP" +
           "QP////8AAAAAFXCJCgIAAAAAAA8AAABTb2Z0d2FyZVZlcnNpb24BAc8AAC8AP88AAAAADP////8BAQAA" +
           "AAAAQI9A/////wAAAAAVcIkKAgAAAAAACwAAAEJ1aWxkTnVtYmVyAQHQAAAvAD/QAAAAAAz/////AQEA" +
           "AAAAAECPQP////8AAAAAFXCJCgIAAAAAAAkAAABCdWlsZERhdGUBAdEAAC8AP9EAAAABACYB/////wEB" +
           "AAAAAABAj0D/////AAAAABVgiQoCAAAAAAATAAAAU2Vjb25kc1RpbGxTaHV0ZG93bgEB0gAALwA/0gAA" +
           "AAAH/////wEB/////wAAAAAVYIkKAgAAAAAADgAAAFNodXRkb3duUmVhc29uAQHTAAAvAD/TAAAAABX/" +
           "////AQH/////AAAAABVwiQoCAAAAAAAMAAAAU2VydmljZUxldmVsAQHUAAAuAETUAAAAAAP/////AQEA" +
           "AAAAAECPQP////8AAAAAFXCJCgIAAAAAAAgAAABBdWRpdGluZwEB1QAALgBE1QAAAAAB/////wEBAAAA" +
           "AABAj0D/////AAAAAARggAoBAAAAAAASAAAAU2VydmVyQ2FwYWJpbGl0aWVzAQHWAAAvAQDdB9YAAAD/" +
           "////CQAAABdgiQoCAAAAAAASAAAAU2VydmVyUHJvZmlsZUFycmF5AQHXAAAuAETXAAAAAAwBAAAAAQAA" +
           "AAAAAAABAf////8AAAAAF2CJCgIAAAAAAA0AAABMb2NhbGVJZEFycmF5AQHYAAAuAETYAAAAAQAnAQEA" +
           "AAABAAAAAAAAAAEB/////wAAAAAVYIkKAgAAAAAAFgAAAE1pblN1cHBvcnRlZFNhbXBsZVJhdGUBAdkA" +
           "AC4ARNkAAAABACIB/////wEB/////wAAAAAVYIkKAgAAAAAAGwAAAE1heEJyb3dzZUNvbnRpbnVhdGlv" +
           "blBvaW50cwEB2gAALgBE2gAAAAAF/////wEB/////wAAAAAVYIkKAgAAAAAAGgAAAE1heFF1ZXJ5Q29u" +
           "dGludWF0aW9uUG9pbnRzAQHbAAAuAETbAAAAAAX/////AQH/////AAAAABVgiQoCAAAAAAAcAAAATWF4" +
           "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" +
           "LwEA8gcEAQAA/////wEAAAAVYIkKAgAAAAAAEQAAAFJlZHVuZGFuY3lTdXBwb3J0AQEFAQAuAEQFAQAA" +
           "AQBTA/////8BAf////8AAAAAFWDJCgIAAAARAAAAQnJvd3NlTmFtZTRub2RlNjYBAA0AAABDaGlsZFBy" +
           "b3BlcnR5AQE1AQAuAEQ1AQAAABX/////AQEBAAAAAQHDAAEBASEBAAAAABVgyQoCAAAADgAAAE5hbWVO" +
           "b3RTZXQxMTA5AQATAAAAQnJvd3NlTmFtZTRub2RlMTEwOQEBIQEALwEAQAkhAQAAABr/////AQEBAAAA" +
           "AQHDAAABATUBAQAAABVgiQoCAAAAAAAHAAAARVVSYW5nZQEBJQEALgBEJQEAAAEAdAP/////AQH/////" +
           "AAAAAARhggoEAAAAAQALAAAAQ2hpbGRNZXRob2QBAScBAC8BAScBJwEAAAEB/////wIAAAAXYKkKAgAA" +
           "AAAADgAAAElucHV0QXJndW1lbnRzAQErAQAuAEQrAQAAlgIAAAABACoBARUAAAAGAAAASW5wdXQxAAb/" +
           "////AAAAAAABACoBARUAAAAGAAAASW5wdXQyAAb/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////" +
           "AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQEsAQAuAEQsAQAAlgEAAAABACoBARYAAAAH" +
           "AAAAT3V0cHV0MQAG/////wAAAAAAAQAoAQEAAAABAAAAAAAAAAEB/////wAAAAAEYYIKBAAAAAEAEwAA" +
           "AE5vbkV4ZWN1dGFibGVNZXRob2QBATIBAC8BATIBMgEAAAEB/////wAAAAA=";
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

        /// <remarks />
        public ChildMethodComplexObjectMethodState ChildMethod
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

        /// <remarks />
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
                                ChildMethod = new ChildMethodComplexObjectMethodState(this);
                            }
                            else
                            {
                                ChildMethod = (ChildMethodComplexObjectMethodState)replacement;
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
        private ChildMethodComplexObjectMethodState m_childMethodMethod;
        private MethodState m_nonExecutableMethodMethod;
        #endregion
    }
    #endif
    #endregion

    #region ChildMethodComplexObjectMethodState Class
    #if (!OPCUA_EXCLUDE_ChildMethodComplexObjectMethodState)
    /// <summary>
    /// Stores an instance of the ChildMethodComplexObjectType Method.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class ChildMethodComplexObjectMethodState : MethodState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public ChildMethodComplexObjectMethodState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Constructs an instance of a node.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>The new node.</returns>
        public new static NodeState Construct(NodeState parent)
        {
            return new ChildMethodComplexObjectMethodState(parent);
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
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAADQAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIvVW5pdFRlc3RzL09iamVjdFR5cGVUZXN0" +
           "/////wRhggoEAAAAAQAcAAAAQ2hpbGRNZXRob2RDb21wbGV4T2JqZWN0VHlwZQEBLwEALwEBLwEvAQAA" +
           "AQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBATABAC4ARDABAACWAgAAAAEAKgEB" +
           "FQAAAAYAAABJbnB1dDEABv////8AAAAAAAEAKgEBFQAAAAYAAABJbnB1dDIABv////8AAAAAAAEAKAEB" +
           "AAAAAQAAAAAAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBATEBAC4ARDEB" +
           "AACWAQAAAAEAKgEBFgAAAAcAAABPdXRwdXQxAAb/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////" +
           "AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        /// <summary>
        /// Raised when the the method is called.
        /// </summary>
        public ChildMethodComplexObjectMethodStateMethodCallHandler OnCall;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Invokes the method, returns the result and output argument.
        /// </summary>
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult result = null;

            int input1 = (int)_inputArguments[0];
            int input2 = (int)_inputArguments[1];

            int output1 = (int)_outputArguments[0];

            if (OnCall != null)
            {
                result = OnCall(
                    _context,
                    this,
                    _objectId,
                    input1,
                    input2,
                    ref output1);
            }

            _outputArguments[0] = output1;

            return result;
        }
        #endregion

        #region Private Fields
        #endregion
    }

    /// <summary>
    /// Used to receive notifications when the method is called.
    /// </summary>
    /// <exclude />
    public delegate ServiceResult ChildMethodComplexObjectMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        int input1,
        int input2,
        ref int output1);
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
           "/////wRggAIBAAAAAQAkAAAARGVyaXZlZEZyb21Db21wbGV4T2JqZWN0VHlwZUluc3RhbmNlAQE2AQEB" +
           "NgE2AQAA/////w0AAAAXcIkKAgAAAAAACwAAAFNlcnZlckFycmF5AQE3AQAuAEQ3AQAAAAwBAAAAAQAA" +
           "AAAAAAABAQAAAAAAQI9A/////wAAAAAXcIkKAgAAAAAADgAAAE5hbWVzcGFjZUFycmF5AQE4AQAuAEQ4" +
           "AQAAAAwBAAAAAQAAAAAAAAABAQAAAAAAQI9A/////wAAAAAVcIkKAgAAAAAADAAAAFNlcnZlclN0YXR1" +
           "cwEBOQEALwEAWgg5AQAAAQBeA/////8BAQAAAAAAQI9A/////wYAAAAVYIkKAgAAAAAACQAAAFN0YXJ0" +
           "VGltZQEBOgEALwA/OgEAAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAALAAAAQ3VycmVudFRpbWUB" +
           "ATsBAC8APzsBAAABACYB/////wEB/////wAAAAAVYIkKAgAAAAAABQAAAFN0YXRlAQE8AQAvAD88AQAA" +
           "AQBUA/////8BAf////8AAAAAFWCJCgIAAAAAAAkAAABCdWlsZEluZm8BAT0BAC8BAOsLPQEAAAEAUgH/" +
           "////AQH/////BgAAABVwiQoCAAAAAAAKAAAAUHJvZHVjdFVyaQEBPgEALwA/PgEAAAAM/////wEBAAAA" +
           "AABAj0D/////AAAAABVwiQoCAAAAAAAQAAAATWFudWZhY3R1cmVyTmFtZQEBPwEALwA/PwEAAAAM////" +
           "/wEBAAAAAABAj0D/////AAAAABVwiQoCAAAAAAALAAAAUHJvZHVjdE5hbWUBAUABAC8AP0ABAAAADP//" +
           "//8BAQAAAAAAQI9A/////wAAAAAVcIkKAgAAAAAADwAAAFNvZnR3YXJlVmVyc2lvbgEBQQEALwA/QQEA" +
           "AAAM/////wEBAAAAAABAj0D/////AAAAABVwiQoCAAAAAAALAAAAQnVpbGROdW1iZXIBAUIBAC8AP0IB" +
           "AAAADP////8BAQAAAAAAQI9A/////wAAAAAVcIkKAgAAAAAACQAAAEJ1aWxkRGF0ZQEBQwEALwA/QwEA" +
           "AAEAJgH/////AQEAAAAAAECPQP////8AAAAAFWCJCgIAAAAAABMAAABTZWNvbmRzVGlsbFNodXRkb3du" +
           "AQFEAQAvAD9EAQAAAAf/////AQH/////AAAAABVgiQoCAAAAAAAOAAAAU2h1dGRvd25SZWFzb24BAUUB" +
           "AC8AP0UBAAAAFf////8BAf////8AAAAAFXCJCgIAAAAAAAwAAABTZXJ2aWNlTGV2ZWwBAUYBAC4AREYB" +
           "AAAAA/////8BAQAAAAAAQI9A/////wAAAAAVcIkKAgAAAAAACAAAAEF1ZGl0aW5nAQFHAQAuAERHAQAA" +
           "AAH/////AQEAAAAAAECPQP////8AAAAABGCACgEAAAAAABIAAABTZXJ2ZXJDYXBhYmlsaXRpZXMBAUgB" +
           "AC8BAN0HSAEAAP////8JAAAAF2CJCgIAAAAAABIAAABTZXJ2ZXJQcm9maWxlQXJyYXkBAUkBAC4AREkB" +
           "AAAADAEAAAABAAAAAAAAAAEB/////wAAAAAXYIkKAgAAAAAADQAAAExvY2FsZUlkQXJyYXkBAUoBAC4A" +
           "REoBAAABACcBAQAAAAEAAAAAAAAAAQH/////AAAAABVgiQoCAAAAAAAWAAAATWluU3VwcG9ydGVkU2Ft" +
           "cGxlUmF0ZQEBSwEALgBESwEAAAEAIgH/////AQH/////AAAAABVgiQoCAAAAAAAbAAAATWF4QnJvd3Nl" +
           "Q29udGludWF0aW9uUG9pbnRzAQFMAQAuAERMAQAAAAX/////AQH/////AAAAABVgiQoCAAAAAAAaAAAA" +
           "TWF4UXVlcnlDb250aW51YXRpb25Qb2ludHMBAU0BAC4ARE0BAAAABf////8BAf////8AAAAAFWCJCgIA" +
           "AAAAABwAAABNYXhIaXN0b3J5Q29udGludWF0aW9uUG9pbnRzAQFOAQAuAEROAQAAAAX/////AQH/////" +
           "AAAAABdgiQoCAAAAAAAUAAAAU29mdHdhcmVDZXJ0aWZpY2F0ZXMBAU8BAC4ARE8BAAABAFgBAQAAAAEA" +
           "AAAAAAAAAQH/////AAAAAARggAoBAAAAAAAOAAAATW9kZWxsaW5nUnVsZXMBAV8BAC8APV8BAAD/////" +
           "AAAAAARggAoBAAAAAAASAAAAQWdncmVnYXRlRnVuY3Rpb25zAQFgAQAvAD1gAQAA/////wAAAAAEYIAK" +
           "AQAAAAAAEQAAAFNlcnZlckRpYWdub3N0aWNzAQFhAQAvAQDkB2EBAAD/////BAAAABVgiQoCAAAAAAAY" +
           "AAAAU2VydmVyRGlhZ25vc3RpY3NTdW1tYXJ5AQFiAQAvAQBmCGIBAAABAFsD/////wEB/////wwAAAAV" +
           "YIkKAgAAAAAADwAAAFNlcnZlclZpZXdDb3VudAEBYwEALwA/YwEAAAAH/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAAEwAAAEN1cnJlbnRTZXNzaW9uQ291bnQBAWQBAC8AP2QBAAAAB/////8BAf////8AAAAAFWCJ" +
           "CgIAAAAAABUAAABDdW11bGF0ZWRTZXNzaW9uQ291bnQBAWUBAC8AP2UBAAAAB/////8BAf////8AAAAA" +
           "FWCJCgIAAAAAABwAAABTZWN1cml0eVJlamVjdGVkU2Vzc2lvbkNvdW50AQFmAQAvAD9mAQAAAAf/////" +
           "AQH/////AAAAABVgiQoCAAAAAAAUAAAAUmVqZWN0ZWRTZXNzaW9uQ291bnQBAWcBAC8AP2cBAAAAB///" +
           "//8BAf////8AAAAAFWCJCgIAAAAAABMAAABTZXNzaW9uVGltZW91dENvdW50AQFoAQAvAD9oAQAAAAf/" +
           "////AQH/////AAAAABVgiQoCAAAAAAARAAAAU2Vzc2lvbkFib3J0Q291bnQBAWkBAC8AP2kBAAAAB///" +
           "//8BAf////8AAAAAFWCJCgIAAAAAABcAAABQdWJsaXNoaW5nSW50ZXJ2YWxDb3VudAEBagEALwA/agEA" +
           "AAAH/////wEB/////wAAAAAVYIkKAgAAAAAAGAAAAEN1cnJlbnRTdWJzY3JpcHRpb25Db3VudAEBawEA" +
           "LwA/awEAAAAH/////wEB/////wAAAAAVYIkKAgAAAAAAGgAAAEN1bXVsYXRlZFN1YnNjcmlwdGlvbkNv" +
           "dW50AQFsAQAvAD9sAQAAAAf/////AQH/////AAAAABVgiQoCAAAAAAAdAAAAU2VjdXJpdHlSZWplY3Rl" +
           "ZFJlcXVlc3RzQ291bnQBAW0BAC8AP20BAAAAB/////8BAf////8AAAAAFWCJCgIAAAAAABUAAABSZWpl" +
           "Y3RlZFJlcXVlc3RzQ291bnQBAW4BAC8AP24BAAAAB/////8BAf////8AAAAAF2CJCgIAAAAAABwAAABT" +
           "dWJzY3JpcHRpb25EaWFnbm9zdGljc0FycmF5AQFwAQAvAQB7CHABAAABAGoDAQAAAAEAAAAAAAAAAQH/" +
           "////AAAAAARggAoBAAAAAAAaAAAAU2Vzc2lvbnNEaWFnbm9zdGljc1N1bW1hcnkBAXEBAC8BAOoHcQEA" +
           "AP////8CAAAAF2CJCgIAAAAAABcAAABTZXNzaW9uRGlhZ25vc3RpY3NBcnJheQEBcgEALwEAlAhyAQAA" +
           "AQBhAwEAAAABAAAAAAAAAAEB/////wAAAAAXYIkKAgAAAAAAHwAAAFNlc3Npb25TZWN1cml0eURpYWdu" +
           "b3N0aWNzQXJyYXkBAXMBAC8BAMMIcwEAAAEAZAMBAAAAAQAAAAAAAAABAf////8AAAAAFWCJCgIAAAAA" +
           "AAsAAABFbmFibGVkRmxhZwEBdAEALgBEdAEAAAAB/////wMD/////wAAAAAEYIAKAQAAAAAAEAAAAFZl" +
           "bmRvclNlcnZlckluZm8BAXUBAC8BAPEHdQEAAP////8AAAAABGCACgEAAAAAABAAAABTZXJ2ZXJSZWR1" +
           "bmRhbmN5AQF2AQAvAQDyB3YBAAD/////AQAAABVgiQoCAAAAAAARAAAAUmVkdW5kYW5jeVN1cHBvcnQB" +
           "AXcBAC4ARHcBAAABAFMD/////wEB/////wAAAAAVYMkKAgAAABEAAABCcm93c2VOYW1lNG5vZGU2NgEA" +
           "DQAAAENoaWxkUHJvcGVydHkBAZIBAC4ARJIBAAAAFf////8BAQEAAAABAcMAAQEBkwEAAAAAFWDJCgIA" +
           "AAAOAAAATmFtZU5vdFNldDExMDkBABMAAABCcm93c2VOYW1lNG5vZGUxMTA5AQGTAQAvAQBACZMBAAAA" +
           "Gv////8BAQEAAAABAcMAAAEBkgEBAAAAFWCJCgIAAAAAAAcAAABFVVJhbmdlAQGXAQAuAESXAQAAAQB0" +
           "A/////8BAf////8AAAAABGGCCgQAAAABAAsAAABDaGlsZE1ldGhvZAEBmQEALwEBJwGZAQAAAQH/////" +
           "AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAZoBAC4ARJoBAACWAgAAAAEAKgEBFQAAAAYA" +
           "AABJbnB1dDEABv////8AAAAAAAEAKgEBFQAAAAYAAABJbnB1dDIABv////8AAAAAAAEAKAEBAAAAAQAA" +
           "AAAAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAZsBAC4ARJsBAACWAQAA" +
           "AAEAKgEBFgAAAAcAAABPdXRwdXQxAAb/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////AAAAAARh" +
           "ggoEAAAAAQATAAAATm9uRXhlY3V0YWJsZU1ldGhvZAEBnAEALwEBMgGcAQAAAQH/////AAAAAA==";
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