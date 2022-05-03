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
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_AddRole Method.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_RoleSet_AddRole = 3;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole Method.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole = 6;

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

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole Method.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole = 10;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole Method.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole = 13;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ChildMethod Method.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ChildMethod = 409;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_AddRole Method.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_RoleSet_AddRole = 17;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_RemoveRole Method.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_RoleSet_RemoveRole = 20;

        /// <summary>
        /// The identifier for the NameNotSet6_ChildMethod Method.
        /// </summary>
        public const uint NameNotSet6_ChildMethod = 512;

        /// <summary>
        /// The identifier for the NameNotSet6_NonExecutableMethod Method.
        /// </summary>
        public const uint NameNotSet6_NonExecutableMethod = 515;
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
        /// The identifier for the ComplexObjectType_ServerCapabilities Object.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities = 214;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_ModellingRules Object.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_ModellingRules = 237;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_AggregateFunctions Object.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_AggregateFunctions = 238;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary Object.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary = 255;

        /// <summary>
        /// The identifier for the ComplexObjectType_ChildObject Object.
        /// </summary>
        public const uint ComplexObjectType_ChildObject = 308;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_ModellingRules Object.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_ModellingRules = 351;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_AggregateFunctions Object.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_AggregateFunctions = 352;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary Object.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary = 369;

        /// <summary>
        /// The identifier for the NameNotSet6 Object.
        /// </summary>
        public const uint NameNotSet6 = 413;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities Object.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities = 431;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_ModellingRules Object.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_ModellingRules = 454;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_AggregateFunctions Object.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_AggregateFunctions = 455;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics Object.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics = 456;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary Object.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary = 472;

        /// <summary>
        /// The identifier for the NameNotSet6_VendorServerInfo Object.
        /// </summary>
        public const uint NameNotSet6_VendorServerInfo = 476;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerRedundancy Object.
        /// </summary>
        public const uint NameNotSet6_ServerRedundancy = 477;
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
        /// The identifier for the ComplexObjectType_ServerStatus_StartTime Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_StartTime = 200;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_CurrentTime Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_CurrentTime = 201;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_State Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_State = 202;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_BuildInfo = 203;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_ProductUri Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_BuildInfo_ProductUri = 204;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_ManufacturerName Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_BuildInfo_ManufacturerName = 205;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_ProductName Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_BuildInfo_ProductName = 206;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion = 207;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_BuildNumber Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_BuildInfo_BuildNumber = 208;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_BuildDate Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_BuildInfo_BuildDate = 209;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_SecondsTillShutdown Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_SecondsTillShutdown = 210;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_ShutdownReason Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerStatus_ShutdownReason = 211;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_ServerProfileArray Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_ServerProfileArray = 215;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_LocaleIdArray Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_LocaleIdArray = 216;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_MinSupportedSampleRate Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_MinSupportedSampleRate = 217;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints = 218;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints = 219;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints = 220;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_SoftwareCertificates Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_SoftwareCertificates = 221;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments = 4;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments = 5;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments = 7;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary = 240;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount = 241;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount = 242;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount = 243;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount = 244;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount = 245;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount = 246;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount = 247;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount = 248;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount = 249;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount = 250;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount = 251;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount = 252;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray = 254;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray = 256;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray = 257;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_EnabledFlag Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerDiagnostics_EnabledFlag = 258;

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerRedundancy_RedundancySupport Variable.
        /// </summary>
        public const uint ComplexObjectType_ServerRedundancy_RedundancySupport = 261;

        /// <summary>
        /// The identifier for the ComplexObjectType_GetMonitoredItems_InputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_GetMonitoredItems_InputArguments = 285;

        /// <summary>
        /// The identifier for the ComplexObjectType_GetMonitoredItems_OutputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_GetMonitoredItems_OutputArguments = 286;

        /// <summary>
        /// The identifier for the ComplexObjectType_ResendData_InputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_ResendData_InputArguments = 526;

        /// <summary>
        /// The identifier for the ComplexObjectType_SetSubscriptionDurable_InputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_SetSubscriptionDurable_InputArguments = 528;

        /// <summary>
        /// The identifier for the ComplexObjectType_SetSubscriptionDurable_OutputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_SetSubscriptionDurable_OutputArguments = 529;

        /// <summary>
        /// The identifier for the ComplexObjectType_RequestServerStateChange_InputArguments Variable.
        /// </summary>
        public const uint ComplexObjectType_RequestServerStateChange_InputArguments = 531;

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

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_StartTime Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_StartTime = 314;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_CurrentTime Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_CurrentTime = 315;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_State Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_State = 316;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_BuildInfo = 317;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductUri Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductUri = 318;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_ManufacturerName Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_BuildInfo_ManufacturerName = 319;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductName Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductName = 320;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion = 321;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildNumber Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildNumber = 322;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildDate Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildDate = 323;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_SecondsTillShutdown Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_SecondsTillShutdown = 324;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_ShutdownReason Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerStatus_ShutdownReason = 325;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_ServerProfileArray Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_ServerProfileArray = 329;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_LocaleIdArray Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_LocaleIdArray = 330;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_MinSupportedSampleRate Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_MinSupportedSampleRate = 331;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints = 332;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints = 333;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints = 334;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_SoftwareCertificates Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_SoftwareCertificates = 335;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments = 11;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments = 12;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments = 14;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary = 354;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount = 355;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount = 356;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount = 357;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount = 358;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount = 359;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount = 360;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount = 361;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount = 362;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount = 363;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount = 364;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount = 365;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount = 366;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray = 368;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray = 370;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray = 371;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_EnabledFlag Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerDiagnostics_EnabledFlag = 372;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerRedundancy_RedundancySupport Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ServerRedundancy_RedundancySupport = 375;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_GetMonitoredItems_InputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_GetMonitoredItems_InputArguments = 399;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_GetMonitoredItems_OutputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_GetMonitoredItems_OutputArguments = 400;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ResendData_InputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ResendData_InputArguments = 542;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_SetSubscriptionDurable_InputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_SetSubscriptionDurable_InputArguments = 544;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_SetSubscriptionDurable_OutputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_SetSubscriptionDurable_OutputArguments = 545;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_RequestServerStateChange_InputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_RequestServerStateChange_InputArguments = 547;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_BrowseName4node66 Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_BrowseName4node66 = 402;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_NameNotSet1109 Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_NameNotSet1109 = 403;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_NameNotSet1109_EURange Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_NameNotSet1109_EURange = 407;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ChildMethod_InputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ChildMethod_InputArguments = 410;

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ChildMethod_OutputArguments Variable.
        /// </summary>
        public const uint DerivedFromComplexObjectType_ChildMethod_OutputArguments = 411;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerArray Variable.
        /// </summary>
        public const uint NameNotSet6_ServerArray = 414;

        /// <summary>
        /// The identifier for the NameNotSet6_NamespaceArray Variable.
        /// </summary>
        public const uint NameNotSet6_NamespaceArray = 415;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus = 416;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_StartTime Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_StartTime = 417;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_CurrentTime Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_CurrentTime = 418;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_State Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_State = 419;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_BuildInfo = 420;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_ProductUri Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_BuildInfo_ProductUri = 421;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_ManufacturerName Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_BuildInfo_ManufacturerName = 422;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_ProductName Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_BuildInfo_ProductName = 423;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_SoftwareVersion Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_BuildInfo_SoftwareVersion = 424;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_BuildNumber Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_BuildInfo_BuildNumber = 425;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_BuildDate Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_BuildInfo_BuildDate = 426;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_SecondsTillShutdown Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_SecondsTillShutdown = 427;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_ShutdownReason Variable.
        /// </summary>
        public const uint NameNotSet6_ServerStatus_ShutdownReason = 428;

        /// <summary>
        /// The identifier for the NameNotSet6_ServiceLevel Variable.
        /// </summary>
        public const uint NameNotSet6_ServiceLevel = 429;

        /// <summary>
        /// The identifier for the NameNotSet6_Auditing Variable.
        /// </summary>
        public const uint NameNotSet6_Auditing = 430;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_ServerProfileArray Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_ServerProfileArray = 432;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_LocaleIdArray Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_LocaleIdArray = 433;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_MinSupportedSampleRate Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_MinSupportedSampleRate = 434;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_MaxBrowseContinuationPoints Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_MaxBrowseContinuationPoints = 435;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_MaxQueryContinuationPoints Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_MaxQueryContinuationPoints = 436;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_MaxHistoryContinuationPoints Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_MaxHistoryContinuationPoints = 437;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_SoftwareCertificates Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_SoftwareCertificates = 438;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_AddRole_InputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_RoleSet_AddRole_InputArguments = 18;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_AddRole_OutputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_RoleSet_AddRole_OutputArguments = 19;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_RemoveRole_InputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_ServerCapabilities_RoleSet_RemoveRole_InputArguments = 21;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary = 457;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount = 458;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount = 459;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount = 460;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount = 461;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount = 462;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount = 463;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount = 464;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount = 465;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount = 466;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount = 467;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount = 468;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount = 469;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_SubscriptionDiagnosticsArray Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_SubscriptionDiagnosticsArray = 471;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray = 473;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray = 474;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_EnabledFlag Variable.
        /// </summary>
        public const uint NameNotSet6_ServerDiagnostics_EnabledFlag = 475;

        /// <summary>
        /// The identifier for the NameNotSet6_ServerRedundancy_RedundancySupport Variable.
        /// </summary>
        public const uint NameNotSet6_ServerRedundancy_RedundancySupport = 478;

        /// <summary>
        /// The identifier for the NameNotSet6_GetMonitoredItems_InputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_GetMonitoredItems_InputArguments = 502;

        /// <summary>
        /// The identifier for the NameNotSet6_GetMonitoredItems_OutputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_GetMonitoredItems_OutputArguments = 503;

        /// <summary>
        /// The identifier for the NameNotSet6_ResendData_InputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_ResendData_InputArguments = 558;

        /// <summary>
        /// The identifier for the NameNotSet6_SetSubscriptionDurable_InputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_SetSubscriptionDurable_InputArguments = 560;

        /// <summary>
        /// The identifier for the NameNotSet6_SetSubscriptionDurable_OutputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_SetSubscriptionDurable_OutputArguments = 561;

        /// <summary>
        /// The identifier for the NameNotSet6_RequestServerStateChange_InputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_RequestServerStateChange_InputArguments = 563;

        /// <summary>
        /// The identifier for the NameNotSet6_BrowseName4node66 Variable.
        /// </summary>
        public const uint NameNotSet6_BrowseName4node66 = 505;

        /// <summary>
        /// The identifier for the NameNotSet6_NameNotSet1109 Variable.
        /// </summary>
        public const uint NameNotSet6_NameNotSet1109 = 506;

        /// <summary>
        /// The identifier for the NameNotSet6_NameNotSet1109_EURange Variable.
        /// </summary>
        public const uint NameNotSet6_NameNotSet1109_EURange = 510;

        /// <summary>
        /// The identifier for the NameNotSet6_ChildMethod_InputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_ChildMethod_InputArguments = 513;

        /// <summary>
        /// The identifier for the NameNotSet6_ChildMethod_OutputArguments Variable.
        /// </summary>
        public const uint NameNotSet6_ChildMethod_OutputArguments = 514;
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
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_AddRole Method.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_RoleSet_AddRole = new ExpandedNodeId(ObjectTypeTest.Methods.ComplexObjectType_ServerCapabilities_RoleSet_AddRole, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole Method.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole = new ExpandedNodeId(ObjectTypeTest.Methods.ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole, ObjectTypeTest.Namespaces.cas);

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

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole Method.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole = new ExpandedNodeId(ObjectTypeTest.Methods.DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole Method.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole = new ExpandedNodeId(ObjectTypeTest.Methods.DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ChildMethod Method.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ChildMethod = new ExpandedNodeId(ObjectTypeTest.Methods.DerivedFromComplexObjectType_ChildMethod, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_AddRole Method.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_RoleSet_AddRole = new ExpandedNodeId(ObjectTypeTest.Methods.NameNotSet6_ServerCapabilities_RoleSet_AddRole, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_RemoveRole Method.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_RoleSet_RemoveRole = new ExpandedNodeId(ObjectTypeTest.Methods.NameNotSet6_ServerCapabilities_RoleSet_RemoveRole, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ChildMethod Method.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ChildMethod = new ExpandedNodeId(ObjectTypeTest.Methods.NameNotSet6_ChildMethod, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_NonExecutableMethod Method.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_NonExecutableMethod = new ExpandedNodeId(ObjectTypeTest.Methods.NameNotSet6_NonExecutableMethod, ObjectTypeTest.Namespaces.cas);
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
        /// The identifier for the ComplexObjectType_ServerCapabilities Object.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities = new ExpandedNodeId(ObjectTypeTest.Objects.ComplexObjectType_ServerCapabilities, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_ModellingRules Object.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_ModellingRules = new ExpandedNodeId(ObjectTypeTest.Objects.ComplexObjectType_ServerCapabilities_ModellingRules, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_AggregateFunctions Object.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_AggregateFunctions = new ExpandedNodeId(ObjectTypeTest.Objects.ComplexObjectType_ServerCapabilities_AggregateFunctions, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary Object.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary = new ExpandedNodeId(ObjectTypeTest.Objects.ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ChildObject Object.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ChildObject = new ExpandedNodeId(ObjectTypeTest.Objects.ComplexObjectType_ChildObject, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_ModellingRules Object.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_ModellingRules = new ExpandedNodeId(ObjectTypeTest.Objects.DerivedFromComplexObjectType_ServerCapabilities_ModellingRules, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_AggregateFunctions Object.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_AggregateFunctions = new ExpandedNodeId(ObjectTypeTest.Objects.DerivedFromComplexObjectType_ServerCapabilities_AggregateFunctions, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary Object.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary = new ExpandedNodeId(ObjectTypeTest.Objects.DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6 Object.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6 = new ExpandedNodeId(ObjectTypeTest.Objects.NameNotSet6, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities Object.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities = new ExpandedNodeId(ObjectTypeTest.Objects.NameNotSet6_ServerCapabilities, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_ModellingRules Object.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_ModellingRules = new ExpandedNodeId(ObjectTypeTest.Objects.NameNotSet6_ServerCapabilities_ModellingRules, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_AggregateFunctions Object.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_AggregateFunctions = new ExpandedNodeId(ObjectTypeTest.Objects.NameNotSet6_ServerCapabilities_AggregateFunctions, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics Object.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics = new ExpandedNodeId(ObjectTypeTest.Objects.NameNotSet6_ServerDiagnostics, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary Object.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary = new ExpandedNodeId(ObjectTypeTest.Objects.NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_VendorServerInfo Object.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_VendorServerInfo = new ExpandedNodeId(ObjectTypeTest.Objects.NameNotSet6_VendorServerInfo, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerRedundancy Object.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerRedundancy = new ExpandedNodeId(ObjectTypeTest.Objects.NameNotSet6_ServerRedundancy, ObjectTypeTest.Namespaces.cas);
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
        /// The identifier for the ComplexObjectType_ServerStatus_StartTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_StartTime = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_StartTime, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_CurrentTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_CurrentTime = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_CurrentTime, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_State Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_State = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_State, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_BuildInfo = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_BuildInfo, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_ProductUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_BuildInfo_ProductUri = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_BuildInfo_ProductUri, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_ManufacturerName Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_BuildInfo_ManufacturerName = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_BuildInfo_ManufacturerName, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_ProductName Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_BuildInfo_ProductName = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_BuildInfo_ProductName, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_BuildNumber Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_BuildInfo_BuildNumber = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_BuildInfo_BuildNumber, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_BuildInfo_BuildDate Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_BuildInfo_BuildDate = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_BuildInfo_BuildDate, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_SecondsTillShutdown Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_SecondsTillShutdown = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_SecondsTillShutdown, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerStatus_ShutdownReason Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerStatus_ShutdownReason = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerStatus_ShutdownReason, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_ServerProfileArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_ServerProfileArray = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_ServerProfileArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_LocaleIdArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_LocaleIdArray = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_LocaleIdArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_MinSupportedSampleRate Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_MinSupportedSampleRate = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_MinSupportedSampleRate, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_SoftwareCertificates Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_SoftwareCertificates = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_SoftwareCertificates, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerDiagnostics_EnabledFlag Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerDiagnostics_EnabledFlag = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerDiagnostics_EnabledFlag, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ServerRedundancy_RedundancySupport Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ServerRedundancy_RedundancySupport = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ServerRedundancy_RedundancySupport, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_GetMonitoredItems_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_GetMonitoredItems_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_GetMonitoredItems_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_GetMonitoredItems_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_GetMonitoredItems_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_GetMonitoredItems_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_ResendData_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_ResendData_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_ResendData_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_SetSubscriptionDurable_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_SetSubscriptionDurable_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_SetSubscriptionDurable_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_SetSubscriptionDurable_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_SetSubscriptionDurable_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_SetSubscriptionDurable_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the ComplexObjectType_RequestServerStateChange_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ComplexObjectType_RequestServerStateChange_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.ComplexObjectType_RequestServerStateChange_InputArguments, ObjectTypeTest.Namespaces.cas);

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

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_StartTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_StartTime = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_StartTime, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_CurrentTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_CurrentTime = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_CurrentTime, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_State Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_State = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_State, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_BuildInfo = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_BuildInfo, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductUri = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductUri, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_ManufacturerName Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_BuildInfo_ManufacturerName = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_BuildInfo_ManufacturerName, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductName Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductName = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_BuildInfo_ProductName, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_BuildInfo_SoftwareVersion, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildNumber Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildNumber = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildNumber, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildDate Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildDate = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_BuildInfo_BuildDate, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_SecondsTillShutdown Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_SecondsTillShutdown = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_SecondsTillShutdown, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerStatus_ShutdownReason Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerStatus_ShutdownReason = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerStatus_ShutdownReason, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_ServerProfileArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_ServerProfileArray = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_ServerProfileArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_LocaleIdArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_LocaleIdArray = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_LocaleIdArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_MinSupportedSampleRate Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_MinSupportedSampleRate = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_MinSupportedSampleRate, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_MaxBrowseContinuationPoints, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_MaxQueryContinuationPoints, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_MaxHistoryContinuationPoints, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_SoftwareCertificates Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_SoftwareCertificates = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_SoftwareCertificates, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_RoleSet_AddRole_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerCapabilities_RoleSet_RemoveRole_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_SubscriptionDiagnosticsArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerDiagnostics_EnabledFlag Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerDiagnostics_EnabledFlag = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerDiagnostics_EnabledFlag, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ServerRedundancy_RedundancySupport Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ServerRedundancy_RedundancySupport = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ServerRedundancy_RedundancySupport, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_GetMonitoredItems_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_GetMonitoredItems_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_GetMonitoredItems_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_GetMonitoredItems_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_GetMonitoredItems_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_GetMonitoredItems_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ResendData_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ResendData_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ResendData_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_SetSubscriptionDurable_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_SetSubscriptionDurable_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_SetSubscriptionDurable_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_SetSubscriptionDurable_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_SetSubscriptionDurable_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_SetSubscriptionDurable_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_RequestServerStateChange_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_RequestServerStateChange_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_RequestServerStateChange_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_BrowseName4node66 Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_BrowseName4node66 = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_BrowseName4node66, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_NameNotSet1109 Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_NameNotSet1109 = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_NameNotSet1109, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_NameNotSet1109_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_NameNotSet1109_EURange = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_NameNotSet1109_EURange, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ChildMethod_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ChildMethod_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ChildMethod_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the DerivedFromComplexObjectType_ChildMethod_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId DerivedFromComplexObjectType_ChildMethod_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.DerivedFromComplexObjectType_ChildMethod_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerArray = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_NamespaceArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_NamespaceArray = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_NamespaceArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_StartTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_StartTime = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_StartTime, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_CurrentTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_CurrentTime = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_CurrentTime, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_State Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_State = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_State, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_BuildInfo = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_BuildInfo, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_ProductUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_BuildInfo_ProductUri = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_BuildInfo_ProductUri, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_ManufacturerName Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_BuildInfo_ManufacturerName = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_BuildInfo_ManufacturerName, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_ProductName Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_BuildInfo_ProductName = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_BuildInfo_ProductName, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_SoftwareVersion Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_BuildInfo_SoftwareVersion = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_BuildInfo_SoftwareVersion, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_BuildNumber Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_BuildInfo_BuildNumber = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_BuildInfo_BuildNumber, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_BuildInfo_BuildDate Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_BuildInfo_BuildDate = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_BuildInfo_BuildDate, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_SecondsTillShutdown Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_SecondsTillShutdown = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_SecondsTillShutdown, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerStatus_ShutdownReason Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerStatus_ShutdownReason = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerStatus_ShutdownReason, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServiceLevel Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServiceLevel = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServiceLevel, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_Auditing Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_Auditing = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_Auditing, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_ServerProfileArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_ServerProfileArray = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_ServerProfileArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_LocaleIdArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_LocaleIdArray = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_LocaleIdArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_MinSupportedSampleRate Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_MinSupportedSampleRate = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_MinSupportedSampleRate, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_MaxBrowseContinuationPoints Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_MaxBrowseContinuationPoints = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_MaxBrowseContinuationPoints, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_MaxQueryContinuationPoints Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_MaxQueryContinuationPoints = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_MaxQueryContinuationPoints, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_MaxHistoryContinuationPoints Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_MaxHistoryContinuationPoints = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_MaxHistoryContinuationPoints, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_SoftwareCertificates Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_SoftwareCertificates = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_SoftwareCertificates, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_AddRole_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_RoleSet_AddRole_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_RoleSet_AddRole_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_AddRole_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_RoleSet_AddRole_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_RoleSet_AddRole_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerCapabilities_RoleSet_RemoveRole_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerCapabilities_RoleSet_RemoveRole_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerCapabilities_RoleSet_RemoveRole_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_ServerViewCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedSessionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionTimeoutCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SessionAbortCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_PublishingIntervalCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CurrentSubscriptionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_CumulatedSubscriptionCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_SecurityRejectedRequestsCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_ServerDiagnosticsSummary_RejectedRequestsCount, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_SubscriptionDiagnosticsArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_SubscriptionDiagnosticsArray = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_SubscriptionDiagnosticsArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionDiagnosticsArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_SessionsDiagnosticsSummary_SessionSecurityDiagnosticsArray, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerDiagnostics_EnabledFlag Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerDiagnostics_EnabledFlag = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerDiagnostics_EnabledFlag, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ServerRedundancy_RedundancySupport Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ServerRedundancy_RedundancySupport = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ServerRedundancy_RedundancySupport, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_GetMonitoredItems_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_GetMonitoredItems_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_GetMonitoredItems_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_GetMonitoredItems_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_GetMonitoredItems_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_GetMonitoredItems_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ResendData_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ResendData_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ResendData_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_SetSubscriptionDurable_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_SetSubscriptionDurable_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_SetSubscriptionDurable_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_SetSubscriptionDurable_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_SetSubscriptionDurable_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_SetSubscriptionDurable_OutputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_RequestServerStateChange_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_RequestServerStateChange_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_RequestServerStateChange_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_BrowseName4node66 Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_BrowseName4node66 = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_BrowseName4node66, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_NameNotSet1109 Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_NameNotSet1109 = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_NameNotSet1109, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_NameNotSet1109_EURange Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_NameNotSet1109_EURange = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_NameNotSet1109_EURange, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ChildMethod_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ChildMethod_InputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ChildMethod_InputArguments, ObjectTypeTest.Namespaces.cas);

        /// <summary>
        /// The identifier for the NameNotSet6_ChildMethod_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId NameNotSet6_ChildMethod_OutputArguments = new ExpandedNodeId(ObjectTypeTest.Variables.NameNotSet6_ChildMethod_OutputArguments, ObjectTypeTest.Namespaces.cas);
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
        /// The BrowseName for the DerivedFromComplexObjectType component.
        /// </summary>
        public const string DerivedFromComplexObjectType = "DerivedFromComplexObjectType";

        /// <summary>
        /// The BrowseName for the FlowTo component.
        /// </summary>
        public const string FlowTo = "FlowTo";

        /// <summary>
        /// The BrowseName for the NameNotSet1109 component.
        /// </summary>
        public const string NameNotSet1109 = "BrowseName4node1109";

        /// <summary>
        /// The BrowseName for the NameNotSet6 component.
        /// </summary>
        public const string NameNotSet6 = "NameNotSet6";

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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
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
}