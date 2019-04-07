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
using UAOOI.SemanticData.AddressSpaceTestTool.CommandLineSyntax;

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
      Assert.IsFalse(_return.DontCreate);
      Assert.IsFalse(_return.OnlyAccessTime);
      Assert.IsFalse(_return.OnlyModificationTime);
      Assert.IsNotNull(_return.Filenames);
      Assert.AreEqual<int>(1, _return.Filenames.Count());
    }
    [TestMethod]
    public void FilesTest()
    {
      string[] _emptyLine = new string[] { "AssemblyInfo.cs", "myproject.csproj" };
      Options _return = null;
      IEnumerable<Error> _errors = null;
      _emptyLine.Parse<Options>(x => _return = x, y => _errors = y);
      Assert.IsNotNull(_return);
      Assert.IsNull(_errors);
      Assert.IsFalse(_return.DontCreate);
      Assert.IsFalse(_return.OnlyAccessTime);
      Assert.IsFalse(_return.OnlyModificationTime);
      Assert.IsNotNull(_return.Filenames);
      Assert.AreEqual<int>(2, _return.Filenames.Count());
      string[] _files = _return.Filenames.ToArray<string>();
      Assert.AreEqual<string>("AssemblyInfo.cs", _files[0]);
      Assert.AreEqual<string>("myproject.csproj", _files[1]);
    }
    [TestMethod]
    public void AllSwitchesTest()
    {
      string[] _emptyLine = new string[] { "-m", "-a", "-c", "App_Data/cachefile.json", "App_Data/cachefile2.json" };
      Options _return = null;
      IEnumerable<Error> _errors = null;
      _emptyLine.Parse<Options>(x => _return = x, y => _errors = y);
      Assert.IsNotNull(_return);
      Assert.IsNull(_errors);
      Assert.IsTrue(_return.DontCreate);
      Assert.IsTrue(_return.OnlyAccessTime);
      Assert.IsTrue(_return.OnlyModificationTime);
      Assert.IsNotNull(_return.Filenames);
      Assert.AreEqual<int>(2, _return.Filenames.Count());
      string[] _files = _return.Filenames.ToArray<string>();
      Assert.AreEqual<string>("App_Data/cachefile.json", _files[0]);
      Assert.AreEqual<string>("App_Data/cachefile2.json", _files[1]);
    }
  }
}
