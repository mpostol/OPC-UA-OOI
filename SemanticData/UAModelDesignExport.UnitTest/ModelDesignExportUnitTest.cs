//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;
using System.IO;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  [TestClass]
  public class ModelDesignExportUnitTest
  {
    [TestMethod]
    public void ConstructorTestMethod()
    {
      Mock<ITraceSource> mock = new Mock<ITraceSource>();
      mock.Setup(x => x.TraceData(It.IsAny<TraceEventType>(), It.IsAny<int>(), It.IsAny<string>()));
      IModelDesignExport _exporter = ModelDesignExportAPI.GetModelDesignExport(mock.Object);
      string _filePath = "ConstructorTestMethodPtah.xml";
      IModelFactory _factory = _exporter.GetFactory();
      _factory.CreateNamespace(new Uri("NameSpace1", UriKind.Relative), DateTime.UtcNow, null);
      _factory.CreateNamespace(new Uri("NameSpace2", UriKind.Relative), DateTime.UtcNow, null);
      _exporter.ExportToXMLFile(_filePath);
      FileInfo _outputFile = new FileInfo(_filePath);
      Assert.IsTrue(_outputFile.Exists);
      mock.Verify(x => x.TraceData(It.IsAny<TraceEventType>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
      Assert.IsTrue(670 < _outputFile.Length, $"File length is {_outputFile.Length}");
    }
  }
}