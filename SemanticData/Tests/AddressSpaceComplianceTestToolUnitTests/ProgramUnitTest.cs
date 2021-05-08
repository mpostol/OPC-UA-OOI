//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using UAOOI.SemanticData.AddressSpacePrototyping.CommandLineSyntax;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation;
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
    public void RunTheApplicationTestMethod()
    {
      Program.Run(new string[] { @"XMLModels\DataTypeTest.NodeSet2.xml" });
    }

    [TestMethod]
    public void OptionsTestMethod()
    {
      Mock<IAddressSpaceContext> asMock = new Mock<IAddressSpaceContext>();
      asMock.Setup(x => x.ImportUANodeSet(It.IsAny<FileInfo>()));
      asMock.Setup(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()));
      asMock.SetupSet(x => x.InformationModelFactory = It.IsAny<IModelFactory>());

      Options options = new Options() { Filenames = null, IMNamespace = "bleble", ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      Assert.ThrowsException<ArgumentOutOfRangeException>(() => Program.Do(options, asMock.Object));
      options = new Options() { Filenames = new List<string>() { "" }, IMNamespace = "bleble", ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      Assert.ThrowsException<UriFormatException>(() => Program.Do(options, asMock.Object));
      options = new Options() { Filenames = new List<string>() { "bleble" }, IMNamespace = "http://cas.eu/UA/CommServer/UnitTests/DataTypeTest", ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      Assert.ThrowsException<FileNotFoundException>(() => Program.Do(options, asMock.Object));
      options = new Options() { Filenames = new List<string>() { @"XMLModels\DataTypeTest.NodeSet2.xml" }, IMNamespace = String.Empty, ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      Assert.ThrowsException<ArgumentOutOfRangeException>(() => Program.Do(options, asMock.Object));
      asMock.VerifySet(x => x.InformationModelFactory = It.IsAny<IModelFactory>(), Times.Never);
      asMock.Verify(x => x.ImportUANodeSet(It.IsAny<FileInfo>()), Times.Never);
      asMock.Verify(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()), Times.Never);

      options = new Options() { Filenames = new List<string>() { @"XMLModels\DataTypeTest.NodeSet2.xml" }, IMNamespace = "http://cas.eu/UA/CommServer/UnitTests/DataTypeTest", ModelDesignFileName = string.Empty, NoLogo = true, Stylesheet = string.Empty };
      Program.Do(options, asMock.Object);
      asMock.VerifySet(x => x.InformationModelFactory = It.IsAny<IModelFactory>(), Times.Never);
      asMock.Verify(x => x.ImportUANodeSet(It.IsAny<FileInfo>()), Times.Once);
      asMock.Verify(x => x.ImportUANodeSet(It.IsAny<UANodeSet>()), Times.Never);
    }
  }
}