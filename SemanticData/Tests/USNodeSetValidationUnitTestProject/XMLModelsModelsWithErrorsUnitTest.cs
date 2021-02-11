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

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
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
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongReference2PropertyTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongReference2Property.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(4, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongValueRankTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongValueRank.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(4, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongAccessLevelTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongAccessLevel.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.IsTrue(_log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0506040000").Any<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongInverseNameTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongInverseName.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(5, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0503020000").Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void DuplicatedNodeIdTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\DuplicatedNodeId.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.IsTrue(_log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0502020000").Any<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongDisplayNameLength()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongDisplayNameLength.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.IsTrue(_log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0502050000").Any<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongWriteMaskValue()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongWriteMask.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(4, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0502070000").Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void NotSupportedFeature()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\NotSupportedFeature.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        Assert.IsNotNull(_as);
        Assert.IsNotNull(_as);
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P0-0001010000").Count<TraceMessage>());
        BuildErrorsHandling.Log.TraceEventAction -= _message => _log.TraceList.Add(_message);
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongBrowseName()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongBrowseName.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(4, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P6-0503011400").Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P6-0F03000000").Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void WrongNodeId()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\WrongNodeId.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(5, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0502020001").Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void UndefinedHasSubtypeTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\UndefinedHasSubtype.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(3, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0710000001").Count<TraceMessage>());
      }
    }

    [TestMethod]
    [TestCategory("Incorrect Model")]
    public void UndefinedHasTypeDefinitionTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ModelsWithErrors\UndefinedHasTypeDefinition.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
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
      using (Logger _log = new Logger())
      {
        IAddressSpaceContext _as = _log.GetInstance();
        _as.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(2, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        _as.ValidateAndExportModel(m_NameSpace);
        Assert.AreEqual<int>(4, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0707000002").Count<TraceMessage>());
        Assert.AreEqual<int>(1, _log.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == "P3-0708000000").Count<TraceMessage>());
      }
    }

    #endregion TestMethod

    #region private

    private class Logger : IDisposable
    {
      internal IAddressSpaceContext GetInstance()
      {
        TraceList = new List<TraceMessage>();
        return new AddressSpaceContext(z => TraceDiagnostic(z, TraceList, ref _diagnosticCounter));
      }

      public void Dispose()
      {
        BuildErrorsHandling.Log.TraceEventAction -= _message => TraceDiagnostic(_message, TraceList, ref _diagnosticCounter);
      }

      internal List<TraceMessage> TraceList;
      internal int _diagnosticCounter = 0;

      internal Logger()
      {
        BuildErrorsHandling.Log.TraceEventAction += _message => TraceDiagnostic(_message, TraceList, ref _diagnosticCounter);
      }

      private static void TraceDiagnostic(TraceMessage msg, List<TraceMessage> errors, ref int diagnosticCounter)
      {
        Console.WriteLine(msg.ToString());
        if (msg.BuildError.Focus == Focus.Diagnostic)
          diagnosticCounter++;
        else
          errors.Add(msg);
      }
    }

    private const string m_NameSpace = @"http://commsvr.com/OOIUA/SemanticData/UnitTest/UANodeSetValidationUnitTestProject";

    #endregion private
  }
}