//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.AddressSpacePrototyping.CommandLineSyntax;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.AddressSpacePrototyping
{
  [TestClass]
  [DeploymentItem(@"XMLModels\", @"XMLModels\")]
  public class ProgramUnitTest
  {
    [TestMethod]
    public void DeploymentItemTestMethod()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"XMLModels\DataTypeTest.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
    }

    [TestMethod]
    public void ConstructorTest()
    {
      Program programInstance = new Program();
      ITraceSource currentLogger = null;
      programInstance.GetTraceSource(x => currentLogger = x);
      Assert.IsNotNull(currentLogger);
    }

    [TestMethod]
    public async Task EmptyArgsTest()
    {
      Program programInstance = new Program();
      Mock<ITraceSource> mockLogerr = new Mock<ITraceSource>();
      mockLogerr.Setup(x => x.TraceData(It.IsAny<TraceEventType>(), It.IsAny<int>(), It.IsAny<string>()));
      programInstance.DebugITraceSource = mockLogerr.Object;
      await programInstance.Run(new string[] { });
      mockLogerr.Verify(x => x.TraceData(It.IsAny<TraceEventType>(), It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(2));
    }

    [TestMethod]
    public async Task RunTheApplicationTestMethod()
    {
      Program program = new Program();
      await program.Run(new string[] { @"XMLModels\DataTypeTest.NodeSet2.xml" });
    }

    [TestMethod]
    public void OptionsTestMethod()
    {
      Mock<IAddressSpaceContext> asMock = new Mock<IAddressSpaceContext>();
      asMock.Setup(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()));
      Program program = new Program();

      Options options = new Options() { Filenames = null, IMNamespace = "bleble", ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      Assert.ThrowsException<ArgumentOutOfRangeException>(() => program.DoValidateAndExportModel(options, asMock.Object));
      asMock.Verify(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()), Times.Never);

      options = new Options() { Filenames = new List<string>() { "" }, IMNamespace = "bleble", ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      Assert.ThrowsException<UriFormatException>(() => program.DoValidateAndExportModel(options, asMock.Object));
      asMock.Verify(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()), Times.Never);

      options = new Options() { Filenames = new List<string>() { @"XMLModels\DataTypeTest.NodeSet2.xml" }, IMNamespace = String.Empty, ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      Assert.ThrowsException<ArgumentOutOfRangeException>(() => program.DoValidateAndExportModel(options, asMock.Object));
      asMock.Verify(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()), Times.Never);
    }

    [TestMethod]
    public void FileNotFounTest()
    {
      Mock<IAddressSpaceContext> asMock = new Mock<IAddressSpaceContext>();
      asMock.Setup(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()));
      Program program = new Program();
      Options options = new Options() { Filenames = new List<string>() { "bleble" }, IMNamespace = "http://cas.eu/UA/CommServer/UnitTests/DataTypeTest", ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      Assert.ThrowsException<FileNotFoundException>(() => program.DoValidateAndExportModel(options, asMock.Object));
      asMock.Verify(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()), Times.Once);
    }

    [TestMethod]
    public void ValidateExistingModelTest()
    {
      Mock<IAddressSpaceContext> asMock = new Mock<IAddressSpaceContext>();
      asMock.Setup(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()));
      Program program = new Program();
      Options options = new Options() { Filenames = new List<string>() { @"XMLModels\DataTypeTest.NodeSet2.xml" }, IMNamespace = "http://cas.eu/UA/CommServer/UnitTests/DataTypeTest", ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      program.DoValidateAndExportModel(options, asMock.Object);
      asMock.Verify(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()), Times.Exactly(2));
    }
  }
}