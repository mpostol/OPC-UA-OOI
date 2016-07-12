using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking.UnitTest
{
  [TestClass]
  public class NodeDescriptorUnitTest
  {
    [TestMethod]
    public void CreatorTest()
    {
      NodeDescriptor _newDescriptor = new NodeDescriptor()
      {
        BindingDescription = "BindingDescription",
        DataType = new System.Xml.XmlQualifiedName("DataType", m_Namespace),
        InstanceDeclaration = false,
        NodeClass = InstanceNodeClassesEnum.Object,
        NodeIdentifier = new System.Xml.XmlQualifiedName("Name1", m_Namespace)
      };
      IComparable _comparer = _newDescriptor.CreateWrapper();
      Assert.AreEqual<int>(0, _comparer.CompareTo(_comparer));
    }
    [TestMethod]
    public void PrecedenceTestMethod()
    {
      NodeDescriptor _FirstNewDescriptor = new NodeDescriptor()
      {
        BindingDescription = "BindingDescription",
        DataType = new System.Xml.XmlQualifiedName("DataType", m_Namespace),
        InstanceDeclaration = false,
        NodeClass = InstanceNodeClassesEnum.Object,
        NodeIdentifier = new System.Xml.XmlQualifiedName("Name1", m_Namespace)
      };
      NodeDescriptor _SecondNewDescriptor = new NodeDescriptor()
      {
        BindingDescription = "BindingDescription",
        DataType = new System.Xml.XmlQualifiedName("DataType", m_Namespace),
        InstanceDeclaration = false,
        NodeClass = InstanceNodeClassesEnum.Object,
        NodeIdentifier = new System.Xml.XmlQualifiedName("Name2", m_Namespace)
      };
      IComparable _FirstComparer = _FirstNewDescriptor.CreateWrapper();
      IComparable _SecondComparer = _SecondNewDescriptor.CreateWrapper();
      Assert.IsTrue(_FirstComparer.CompareTo(_SecondComparer ) <0);
      Assert.IsTrue(_SecondComparer.CompareTo(_FirstComparer) > 0 );
    }
    private readonly string m_Namespace = @"http://tempuri.org/TestData";
  }
}
