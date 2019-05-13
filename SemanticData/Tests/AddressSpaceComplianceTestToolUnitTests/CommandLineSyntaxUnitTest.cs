//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommandLine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.AddressSpacePrototyping.CommandLineSyntax;

namespace UAOOI.SemanticData.AddressSpaceTestTool.UnitTests
{

  [TestClass]
  public class CommandLineSyntaxUnitTest
  {
    [TestMethod]
    public void EmptyLineTest()
    {
      string[] _emptyLine = new string[] {"One file name must be specified" };
      Options _return = null;
      IEnumerable<Error> _errors = null;
      _emptyLine.Parse<Options>(x => _return = x, y => _errors = y);
      Assert.IsNotNull(_return);
      Assert.IsNull(_errors);
      Assert.IsNotNull(_return.Filenames);
      Assert.AreEqual<int>(1, _return.Filenames.Count());
      Assert.IsTrue(string.IsNullOrEmpty(_return.IMNamespace));
      Assert.IsTrue(string.IsNullOrEmpty(_return.ModelDesignFileName));
      Assert.IsFalse(_return.NoLogo);
      Assert.IsTrue(string.IsNullOrEmpty(_return.Stylesheet));
    }
    [TestMethod]
    public void FilenamesTest()
    {
      string[] _emptyLine = new string[] { "AssemblyInfo.cs", "myproject.csproj" };
      Options _return = null;
      IEnumerable<Error> _errors = null;
      _emptyLine.Parse<Options>(x => _return = x, y => _errors = y);
      Assert.IsNotNull(_return);
      Assert.IsNull(_errors);
      Assert.IsNotNull(_return.Filenames);
      Assert.AreEqual<int>(2, _return.Filenames.Count());
      string[] _files = _return.Filenames.ToArray<string>();
      Assert.AreEqual<string>("AssemblyInfo.cs", _files[0]);
      Assert.AreEqual<string>("myproject.csproj", _files[1]);
    }
    [TestMethod]
    public void AllLongSwitchesTest()
    {
      string[] _emptyLine = new string[] { "App_Data/cachefile.json", "App_Data/cachefile2.json", "--namespace=XSLTName", "--export=ModelDesign.xml", "--stylesheet=XMLstylesheet", "--nologo" };
      Options _return = null;
      IEnumerable<Error> _errors = null;
      _emptyLine.Parse<Options>(x => _return = x, y => _errors = y);
      Assert.IsNotNull(_return);
      Assert.IsNull(_errors);
      Assert.IsNotNull(_return.Filenames);
      Assert.AreEqual<int>(2, _return.Filenames.Count());
      string[] _files = _return.Filenames.ToArray<string>();
      Assert.AreEqual<string>("App_Data/cachefile.json", _files[0]);
      Assert.AreEqual<string>("App_Data/cachefile2.json", _files[1]);
      Assert.AreEqual<string>("XSLTName", _return.IMNamespace);
      Assert.AreEqual<string>("ModelDesign.xml", _return.ModelDesignFileName);
      Assert.IsTrue(_return.NoLogo);
      Assert.AreEqual<string>("XMLstylesheet", _return.Stylesheet);  
    }
    [TestMethod]
    public void AllShortSwitchesTest()
    {
      string[] _emptyLine = new string[] { "App_Data/cachefile.json", "App_Data/cachefile2.json", "-n", "XSLTName", "-e", "ModelDesign.xml", "-s", "XMLstylesheet", "--nologo" };
      Options _return = null;
      IEnumerable<Error> _errors = null;
      _emptyLine.Parse<Options>(x => _return = x, y => _errors = y);
      Assert.IsNotNull(_return);
      Assert.IsNull(_errors);
      Assert.IsNotNull(_return.Filenames);
      Assert.AreEqual<int>(2, _return.Filenames.Count());
      string[] _files = _return.Filenames.ToArray<string>();
      Assert.AreEqual<string>("App_Data/cachefile.json", _files[0]);
      Assert.AreEqual<string>("App_Data/cachefile2.json", _files[1]);
      Assert.AreEqual<string>("XSLTName", _return.IMNamespace);
      Assert.AreEqual<string>("ModelDesign.xml", _return.ModelDesignFileName);
      Assert.IsTrue(_return.NoLogo);
      Assert.AreEqual<string>("XMLstylesheet", _return.Stylesheet);
    }
  }
}
