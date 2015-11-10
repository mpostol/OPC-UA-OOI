
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UAOOI.DataBindings.Serializers;

namespace UAOOI.DataBindings.UnitTest
{
  /// <summary>
  /// Summary description for DataContractSerializersUnitTest
  /// </summary>
  [TestClass]
  public class XmlDataContractSerializersUnitTest
  {
    public XmlDataContractSerializersUnitTest()
    {
      //
      // TODO: Add constructor logic here
      //
    }

    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    [TestMethod]
    [TestCategory("DataBindings_XmlSerializerTestMethod")]
    public void XmlSerializerTestMethod()
    {
      // Create a new instance of the Person class and serialize it to an XML file.
      CSharpSelectedTypesEncoding p1 = new CSharpSelectedTypesEncoding();
      string fileName = "DataContractExample.xml";
      XmlDataContractSerializers.Save<CSharpSelectedTypesEncoding>(new FileInfo(fileName), p1, (x, y, z) => Assert.AreEqual(System.Diagnostics.TraceEventType.Verbose, x));
      FileInfo _newFile = new FileInfo(fileName);
      Assert.IsTrue(_newFile.Exists);
      CSharpSelectedTypesEncoding p2 = XmlDataContractSerializers.Load<CSharpSelectedTypesEncoding>(new FileInfo(fileName), (x, y, z) => Assert.AreEqual<System.Diagnostics.TraceEventType>(System.Diagnostics.TraceEventType.Verbose, x));
      Assert.IsNotNull(p2);
      p1.AreEqual(p2);

    }
  }
}

