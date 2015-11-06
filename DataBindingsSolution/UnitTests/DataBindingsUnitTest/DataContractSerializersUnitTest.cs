
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization;
using UAOOI.DataBindings.Serializers;

namespace UAOOI.DataBindings.UnitTest
{
  /// <summary>
  /// Summary description for DataContractSerializersUnitTest
  /// </summary>
  [TestClass]
  public class DataContractSerializersUnitTest
  {
    public DataContractSerializersUnitTest()
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
    public void SerializerTestMethod1()
    {
      // Create a new instance of the Person class and serialize it to an XML file.
      Person p1 = new Person("Mary", 1);
      string fileName = "DataContractExample.xml";
      DataContractSerializers.Save<Person>(new FileInfo(fileName), p1, (x, y, z) => Assert.AreEqual(System.Diagnostics.TraceEventType.Verbose, x));
      FileInfo _newFile = new FileInfo(fileName);
      Assert.IsTrue(_newFile.Exists);
      Person p2 = DataContractSerializers.Load<Person>(new FileInfo(fileName), (x, y, z) => Assert.AreEqual<System.Diagnostics.TraceEventType>(System.Diagnostics.TraceEventType.Verbose, x));
      Assert.IsNotNull(p2);
      Assert.AreEqual<int>(p1.ID, p2.ID);
      Assert.AreEqual<string>(p1.Name, p2.Name);
    }

    #region private part
    // Set the Name and Namespace properties to new values.
    [DataContract(Name = "Customer", Namespace = "http://www.commsvr.com/UAOOI/DataBindings/UnitTest")]
    private class Person : IExtensibleDataObject
    {
      public Person() { }
      // To implement the IExtensibleDataObject interface, you must also implement the ExtensionData property.
      private ExtensionDataObject extensionDataObjectValue;
      public ExtensionDataObject ExtensionData
      {
        get
        {
          return extensionDataObjectValue;
        }
        set
        {
          extensionDataObjectValue = value;
        }
      }

      [DataMember(Name = "CustName")]
      internal string Name;

      [DataMember(Name = "CustID")]
      internal int ID;

      public Person(string newName, int newID)
      {
        Name = newName;
        ID = newID;
      }

    }
    #endregion

  }
}

