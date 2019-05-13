//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  [TestClass]
  public class ModelDesignExportUnitTest
  {
    [TestMethod]
    public void ConstructorTestMethod()
    {
      ModelDesignExport _exporter = new ModelDesignExport();
      List<TraceMessage> _log = new List<TraceMessage>();
      string _filePath = "ConstructorTestMethodPtah.xml";
      IModelFactory _factory = _exporter.GetFactory(_filePath, x => _log.Add(x));
      _factory.CreateNamespace("NameSpace1", String.Empty, String.Empty);
      _factory.CreateNamespace("NameSpace2", String.Empty, String.Empty);
      _exporter.ExportToXMLFile();
      FileInfo _outputFile = new FileInfo(_filePath);
      Assert.IsTrue(_outputFile.Exists);
      Assert.AreEqual<long>(1, _log.Count);
      Assert.IsTrue(720 < _outputFile.Length);
    }
  }
}
