//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UAOOI.Common.Infrastructure.Serializers;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UAModelDesignExport.Instrumentation;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  [TestClass]
  [DeploymentItem(@"Models\", @"Models\")]
  public class NodeSetIntegrationTest
  {
    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    //public TestContext TestContext { get; set; }

    #region TestMethod

    [TestMethod]
    public void FileNotFoundTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"sajlajsjljjjjk.sasa.sasa.sasa");
      Assert.IsFalse(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext addressSpace = new Instrumentation.TracedAddressSpaceContext())
      {
        Assert.ThrowsException<FileNotFoundException>(() => addressSpace.CreateInstance(_testDataFileInfo, String.Empty));
        _testDataFileInfo = new FileInfo(@"Models\ReferenceTest\ReferenceTest.NodeSet.xml");
        Assert.IsTrue(_testDataFileInfo.Exists);
        Assert.ThrowsException<InvalidOperationException>(() => addressSpace.CreateInstance(_testDataFileInfo, String.Empty));
      }
    }

    [TestMethod]
    public void UAReferenceTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"Models\ReferenceTest\ReferenceTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      ModelDesign _expected = XmlFile.ReadXmlFile<ModelDesign>(@"Models\ReferenceTest.asp.xml");
      List<TraceMessage> _trace = new List<TraceMessage>();
      string uri = "http://cas.eu/UA/CommServer/UnitTests/ReferenceTest";
      using (TracedAddressSpaceContext addressSpace = new Instrumentation.TracedAddressSpaceContext())
      {
        ModelDesign _actual = addressSpace.CreateInstance(_testDataFileInfo, uri);
        CompareModelDesign(_expected, _actual);
      }
    }

    [TestMethod]
    //TODO The exported model doesn't contain all nodes #653
    public void UAObjectTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"Models\ObjectTypeTest\ObjectTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      ModelDesign _expected = XmlFile.ReadXmlFile<ModelDesign>(@"Models\ObjectTypeTest.asp.xml");
      string uri = "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest";
      using (TracedAddressSpaceContext addressSpace = new Instrumentation.TracedAddressSpaceContext())
      {
        ModelDesign _actual = addressSpace.CreateInstance(_testDataFileInfo, uri);
        CompareModelDesign(_expected, _actual);
        Assert.AreEqual<int>(0, addressSpace.TraceList.Count);
        Assert.AreEqual<int>(3, _expected.Items.Length);
        CompareModelDesign(_expected, _actual);
      }
    }

    [TestMethod]
    public void UAVariableTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"Models\VariableTypeTest\VariableTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      ModelDesign _expected = XmlFile.ReadXmlFile<ModelDesign>(@"Models\VariableTypeTest.asp.xml");
      string uri = "http://cas.eu/UA/CommServer/UnitTests/VariableTypeTest";
      using (TracedAddressSpaceContext addressSpace = new Instrumentation.TracedAddressSpaceContext())
      {
        ModelDesign _actual = addressSpace.CreateInstance(_testDataFileInfo, uri);
        CompareModelDesign(_expected, _actual);
        Assert.AreEqual<int>(0, addressSpace.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(3, _expected.Items.Length);
        CompareModelDesign(_expected, _actual);
      }
    }

    [TestMethod]
    public void UADataTypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"Models\DataTypeTest\DataTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      ModelDesign _expected = XmlFile.ReadXmlFile<ModelDesign>(@"Models\DataTypeTest.asp.xml");
      string uri = "http://cas.eu/UA/CommServer/UnitTests/DataTypeTest";
      using (TracedAddressSpaceContext addressSpace = new Instrumentation.TracedAddressSpaceContext())
      {
        ModelDesign _actual = addressSpace.CreateInstance(_testDataFileInfo, uri);
        CompareModelDesign(_expected, _actual);
        Assert.AreEqual<int>(0, addressSpace.TraceList.Count);
        Assert.AreEqual<int>(4, _expected.Items.Length);
        Assert.AreEqual<int>(4, _actual.Items.Length);
        CompareModelDesign(_expected, _actual);
      }
    }

    #endregion TestMethod

    #region Test instrumentation

    #region ModelDesign
    //TODO UANodeSet prepare a semantic diff #565
    //TODO ModelDesign prepare a semantic diff #566

    private static void CompareModelDesign(ModelDesign expected, ModelDesign actual)
    {
      Assert.AreEqual<int>(expected.Items.Length, actual.Items.Length);
      Assert.AreEqual<string>(expected.TargetNamespace, actual.TargetNamespace);
      Dictionary<string, NodeDesign> _items = expected.Items.ToDictionary<NodeDesign, string>(x => x.SymbolicName.ToString());
      foreach (NodeDesign _node in actual.Items)
        CompareNode(_items[_node.SymbolicName.ToString()], _node);
    }

    public static void CompareNode(NodeDesign expected, NodeDesign actual)
    {
      if (expected.GetType() == typeof(ObjectTypeDesign))
        CompareObjectTypeDesign((ObjectTypeDesign)expected, (ObjectTypeDesign)actual);
      else if (expected.GetType() == typeof(VariableTypeDesign))
        CompareVariableTypeDesign((VariableTypeDesign)expected, (VariableTypeDesign)actual);
      else if (expected.GetType() == typeof(DataTypeDesign))
        CompareDataTypeDesign((DataTypeDesign)expected, (DataTypeDesign)actual);
      else if (expected.GetType() == typeof(ObjectDesign))
        CompareObjectDesign((ObjectDesign)expected, (ObjectDesign)actual);
      else if (expected.GetType() == typeof(PropertyDesign))
        ComparePropertyDesign((PropertyDesign)expected, (PropertyDesign)actual);
      else if (expected.GetType() == typeof(VariableDesign))
        CompareVariableDesign((VariableDesign)expected, (VariableDesign)actual);
      else if (expected.GetType() == typeof(MethodDesign))
        CompareMethodDesign((MethodDesign)expected, (MethodDesign)actual);
      else if (expected.GetType() == typeof(ReferenceTypeDesign))
        CompareReferenceTypeDesign((ReferenceTypeDesign)expected, (ReferenceTypeDesign)actual);
      else
        throw new NotImplementedException();
    }

    //Types
    private static void CompareDataTypeDesign(DataTypeDesign expected, DataTypeDesign actual)
    {
      Assert.AreEqual<bool>(expected.NoArraysAllowed, actual.NoArraysAllowed);
      Assert.IsFalse(actual.NotInAddressSpace);
      Compare(expected.Fields, actual.Fields);
      CompareTypeDesign(expected, actual);
    }

    private static void CompareReferenceTypeDesign(ReferenceTypeDesign expected, ReferenceTypeDesign actual)
    {
      CompareTypeDesign(expected, actual);
      UnitTestsExtensions.Compare(expected.InverseName, actual.InverseName);
      expected.SymmetricSpecified = expected.Symmetric; // false is default, if set must be ignored.
      Assert.AreEqual<bool>(expected.SymmetricSpecified, actual.SymmetricSpecified);
      Assert.AreEqual<bool>(expected.Symmetric, actual.Symmetric);
    }

    private static void CompareObjectTypeDesign(ObjectTypeDesign expected, ObjectTypeDesign actual)
    {
      CompareTypeDesign(expected, actual);
      Assert.IsFalse(expected.SupportsEventsSpecified, "Field not supported for types - should always be false");
      Assert.IsFalse(actual.SupportsEventsSpecified, "Field not supported for types - should always be false");
      Assert.IsFalse(expected.SupportsEvents, "Field not supported for types - should always be false");
      Assert.IsFalse(actual.SupportsEvents, "Field not supported for types - should always be false");
    }

    private static void CompareVariableTypeDesign(VariableTypeDesign expected, VariableTypeDesign actual)
    {
      CompareTypeDesign(expected, actual);
      Compare(expected.DefaultValue, actual.DefaultValue);
      Compare(expected.DataType, actual.DataType, "DataType");
      if (expected.ValueRankSpecified && expected.ValueRank == ValueRank.Scalar)
        expected.ValueRankSpecified = false;
      Assert.AreEqual<bool>(expected.ValueRankSpecified, actual.ValueRankSpecified);
      if (expected.ValueRankSpecified)
        Assert.AreEqual<ValueRank>(expected.ValueRank, actual.ValueRank);
      Assert.AreEqual<string>(expected.ArrayDimensions, actual.ArrayDimensions);
      //Not supported by the VariableType NodeClass
      Assert.IsFalse(expected.ExposesItsChildren);
      Assert.IsFalse(actual.ExposesItsChildren);
      Assert.IsFalse(expected.AccessLevelSpecified);
      Assert.IsFalse(actual.AccessLevelSpecified);
      Assert.IsFalse(expected.HistorizingSpecified);
      Assert.IsFalse(actual.HistorizingSpecified);
      Assert.IsFalse(expected.MinimumSamplingIntervalSpecified);
      Assert.IsFalse(actual.MinimumSamplingIntervalSpecified);
    }

    //Instances
    private static void CompareObjectDesign(ObjectDesign expected, ObjectDesign actual)
    {
      Assert.AreEqual<bool>(expected.SupportsEventsSpecified, actual.SupportsEventsSpecified);
      if (expected.SupportsEventsSpecified)
        Assert.AreEqual<bool>(expected.SupportsEvents, actual.SupportsEvents);
      CompareInstanceDesign(expected, actual);
    }

    private static void CompareVariableDesign(VariableDesign expected, VariableDesign actual)
    {
      Compare(expected.DefaultValue, actual.DefaultValue);
      Compare(expected.DataType, actual.DataType, "VariableDesign.DataType");
      if (expected.ValueRankSpecified && expected.ValueRank == ValueRank.Scalar)
        expected.ValueRankSpecified = false;
      Assert.AreEqual<bool>(expected.ValueRankSpecified, actual.ValueRankSpecified);
      if (expected.ValueRankSpecified)
        Assert.AreEqual<ValueRank>(expected.ValueRank, actual.ValueRank);
      Assert.AreEqual<string>(expected.ArrayDimensions, actual.ArrayDimensions);
      Assert.AreEqual<bool>(expected.AccessLevelSpecified, actual.AccessLevelSpecified, actual.SymbolicName.ToString());
      if (expected.AccessLevelSpecified)
        Assert.AreEqual<AccessLevel>(expected.AccessLevel, actual.AccessLevel);
      Assert.AreEqual<bool>(expected.MinimumSamplingIntervalSpecified, actual.MinimumSamplingIntervalSpecified);
      if (expected.MinimumSamplingIntervalSpecified)
        Assert.AreEqual<int>(expected.MinimumSamplingInterval, actual.MinimumSamplingInterval);
      Assert.AreEqual<bool>(expected.HistorizingSpecified, actual.HistorizingSpecified);
      if (expected.HistorizingSpecified)
        Assert.AreEqual<bool>(expected.Historizing, actual.Historizing);
      CompareInstanceDesign(expected, actual);
    }

    private static void ComparePropertyDesign(PropertyDesign expected, PropertyDesign actual)
    {
      Assert.IsNotNull(expected);
      Assert.IsNotNull(actual);
      CompareVariableDesign((VariableDesign)expected, (VariableDesign)actual);
    }

    private static void CompareMethodDesign(MethodDesign expected, MethodDesign actual)
    {
      Compare(expected.InputArguments, actual.InputArguments);
      Compare(expected.OutputArguments, actual.OutputArguments);
      Assert.IsFalse(actual.NonExecutableSpecified); //is not supported - cannot be recovered from NodeSet
      Assert.IsFalse(actual.NonExecutable);
      CompareInstanceDesign(expected, actual);
    }

    //base types
    private static void CompareTypeDesign(TypeDesign expected, TypeDesign actual)
    {
      CompareNodeDesign(expected, actual);
      if (expected.BaseType == null && actual.BaseType == null)
        return;
      Assert.IsNotNull(expected.BaseType);
      Assert.IsNotNull(actual.BaseType);
      Assert.AreEqual<string>(expected.BaseType.ToString(), actual.BaseType.ToString());
    }

    private static void CompareInstanceDesign(InstanceDesign expected, InstanceDesign actual)
    {
      Assert.IsTrue(expected.GetType() == actual.GetType());
      Compare(expected.ReferenceType, actual.ReferenceType, "InstanceDesign.ReferenceType");
      Compare(expected.TypeDefinition, actual.TypeDefinition, "InstanceDesign.TypeDefinition");
      Assert.AreEqual<bool>(expected.ModellingRuleSpecified, actual.ModellingRuleSpecified, actual.SymbolicName.ToString());
      if (expected.ModellingRuleSpecified)
        Assert.AreEqual<ModellingRule>(expected.ModellingRule, actual.ModellingRule);
      //test base type
      CompareNodeDesign(expected, actual);
      //Not supported attributes
      Assert.IsNull(actual.Declaration, "InstanceDesign.Declaration");
      Assert.AreEqual<uint>(0, actual.MinCardinality);
      Assert.AreEqual<uint>(0, actual.MaxCardinality);
      Assert.IsFalse(actual.PreserveDefaultAttributes);
    }

    private static void CompareNodeDesign(NodeDesign expected, NodeDesign actual)
    {
      Assert.IsTrue(expected.BrowseName.AreEqual(actual.BrowseName));
      UnitTestsExtensions.Compare(expected.DisplayName, actual.DisplayName);
      UnitTestsExtensions.Compare(expected.Description, actual.Description);
      CompareListOfChildren(expected.Children, actual.Children);
      Compare(expected.References, actual.References);
      Compare(expected.SymbolicName, actual.SymbolicName, "NodeDesign.SymbolicName");
      Compare(expected.SymbolicId, actual.SymbolicId, "NodeDesign.SymbolicId");
      Assert.AreEqual<bool>(expected.IsDeclaration, actual.IsDeclaration);
      Assert.AreEqual<uint>(expected.NumericId, actual.NumericId);
      Assert.AreEqual<bool>(expected.NumericIdSpecified, actual.NumericIdSpecified);
      Assert.AreEqual<string>(expected.StringId, actual.StringId);
      Assert.AreEqual<uint>(expected.WriteAccess, actual.WriteAccess);
      Assert.AreEqual<string>(expected.StringId, actual.StringId);
      Assert.AreEqual<uint>(expected.PartNo, actual.PartNo);
    }

    #endregion ModelDesign

    #region private helper

    /// <summary>
    /// Compares the parameters of a method.
    /// </summary>
    /// <remarks>
    /// ModelCompiler doesn't generate parameters if TypeDefinition for the method is not set.
    /// TypeDefinition is not defined in the specification, but has to refer to a method defined top most level.
    /// The ModelDesign contains parameters but the UANodeSet doesn't have.
    /// </remarks>
    /// <param name="expected">The expected.</param>
    /// <param name="actual">The actual.</param>
    private static void Compare(Parameter[] expected, Parameter[] actual)
    {
      if (expected == null || actual == null)
        return;
      if (expected.Length != actual.Length)
        return;
      for (int i = 0; i < expected.Length; i++)
      {
        Compare(expected[i].DataType, actual[i].DataType, "Parameter.DataType");
        UnitTestsExtensions.Compare(expected[i].Description, actual[i].Description);
        Assert.AreEqual<bool>(expected[i].IdentifierSpecified, actual[i].IdentifierSpecified);
        if (expected[i].IdentifierSpecified)
          Assert.AreEqual<int>(expected[i].Identifier, actual[i].Identifier);
        Assert.AreEqual<string>(expected[i].Name, actual[i].Name);
        Assert.AreEqual<ValueRank>(expected[i].ValueRank, actual[i].ValueRank);
      }
    }

    private static void CompareListOfChildren(ListOfChildren expected, ListOfChildren actual)
    {
      if (expected == null && actual == null)
        return;
      Assert.IsNotNull(expected);
      Assert.IsNotNull(actual);
      Assert.AreEqual<int>(expected.Items.Length, actual.Items.Length);
      List<InstanceDesign> _expectedList = new List<InstanceDesign>(expected.Items);
      _expectedList.Sort((x, y) => x.SymbolicName.ToString().CompareTo(y.SymbolicName.ToString()));
      List<InstanceDesign> _actualList = new List<InstanceDesign>(actual.Items);
      _actualList.Sort((x, y) => x.SymbolicName.ToString().CompareTo(y.SymbolicName.ToString()));
      for (int i = 0; i < _expectedList.Count; i++)
      {
        //CompareInstanceDesign(, );
        Type _expectedType = _expectedList[i].GetType();
        Type _actualType = _actualList[i].GetType();
        Assert.AreSame(_expectedType, _actualType);
        CompareNode(_expectedList[i], _actualList[i]);
      }
    }

    private static void Compare(Reference[] expected, Reference[] actual)
    {
      if (expected == null && actual == null)
        return;
      Assert.IsNotNull(expected);
      Assert.IsNotNull(actual);
      Assert.AreEqual<int>(expected.Length, actual.Length);
      Assert.IsTrue(!expected.Where<Reference>(x => x.ReferenceType == null || x.ReferenceType.IsEmpty).Any<Reference>());
      Assert.IsTrue(!actual.Where<Reference>(x => x.ReferenceType == null || x.ReferenceType.IsEmpty).Any<Reference>());
      Assert.IsTrue(!expected.Where<Reference>(x => x.TargetId == null || x.TargetId.IsEmpty).Any<Reference>());
      Assert.IsTrue(!actual.Where<Reference>(x => x.TargetId == null || x.TargetId.IsEmpty).Any<Reference>());
      Dictionary<string, Reference> _actualDictionary = actual.ToDictionary<Reference, string>(x => x.Key());
      foreach (Reference _rf in expected)
      {
        Assert.IsTrue(_actualDictionary.ContainsKey(_rf.Key()), _rf.Key());
        Assert.AreEqual<bool>(_rf.IsInverse, _actualDictionary[_rf.Key()].IsInverse, _rf.Key());
        Assert.IsFalse(_actualDictionary[_rf.Key()].IsOneWay, _rf.Key());
      }
    }

    private static void Compare(XmlQualifiedName expected, XmlQualifiedName actual, string parameter)
    {
      if ((expected == null || expected.IsEmpty) && (actual == null || actual.IsEmpty))
        return;
      Assert.IsNotNull(expected, parameter);
      Assert.IsNotNull(actual, parameter);
      Assert.IsFalse(expected.IsEmpty, parameter);
      Assert.IsFalse(actual.IsEmpty, parameter);
      Assert.AreEqual<string>(expected.Name, actual.Name.Replace("_", ""), parameter);
      Assert.AreEqual<string>(expected.Namespace, actual.Namespace, parameter);
    }

    private static void Compare(XmlElement expected, XmlElement actual)
    {
      return;
      if (expected == null && actual == null)
        return;
      Assert.IsNotNull(expected);
      Assert.IsNotNull(actual);
      Assert.AreEqual<string>(expected.InnerText, actual.InnerText);
      Compare(expected.Attributes, actual.Attributes);
    }

    private static void Compare(XmlAttributeCollection expected, XmlAttributeCollection actual)
    {
      if (expected == null && actual == null)
        return;
      Assert.IsNotNull(expected);
      Assert.IsNotNull(actual);
      Dictionary<string, XmlAttribute> _ad = expected.Cast<XmlAttribute>().ToDictionary<XmlAttribute, string>(x => x.Name);
      foreach (XmlAttribute _atr in actual)
      {
        Assert.IsTrue(_ad.ContainsKey(_atr.Name), _atr.InnerText);
        Assert.AreEqual<string>(_ad[_atr.Name].InnerText, _atr.InnerText);
      }
    }

    private void TraceDiagnostic(TraceMessage msg, List<TraceMessage> errors, ref int diagnosticCounter)
    {
      Console.WriteLine(msg.ToString());
      if (msg.BuildError.Focus == Focus.Diagnostic)
      {
        diagnosticCounter++;
      }
      else
        errors.Add(msg);
    }

    #endregion private helper

    #endregion Test instrumentation
  }
}