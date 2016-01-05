
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows.Data;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ConsumerBindingUnitTest
  {

    #region TestMethods
    [TestMethod]
    [TestCategory("DataManagement_Binding")]
    public void TestMethod1()
    {
      ConsumerBinding<int> _nb = new ConsumerBinding<int>(x => { }, BuiltInType.Null);
      Assert.IsNotNull(_nb);
    }
    [TestMethod]
    [TestCategory("DataManagement_Binding")]
    public void DataRepositoryTestMethod()
    {
      DataRepository _rpo = new DataRepository();
      IConsumerBinding _nb = _rpo.GetConsumerBinding(String.Empty, DataRepository.name, new UATypeInfo(BuiltInType.String));
      Assert.IsNotNull(_nb);
      Assert.IsNotNull(_nb.Encoding);
      _nb.Converter = null;
      Assert.AreEqual<BuiltInType>(BuiltInType.String, _nb.Encoding);
      string _testValue = "123wjkqjwkqjwjqjwqwjwqkwqjw";
      _nb.Assign2Repository(_testValue);
      Assert.AreEqual<string>(_testValue, _rpo.Buffer);
    }
    [TestMethod]
    [TestCategory("DataManagement_Binding")]
    public void RecordingRepositoryTestMethod()
    {
      RecordingRepository _rpo = new RecordingRepository();
      IConsumerBinding _nb = _rpo.GetConsumerBinding(String.Empty, DataRepository.name, new UATypeInfo(BuiltInType.String));
      Assert.IsNotNull(_nb);
      Assert.IsNotNull(_nb.Encoding);
      _nb.Converter = new DateFormatter();
      Assert.AreEqual<BuiltInType>(BuiltInType.String, _nb.Encoding);
      DateTime _dt = new DateTime(2008, 2, 5);
      Recording _testValue = new Recording("Chris Sells", "Chris Sells Live", _dt);
      _nb.Assign2Repository(_testValue);
      Assert.AreEqual<string>(_dt.ToString(CultureInfo.InvariantCulture), _rpo.Buffer);
    }
    #endregion

    #region private
    //private part 
    private class DataRepository : IBindingFactory
    {
      public const string name = "variableName";
      public string Buffer = null;

      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        if (processValueName != name)
          throw new ArgumentOutOfRangeException();
        return new ConsumerBinding<string>(x => Buffer = x, fieldTypeInfo.BuiltInType);
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
      {
        throw new NotImplementedException();
      }
    }
    private class RecordingRepository : IBindingFactory
    {

      public const string name = "variableName";
      public string Buffer = null;

      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo field)
      {
        if (processValueName != name)
          throw new ArgumentOutOfRangeException();
        return new ConsumerBinding<string>(x => Buffer = x, field.BuiltInType);
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo encoding)
      {
        throw new NotImplementedException();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName, BuiltInType encoding)
      {
        throw new NotImplementedException();
      }
    }
    // Simple business object.
    private class Recording
    {
      public Recording() { }
      public Recording(string artistName, string cdName, DateTime release)
      {
        Artist = artistName;
        Name = cdName;
        ReleaseDate = release;
      }
      public string Artist { get; set; }
      public string Name { get; set; }
      public DateTime ReleaseDate { get; set; }
    }
    private class DateFormatter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        Assert.IsInstanceOfType(value, typeof(Recording));
        Assert.IsNull(parameter);
        DateTime date = ((Recording)value).ReleaseDate;
        return date.ToString(CultureInfo.InvariantCulture);
      }
      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotFiniteNumberException();
      }
    }
    #endregion

  } //BindingUnitTest
}
