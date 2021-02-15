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
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongEventNotifier.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log.TraceList[0].BuildError.Identifier);
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(2, _log.TraceList.Count);
        Assert.AreEqual<string>(BuildError.WrongEventNotifier.Identifier, _log.TraceList[0].BuildError.Identifier);
        Assert.AreEqual<string>(BuildError.ModelContainsErrors.Identifier, _log.TraceList[1].BuildError.Identifier);
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongReference2PropertyTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongReference2Property.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Count());
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Count());
        Assert.AreEqual<int>(1, (_log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P0-0001050000").Count<TraceMessage>()));
        Assert.AreEqual<string>(BuildError.WrongReference2Variable.Identifier, _log.TraceList[0].BuildError.Identifier);
        Assert.AreEqual<string>(BuildError.WrongReference2Property.Identifier, _log.TraceList[1].BuildError.Identifier);
        Assert.AreEqual<string>(BuildError.ModelContainsErrors.Identifier, _log.TraceList[2].BuildError.Identifier);
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongValueRankTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongValueRank.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Count());
        Assert.AreEqual<string>(BuildError.WrongValueRank.Identifier, _log.TraceList[0].BuildError.Identifier);
        Assert.AreEqual<string>(BuildError.WrongValueRank.Identifier, _log.TraceList[1].BuildError.Identifier);
        Assert.AreEqual<string>(_log.TraceList[0].Message, _log.TraceList[1].Message); //Duplicated log entry
        Assert.AreEqual<string>("The value -4 is not supported", _log.TraceList[0].Message);
        Assert.AreEqual<string>(BuildError.ModelContainsErrors.Identifier, _log.TraceList[2].BuildError.Identifier);
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongAccessLevelTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongAccessLevel.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Count());
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.IsTrue(_log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0506040000").Any<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongInverseNameTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongInverseName.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(4, _log.TraceList.Count());
        Assert.AreEqual(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0503020000").Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void DuplicatedNodeIdTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\DuplicatedNodeId.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Count());
        Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, _log.TraceList[0].BuildError.Identifier);
        Assert.AreEqual<string>(BuildError.NodeIdDuplicated.Identifier, _log.TraceList[1].BuildError.Identifier);
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(0, _log.TraceList.Count);
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongDisplayNameLength()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongDisplayNameLength.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.IsTrue(_log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0502050000").Any<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongWriteMaskValue()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongWriteMask.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == BuildError.WrongWriteMaskValue.Identifier).Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void NotSupportedFeature()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\NotSupportedFeature.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P0-0001010000").Count<TraceMessage>());
        Assert.AreEqual<int>(1, (_log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P0-0001050000").Count<TraceMessage>()));
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongBrowseName()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongBrowseName.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Count());
        Assert.AreEqual<string>(BuildError.EmptyBrowseName.Identifier, _log.TraceList[1].BuildError.Identifier);
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<string>(BuildError.WrongSymbolicName.Identifier, _log.TraceList[0].BuildError.Identifier);
        Assert.AreEqual<string>(BuildError.ModelContainsErrors.Identifier, _log.TraceList[1].BuildError.Identifier);
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongNodeId()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongNodeId.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(4, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0502020001").Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void UndefinedHasSubtypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\UndefinedHasSubtype.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0710000001").Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void UndefinedHasTypeDefinitionTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\UndefinedHasTypeDefinition.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        //TODO Recognize problems with P3.7.13 HasTypeDefinition ReferenceType #39
        Assert.Inconclusive("Instances are not imported - the error is not recognized.");
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0713000000").Count<TraceMessage>());
      }
    }

    /// <summary>
    /// Class UndefinedHasComponentTargetTestClass: Handle HasComponent ReferenceType errors. #42
    /// </summary>
    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void UndefinedHasComponentTargetTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\UndefinedHasChildren.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext _log = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext _as = _log.CreateAddressSpaceContext();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, _log.TraceList.Count());
        _log.Clear();
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0707000002").Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0708000000").Count<TraceMessage>());
      }
    }

    #endregion TestMethod

    #region private



    private readonly Uri m_NameSpace = new Uri(@"http://commsvr.com/OOIUA/SemanticData/UnitTest/UANodeSetValidationUnitTestProject");

    #endregion private
  }
}