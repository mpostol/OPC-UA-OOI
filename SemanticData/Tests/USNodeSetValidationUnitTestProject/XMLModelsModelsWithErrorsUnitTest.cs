//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.Helpers;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  [DeploymentItem(@"XMLModels\ModelsWithErrors\", @"ModelsWithErrors\")]
  public class XMLModelsModelsWithErrorsUnitTest
  {
    #region TestMethod

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void ObjectEventNotifierOutOfRangeTestMethod()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\WrongEventNotifier.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(2);
      Assert.AreEqual<string>(BuildError.WrongEventNotifier.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[1].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongReference2PropertyTestMethod()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\WrongReference2Property.xml");
      _log.TestConsistency(1);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(2);
      //TODO The exported model doesn't contain all nodes #653 review WrongReference2PropertyTestMethod
      Assert.AreEqual<string>(BuildError.WrongReference2Property.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[1].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongValueRankTestMethod()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\WrongValueRank.xml");
      _log.TestConsistency(1);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(3);
      Assert.AreEqual<string>(BuildError.WrongValueRank.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.WrongValueRank.Identifier, _log[1].BuildError.Identifier);
      Assert.AreEqual<string>(_log[0].Message, _log[1].Message); //Duplicated log entry
      Assert.AreEqual<string>("The value -4 is not supported", _log[0].Message);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[2].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongAccessLevelTestMethod()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\WrongAccessLevel.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(2);
      Assert.AreEqual<string>(BuildError.WrongAccessLevel.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[1].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongInverseNameTestMethod()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\WrongInverseName.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(4);
      Assert.AreEqual<string>(BuildError.WrongInverseName.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.WrongInverseName.Identifier, _log[1].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.WrongInverseName.Identifier, _log[2].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[3].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void DuplicatedNodeIdTestMethod()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\DuplicatedNodeId.xml");
      _log.TestConsistency(2);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NodeIdDuplicated.Identifier, _log[1].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(0);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongDisplayNameLength()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\WrongDisplayNameLength.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(2);
      Assert.AreEqual<string>(BuildError.WrongDisplayNameLength.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[1].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongWriteMaskValue()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\WrongWriteMask.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(3);
      Assert.AreEqual<string>(BuildError.WrongWriteMaskValue.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.WrongWriteMaskValue.Identifier, _log[1].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[2].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void NotSupportedFeature()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\NotSupportedFeature.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(0);
      //Assert.AreEqual<int>(0, _log.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongBrowseName()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\WrongBrowseName.xml");
      _log.TestConsistency(2);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.EmptyBrowseName.Identifier, _log[1].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      Assert.AreEqual<string>(BuildError.WrongSymbolicName.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[1].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongNodeId()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\WrongNodeId.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(2);
      Assert.AreEqual<string>(BuildError.NodeIdNotDefined.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[1].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void UndefinedHasSubtypeTestMethod()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\UndefinedHasSubtype.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(2);
      Assert.AreEqual<string>(BuildError.UndefinedHasSubtypeTarget.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[1].BuildError.Identifier);
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void UndefinedHasTypeDefinitionTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\UndefinedHasTypeDefinition.xml");
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\UndefinedHasTypeDefinition.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(0);
      //TODO Recognize problems with P3.7.13 HasTypeDefinition ReferenceType #39
    }

    /// <summary>
    /// Class UndefinedHasComponentTargetTestClass: Handle HasComponent ReferenceType errors. #42
    /// </summary>
    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void UndefinedHasComponentTargetTestMethod()
    {
      TracedAddressSpaceContext _log = new TracedAddressSpaceContext(@"ModelsWithErrors\UndefinedHasChildren.xml");
      _log.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      _log.Clear();
      _log.ValidateAndExportModel(m_NameSpace);
      _log.TestConsistency(5);
      Assert.AreEqual<string>(BuildError.NodeCannotBeNull.Identifier, _log[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NodeCannotBeNull.Identifier, _log[1].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.DanglingReferenceTarget.Identifier, _log[2].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.DanglingReferenceTarget.Identifier, _log[3].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.NonCategorized.Identifier, _log[4].BuildError.Identifier);
    }

    #endregion TestMethod

    #region private

    private readonly Uri m_NameSpace = new Uri(@"http://commsvr.com/OOIUA/SemanticData/UnitTest/UANodeSetValidationUnitTestProject");

    #endregion private
  }
}