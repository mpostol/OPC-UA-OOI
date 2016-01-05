
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.DataManagement.UnitTest.Simulator;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{

  [TestClass]
  public class AssociationUnitTest
  {
    #region test

    #region Association
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    [TestCategory("DataManagement_Association")]
    public void AssociationCreatorTestMethod1()
    {
      TestAssociation _nt = new TestAssociation(null, null);
    }
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    [TestCategory("DataManagement_Association")]
    public void AssociationCreatorTestMethod2()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, null);
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    public void AssociationCreatorTestMethod4()
    {
      string _alias = "AssociationCreatorTestMethod4";
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, _alias);
      Assert.IsNotNull(_nt);
      Assert.IsNotNull(_nt.DataDescriptor);
      Assert.IsNotNull(_nt.DataDescriptor.Identifier);
      Assert.IsNotNull(_nt.State);
      Assert.AreNotEqual<Guid>(Guid.Empty, _nt.Id);
      Assert.AreEqual<HandlerState>(HandlerState.NoConfiguration, _nt.State.State);
      Assert.AreEqual<string>(_alias, _nt.ToString());
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    public void AssociationCompareToTestMethod()
    {
      ISemanticData _testISemanticData0 = new TestISemanticData("TestISemanticData1", 0, Guid.Parse(@"{9912B722-304D-438F-8538-3C6F98068E66}"));
      Assert.IsNotNull(_testISemanticData0);
      TestAssociation _nt0 = new TestAssociation(_testISemanticData0, "AssociationCompareToTestMethod0");
      Assert.IsNotNull(_nt0);
      Assert.AreEqual<int>(0, _nt0.CompareTo(_nt0));
      ISemanticData _testISemanticData1 = new TestISemanticData("TestISemanticData0", 0, Guid.Parse(@"{9912B722-304D-438F-8538-3C6F98068E65}"));
      Assert.IsNotNull(_testISemanticData1);
      TestAssociation _nt1 = new TestAssociation(_testISemanticData1, "AssociationCompareToTestMethod1");
      Assert.IsNotNull(_nt1);
      Assert.AreEqual<int>(-1, _nt1.CompareTo(_nt0));
      Assert.AreEqual<int>(1, _nt0.CompareTo(_nt1));
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AssociationStateDisableTestMethod()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "AssociationStateDisableTestMethod");
      Assert.IsNotNull(_nt);
      _nt.State.Disable();
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AssociationStateEnableTestMethod()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "AssociationStateEnableTestMethod");
      Assert.IsNotNull(_nt);
      _nt.State.Enable();
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    public void AssociationInitializeMethod()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "AssociationInitializeMethod");
      Assert.IsNotNull(_nt);
      int _eventsCount = 0;
      HandlerState _lastState = default(HandlerState);
      _nt.StateChangedEventHandler += (x, y) => { _eventsCount++; _lastState = y.State; };
      Assert.AreEqual<HandlerState>(HandlerState.NoConfiguration, _nt.State.State);
      Assert.AreEqual<int>(0, _eventsCount);
      _nt.Initialize();
      Assert.AreEqual<int>(1, _eventsCount);
      Assert.IsNotNull(_lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _nt.State.State);
      _nt.State.Enable();
      Assert.AreEqual<int>(2, _eventsCount);
      Assert.IsNotNull(_lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Operational, _lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Operational, _nt.State.State);
      _nt.State.Disable();
      Assert.AreEqual<int>(3, _eventsCount);
      Assert.IsNotNull(_lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Disabled, _nt.State.State);
    }
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AssociationInitializeMethod2()
    {
      ISemanticData _testISemanticData = new TestISemanticData();
      Assert.IsNotNull(_testISemanticData);
      TestAssociation _nt = new TestAssociation(_testISemanticData, "AssociationInitializeMethod2", false);
      Assert.IsNotNull(_nt);
      int _eventsCount = 0;
      HandlerState _lastState = default(HandlerState);
      _nt.StateChangedEventHandler += (x, y) => { _eventsCount++; _lastState = y.State; };
      Assert.AreEqual<HandlerState>(HandlerState.NoConfiguration, _nt.State.State);
      _nt.Initialize();
      Assert.AreEqual<int>(1, _eventsCount);
      Assert.IsNotNull(_lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Error, _lastState);
      Assert.AreEqual<HandlerState>(HandlerState.Error, _nt.State.State);
      _nt.State.Enable();
    }
    #endregion

    #region ConsumerAssociation
    [TestMethod]
    [TestCategory("DataManagement_Association")]
    public void ConsumerAssociationCreatorTestMethod()
    {
      ConsumerAssociation _ca = new ConsumerAssociation(new SemanticData(), PersistentConfiguration.GetAssociationConfiguration(), new BindingFactory(Repository), new IEF());
      Assert.IsNotNull(_ca);
    }
    #endregion

    #endregion

    #region private
    private class TestAssociation : Association
    {
      public TestAssociation(ISemanticData data, string aliasName, bool success)
        : base(data, aliasName)
      {
        m_Success = success;
      }
      public TestAssociation(ISemanticData data, string aliasName)
        : this(data, aliasName, true)
      { }
      protected override void InitializeCommunication()
      {
        if (!m_Success)
          throw new InvalidOperationException("Wrong configuration");
      }
      public Guid Id { get { return DataDescriptor.Guid; } }
      protected override void OnEnabling() { }
      protected override void OnDisabling() { }
      private bool m_Success = false;
      protected internal override void AddMessageHandler(IMessageHandler messageHandler, AssociationConfiguration configuration)
      {
        throw new NotImplementedException();
      }
    }
    private class DataBrokerFactory : IBindingFactory
    {
      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo field)
      {
        return new ConsumerBinding<int>(x => { }, field.BuiltInType);
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, UATypeInfo encoding)
      {
        throw new NotImplementedException();
      }

      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        throw new NotImplementedException();
      }
    }
    private class TestISemanticData : ISemanticData
    {
      public TestISemanticData()
        : this("SymbolicName".AddId(_count), _count)
      {
        _count++;
      }
      public TestISemanticData(string symbolicName, IComparable nodeId)
        : this(symbolicName, nodeId, Guid.NewGuid())
      {
        Identifier = new Uri(@"Http://commsvr.com");
        SymbolicName = symbolicName;
        NodeId = nodeId;
      }
      public TestISemanticData(string symbolicName, IComparable nodeId, Guid newGuid)
      {
        Guid = newGuid;
      }
      public Uri Identifier
      {
        get;
        private set;
      }
      public string SymbolicName
      {
        get;
        private set;
      }
      public IComparable NodeId
      {
        get;
        private set;
      }
      private static int _count = 0;
      public Guid Guid
      {
        get;
        private set;
      }
    }
    /// <summary>
    /// Class SemanticData.
    /// </summary>
    private class SemanticData : ISemanticData
    {
      public Uri Identifier
      {
        get { throw new NotImplementedException(); }
      }
      public string SymbolicName
      {
        get { throw new NotImplementedException(); }
      }
      public IComparable NodeId
      {
        get { throw new NotImplementedException(); }
      }
      public Guid Guid
      {
        get { return Guid.NewGuid(); }
      }
    }
    private class BindingFactory : IBindingFactory
    {
      public BindingFactory(Dictionary<string, IBinding> repository)
      {
        m_Repository = repository;
      }

      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo field)
      {
        IConsumerBinding _ncb = new ConsumerBindingMonitoredValue<object>(field.BuiltInType);
        string _key = String.Format("{0}.{1}", repositoryGroup, processValueName);
        m_Repository.Add(_key, _ncb);
        return _ncb;
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        string _key = String.Format("{0}.{1}", repositoryGroup, variableName);
        IProducerBinding _npb = new ProducerBindingMonitoredValue<object>(_key, encoding);
        m_Repository.Add(_key, _npb);
        return _npb;
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, UATypeInfo encoding)
      {
        throw new NotImplementedException();
      }

      private Dictionary<string, IBinding> m_Repository = new Dictionary<string, IBinding>();
    }
    private Dictionary<string, IBinding> Repository = new Dictionary<string, IBinding>();
    private class IEF : IEncodingFactory
    {
      public IUADecoder UADecoder
      {
        get { return m_UADecoder; }
      }
      public IUAEncoder UAEncoder
      {
        get
        {
          throw new NotImplementedException();
        }
      }
      public void UpdateValueConverter(IBinding binding, string repositoryGroup, UATypeInfo sourceEncoding)
      {
        Assert.IsNotNull(binding);
        binding.Culture = null;
        binding.Converter = null;
        binding.Parameter = null;
      }
      private readonly IUADecoder m_UADecoder = new Helpers.UABinaryDecoderImplementation();

    }
    #endregion

  }

}
