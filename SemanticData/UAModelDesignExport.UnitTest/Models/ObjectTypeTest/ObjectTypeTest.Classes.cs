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
        /// The identifier for the ChildMethodComplexObjectType Method.
        /// </summary>
        public const uint ChildMethodComplexObjectType = 303;

        /// <summary>
        /// The identifier for the NonExecutableMethodComplexObjectType Method.
        /// </summary>
        public const uint NonExecutableMethodComplexObjectType = 307;
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
        /// The identifier for the ChildMethodComplexObjectType_InputArguments Variable.
        /// </summary>
        public const uint ChildMethodComplexObjectType_InputArguments = 304;

        /// <summary>
        /// The identifier for the ChildMethodComplexObjectType_OutputArguments Variable.
        /// </summary>
        public const uint ChildMethodComplexObjectType_OutputArguments = 305;
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
        /// The identifier for the ChildMethodComplexObjectType Method.
        /// </summary>
        public static readonly ExpandedNodeId ChildMethodComplexObjectType = new ExpandedNodeId(ObjectTypeTest.Methods.ChildMethodComplexObjectType, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NonExecutableMethodComplexObjectType Method.
        /// </summary>
        public static readonly ExpandedNodeId NonExecutableMethodComplexObjectType = new ExpandedNodeId(ObjectTypeTest.Methods.NonExecutableMethodComplexObjectType, ObjectTypeTest.Namespaces.cas);
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
        /// The identifier for the ChildMethodComplexObjectType_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ChildMethodComplexObjectType_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ChildMethodComplexObjectType_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ChildMethodComplexObjectType_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ChildMethodComplexObjectType_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ChildMethodComplexObjectType_OutputArguments, ObjectTypeTest.Namespaces.cas);
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
        /// The BrowseName for the ChildMethodComplexObjectType component.
        /// </summary>
        public const string ChildMethodComplexObjectType = "ChildMethodComplexObjectType";

        /// <summary>
        /// The BrowseName for the ChildObject component.
        /// </summary>
        public const string ChildObject = "ChildObject";

        /// <summary>
        /// The BrowseName for the ComplexObjectType component.
        /// </summary>
        public const string ComplexObjectType = "ComplexObjectType";

        /// <summary>
        /// The BrowseName for the FlowTo component.
        /// </summary>
        public const string FlowTo = "FlowTo";

        /// <summary>
        /// The BrowseName for the NameNotSet1109 component.
        /// </summary>
        public const string NameNotSet1109 = "BrowseName4node1109";

        /// <summary>
        /// The BrowseName for the NonExecutableMethod component.
        /// </summary>
        public const string NonExecutableMethod = "NonExecutableMethod";

        /// <summary>
        /// The BrowseName for the NonExecutableMethodComplexObjectType component.
        /// </summary>
        public const string NonExecutableMethodComplexObjectType = "NonExecutableMethodComplexObjectType";
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
           "/////wRggAABAAAAAQAZAAAAQ29tcGxleE9iamVjdFR5cGVJbnN0YW5jZQEBxAABAcQA/////w0AAAA1" +
           "cIkKAgAAAAAACwAAAFNlcnZlckFycmF5AQHFAAMAAAAAKwAAAFRoZSBsaXN0IG9mIHNlcnZlciBVUklz" +
           "IHVzZWQgYnkgdGhlIHNlcnZlci4ALgBExQAAAAAMAQAAAAEBAAAAAABAj0D/////AAAAADVwiQoCAAAA" +
           "AAAOAAAATmFtZXNwYWNlQXJyYXkBAcYAAwAAAAAuAAAAVGhlIGxpc3Qgb2YgbmFtZXNwYWNlIFVSSXMg" +
           "dXNlZCBieSB0aGUgc2VydmVyLgAuAETGAAAAAAwBAAAAAQEAAAAAAECPQP////8AAAAANXCJCgIAAAAA" +
           "AAwAAABTZXJ2ZXJTdGF0dXMBAccAAwAAAAAhAAAAVGhlIGN1cnJlbnQgc3RhdHVzIG9mIHRoZSBzZXJ2" +
           "ZXIuAC8BAFoIxwAAAAEAXgP/////AQEAAAAAAECPQP////8GAAAAFWCJCgIAAAAAAAkAAABTdGFydFRp" +
           "bWUBAcgAAC8AP8gAAAABACYB/////wEB/////wAAAAAVYIkKAgAAAAAACwAAAEN1cnJlbnRUaW1lAQHJ" +
           "AAAvAD/JAAAAAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAUAAABTdGF0ZQEBygAALwA/ygAAAAEA" +
           "VAP/////AQH/////AAAAABVgiQoCAAAAAAAJAAAAQnVpbGRJbmZvAQHLAAAvAQDrC8sAAAABAFIB////" +
           "/wEB/////wYAAAAVcIkKAgAAAAAACgAAAFByb2R1Y3RVcmkBAcwAAC8AP8wAAAAADP////8BAQAAAAAA" +
           "QI9A/////wAAAAAVcIkKAgAAAAAAEAAAAE1hbnVmYWN0dXJlck5hbWUBAc0AAC8AP80AAAAADP////8B" +
           "AQAAAAAAQI9A/////wAAAAAVcIkKAgAAAAAACwAAAFByb2R1Y3ROYW1lAQHOAAAvAD/OAAAAAAz/////" +
           "AQEAAAAAAECPQP////8AAAAAFXCJCgIAAAAAAA8AAABTb2Z0d2FyZVZlcnNpb24BAc8AAC8AP88AAAAA" +
           "DP////8BAQAAAAAAQI9A/////wAAAAAVcIkKAgAAAAAACwAAAEJ1aWxkTnVtYmVyAQHQAAAvAD/QAAAA" +
           "AAz/////AQEAAAAAAECPQP////8AAAAAFXCJCgIAAAAAAAkAAABCdWlsZERhdGUBAdEAAC8AP9EAAAAB" +
           "ACYB/////wEBAAAAAABAj0D/////AAAAABVgiQoCAAAAAAATAAAAU2Vjb25kc1RpbGxTaHV0ZG93bgEB" +
           "0gAALwA/0gAAAAAH/////wEB/////wAAAAAVYIkKAgAAAAAADgAAAFNodXRkb3duUmVhc29uAQHTAAAv" +
           "AD/TAAAAABX/////AQH/////AAAAADVwiQoCAAAAAAAMAAAAU2VydmljZUxldmVsAQHUAAMAAAAAVwAA" +
           "AEEgdmFsdWUgaW5kaWNhdGluZyB0aGUgbGV2ZWwgb2Ygc2VydmljZSB0aGUgc2VydmVyIGNhbiBwcm92" +
           "aWRlLiAyNTUgaW5kaWNhdGVzIHRoZSBiZXN0LgAuAETUAAAAAAP/////AQEAAAAAAECPQP////8AAAAA" +
           "NXCJCgIAAAAAAAgAAABBdWRpdGluZwEB1QADAAAAAEoAAABBIGZsYWcgaW5kaWNhdGluZyB3aGV0aGVy" +
           "IHRoZSBzZXJ2ZXIgaXMgY3VycmVudGx5IGdlbmVyYXRpbmcgYXVkaXQgZXZlbnRzLgAuAETVAAAAAAH/" +
           "////AQEAAAAAAECPQP////8AAAAAJGCACgEAAAAAABIAAABTZXJ2ZXJDYXBhYmlsaXRpZXMBAdYAAwAA" +
           "AAAvAAAARGVzY3JpYmVzIGNhcGFiaWxpdGllcyBzdXBwb3J0ZWQgYnkgdGhlIHNlcnZlci4ALwEA3QfW" +
           "AAAA/////wkAAAA1YIkKAgAAAAAAEgAAAFNlcnZlclByb2ZpbGVBcnJheQEB1wADAAAAACsAAABBIGxp" +
           "c3Qgb2YgcHJvZmlsZXMgc3VwcG9ydGVkIGJ5IHRoZSBzZXJ2ZXIuAC4ARNcAAAAADAEAAAABAf////8A" +
           "AAAANWCJCgIAAAAAAA0AAABMb2NhbGVJZEFycmF5AQHYAAMAAAAAKgAAAEEgbGlzdCBvZiBsb2NhbGVz" +
           "IHN1cHBvcnRlZCBieSB0aGUgc2VydmVyLgAuAETYAAAAAQAnAQEAAAABAf////8AAAAANWCJCgIAAAAA" +
           "ABYAAABNaW5TdXBwb3J0ZWRTYW1wbGVSYXRlAQHZAAMAAAAANgAAAFRoZSBtaW5pbXVtIHNhbXBsaW5n" +
           "IGludGVydmFsIHN1cHBvcnRlZCBieSB0aGUgc2VydmVyLgAuAETZAAAAAQAiAf////8BAf////8AAAAA" +
           "NWCJCgIAAAAAABsAAABNYXhCcm93c2VDb250aW51YXRpb25Qb2ludHMBAdoAAwAAAABMAAAAVGhlIG1h" +
           "eGltdW0gbnVtYmVyIG9mIGNvbnRpbnVhdGlvbiBwb2ludHMgZm9yIEJyb3dzZSBvcGVyYXRpb25zIHBl" +
           "ciBzZXNzaW9uLgAuAETaAAAAAAX/////AQH/////AAAAADVgiQoCAAAAAAAaAAAATWF4UXVlcnlDb250" +
           "aW51YXRpb25Qb2ludHMBAdsAAwAAAABLAAAAVGhlIG1heGltdW0gbnVtYmVyIG9mIGNvbnRpbnVhdGlv" +
           "biBwb2ludHMgZm9yIFF1ZXJ5IG9wZXJhdGlvbnMgcGVyIHNlc3Npb24uAC4ARNsAAAAABf////8BAf//" +
           "//8AAAAANWCJCgIAAAAAABwAAABNYXhIaXN0b3J5Q29udGludWF0aW9uUG9pbnRzAQHcAAMAAAAAUQAA" +
           "AFRoZSBtYXhpbXVtIG51bWJlciBvZiBjb250aW51YXRpb24gcG9pbnRzIGZvciBSZWFkSGlzdG9yeSBv" +
           "cGVyYXRpb25zIHBlciBzZXNzaW9uLgAuAETcAAAAAAX/////AQH/////AAAAADVgiQoCAAAAAAAUAAAA" +
           "U29mdHdhcmVDZXJ0aWZpY2F0ZXMBAd0AAwAAAAAuAAAAVGhlIHNvZnR3YXJlIGNlcnRpZmljYXRlcyBv" +
           "d25lZCBieSB0aGUgc2VydmVyLgAuAETdAAAAAQBYAQEAAAABAf////8AAAAAJGCACgEAAAAAAA4AAABN" +
           "b2RlbGxpbmdSdWxlcwEB7QADAAAAADkAAABBIGZvbGRlciBmb3IgdGhlIG1vZGVsbGluZyBydWxlcyBz" +
           "dXBwb3J0ZWQgYnkgdGhlIHNlcnZlci4ALwA97QAAAP////8AAAAAJGCACgEAAAAAABIAAABBZ2dyZWdh" +
           "dGVGdW5jdGlvbnMBAe4AAwAAAAA+AAAAQSBmb2xkZXIgZm9yIHRoZSByZWFsIHRpbWUgYWdncmVnYXRl" +
           "cyBzdXBwb3J0ZWQgYnkgdGhlIHNlcnZlci4ALwA97gAAAP////8AAAAAJGCACgEAAAAAABEAAABTZXJ2" +
           "ZXJEaWFnbm9zdGljcwEB7wADAAAAACUAAABSZXBvcnRzIGRpYWdub3N0aWNzIGFib3V0IHRoZSBzZXJ2" +
           "ZXIuAC8BAOQH7wAAAP////8EAAAANWCJCgIAAAAAABgAAABTZXJ2ZXJEaWFnbm9zdGljc1N1bW1hcnkB" +
           "AfAAAwAAAAAmAAAAQSBzdW1tYXJ5IG9mIHNlcnZlciBsZXZlbCBkaWFnbm9zdGljcy4ALwEAZgjwAAAA" +
           "AQBbA/////8BAf////8MAAAAFWCJCgIAAAAAAA8AAABTZXJ2ZXJWaWV3Q291bnQBAfEAAC8AP/EAAAAA" +
           "B/////8BAf////8AAAAAFWCJCgIAAAAAABMAAABDdXJyZW50U2Vzc2lvbkNvdW50AQHyAAAvAD/yAAAA" +
           "AAf/////AQH/////AAAAABVgiQoCAAAAAAAVAAAAQ3VtdWxhdGVkU2Vzc2lvbkNvdW50AQHzAAAvAD/z" +
           "AAAAAAf/////AQH/////AAAAABVgiQoCAAAAAAAcAAAAU2VjdXJpdHlSZWplY3RlZFNlc3Npb25Db3Vu" +
           "dAEB9AAALwA/9AAAAAAH/////wEB/////wAAAAAVYIkKAgAAAAAAFAAAAFJlamVjdGVkU2Vzc2lvbkNv" +
           "dW50AQH1AAAvAD/1AAAAAAf/////AQH/////AAAAABVgiQoCAAAAAAATAAAAU2Vzc2lvblRpbWVvdXRD" +
           "b3VudAEB9gAALwA/9gAAAAAH/////wEB/////wAAAAAVYIkKAgAAAAAAEQAAAFNlc3Npb25BYm9ydENv" +
           "dW50AQH3AAAvAD/3AAAAAAf/////AQH/////AAAAABVgiQoCAAAAAAAXAAAAUHVibGlzaGluZ0ludGVy" +
           "dmFsQ291bnQBAfgAAC8AP/gAAAAAB/////8BAf////8AAAAAFWCJCgIAAAAAABgAAABDdXJyZW50U3Vi" +
           "c2NyaXB0aW9uQ291bnQBAfkAAC8AP/kAAAAAB/////8BAf////8AAAAAFWCJCgIAAAAAABoAAABDdW11" +
           "bGF0ZWRTdWJzY3JpcHRpb25Db3VudAEB+gAALwA/+gAAAAAH/////wEB/////wAAAAAVYIkKAgAAAAAA" +
           "HQAAAFNlY3VyaXR5UmVqZWN0ZWRSZXF1ZXN0c0NvdW50AQH7AAAvAD/7AAAAAAf/////AQH/////AAAA" +
           "ABVgiQoCAAAAAAAVAAAAUmVqZWN0ZWRSZXF1ZXN0c0NvdW50AQH8AAAvAD/8AAAAAAf/////AQH/////" +
           "AAAAADVgiQoCAAAAAAAcAAAAU3Vic2NyaXB0aW9uRGlhZ25vc3RpY3NBcnJheQEB/gADAAAAADMAAABB" +
           "IGxpc3Qgb2YgZGlhZ25vc3RpY3MgZm9yIGVhY2ggYWN0aXZlIHN1YnNjcmlwdGlvbi4ALwEAewj+AAAA" +
           "AQBqAwEAAAABAf////8AAAAAJGCACgEAAAAAABoAAABTZXNzaW9uc0RpYWdub3N0aWNzU3VtbWFyeQEB" +
           "/wADAAAAACcAAABBIHN1bW1hcnkgb2Ygc2Vzc2lvbiBsZXZlbCBkaWFnbm9zdGljcy4ALwEA6gf/AAAA" +
           "/////wIAAAA1YIkKAgAAAAAAFwAAAFNlc3Npb25EaWFnbm9zdGljc0FycmF5AQEAAQMAAAAALgAAAEEg" +
           "bGlzdCBvZiBkaWFnbm9zdGljcyBmb3IgZWFjaCBhY3RpdmUgc2Vzc2lvbi4ALwEAlAgAAQAAAQBhAwEA" +
           "AAABAf////8AAAAANWCJCgIAAAAAAB8AAABTZXNzaW9uU2VjdXJpdHlEaWFnbm9zdGljc0FycmF5AQEB" +
           "AQMAAAAAPwAAAEEgbGlzdCBvZiBzZWN1cml0eSByZWxhdGVkIGRpYWdub3N0aWNzIGZvciBlYWNoIGFj" +
           "dGl2ZSBzZXNzaW9uLgAvAQDDCAEBAAABAGQDAQAAAAEB/////wAAAAA1YIkKAgAAAAAACwAAAEVuYWJs" +
           "ZWRGbGFnAQECAQMAAAAALgAAAElmIFRSVUUgdGhlIGRpYWdub3N0aWNzIGNvbGxlY3Rpb24gaXMgZW5h" +
           "YmxlZC4ALgBEAgEAAAAB/////wMD/////wAAAAAkYIAKAQAAAAAAEAAAAFZlbmRvclNlcnZlckluZm8B" +
           "AQMBAwAAAAAqAAAAU2VydmVyIGluZm9ybWF0aW9uIHByb3ZpZGVkIGJ5IHRoZSB2ZW5kb3IuAC8BAPEH" +
           "AwEAAP////8AAAAAJGCACgEAAAAAABAAAABTZXJ2ZXJSZWR1bmRhbmN5AQEEAQMAAAAANAAAAERlc2Ny" +
           "aWJlcyB0aGUgcmVkdW5kYW5jeSBjYXBhYmlsaXRpZXMgb2YgdGhlIHNlcnZlci4ALwEA8gcEAQAA////" +
           "/wEAAAA1YIkKAgAAAAAAEQAAAFJlZHVuZGFuY3lTdXBwb3J0AQEFAQMAAAAAPgAAAEluZGljYXRlcyB3" +
           "aGF0IHN0eWxlIG9mIHJlZHVuZGFuY3kgaXMgc3VwcG9ydGVkIGJ5IHRoZSBzZXJ2ZXIuAC4ARAUBAAAB" +
           "AFMD/////wEB/////wAAAAAVYMkKAgAAABEAAABCcm93c2VOYW1lNG5vZGU2NgEADQAAAENoaWxkUHJv" +
           "cGVydHkBATUBAC4ARDUBAAAAFf////8BAQEAAAABAcMAAQEBIQEAAAAAFWDJCgIAAAAOAAAATmFtZU5v" +
           "dFNldDExMDkBABMAAABCcm93c2VOYW1lNG5vZGUxMTA5AQEhAQAvAQBACSEBAAAAGv////8BAQEAAAAB" +
           "AcMAAAEBNQEBAAAAFWCJCgIAAAAAAAcAAABFVVJhbmdlAQElAQAuAEQlAQAAAQB0A/////8BAf////8A" +
           "AAAABGGCCgQAAAABAAsAAABDaGlsZE1ldGhvZAEBJwEALwEBJwEnAQAAAQH/////AgAAABVgqQoCAAAA" +
           "AAAOAAAASW5wdXRBcmd1bWVudHMBASsBAC4ARCsBAACWAgAAAAEAKgEBFQAAAAYAAABJbnB1dDEABv//" +
           "//8AAAAAAAEAKgEBFQAAAAYAAABJbnB1dDIABv////8AAAAAAAEAKAEBAAAAAQH/////AAAAABVgqQoC" +
           "AAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQEsAQAuAEQsAQAAlgEAAAABACoBARYAAAAHAAAAT3V0cHV0" +
           "MQAG/////wAAAAAAAQAoAQEAAAABAf////8AAAAABGGCCgQAAAABABMAAABOb25FeGVjdXRhYmxlTWV0" +
           "aG9kAQEyAQAvAQEyATIBAAABAf////8AAAAA";
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
        /// A description for the ChildMethodComplexObjectType Method.
        /// </summary>
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

        /// <summary>
        /// A description for the NonExecutableMethodComplexObjectType Method.
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
           "AQH/////AgAAABVgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBATABAC4ARDABAACWAgAAAAEAKgEB" +
           "FQAAAAYAAABJbnB1dDEABv////8AAAAAAAEAKgEBFQAAAAYAAABJbnB1dDIABv////8AAAAAAAEAKAEB" +
           "AAAAAQH/////AAAAABVgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQExAQAuAEQxAQAAlgEAAAAB" +
           "ACoBARYAAAAHAAAAT3V0cHV0MQAG/////wAAAAAAAQAoAQEAAAABAf////8AAAAA";
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
        /// <param name="context">The current context.</param>
        /// <param name="objectId">The id of the object.</param>
        /// <param name="inputArguments">The input arguments which have been already validated.</param>
        /// <param name="outputArguments">The output arguments which have initialized with thier default values.</param>
        /// <returns></returns>
        protected override ServiceResult Call(
            ISystemContext context,
            NodeId objectId,
            IList<object> inputArguments,
            IList<object> outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(context, objectId, inputArguments, outputArguments);
            }

            ServiceResult result = null;

            int input1 = (int)inputArguments[0];
            int input2 = (int)inputArguments[1];

            int output1 = (int)outputArguments[0];

            if (OnCall != null)
            {
                result = OnCall(
                    context,
                    this,
                    objectId,
                    input1,
                    input2,
                    ref output1);
            }

            outputArguments[0] = output1;

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
        ISystemContext context,
        MethodState method,
        NodeId objectId,
        int input1,
        int input2,
        ref int output1);
    #endif
    #endregion
}