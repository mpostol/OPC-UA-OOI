
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows.Data;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class BindingUnitTest
  {

    #region TestMethods
    [TestMethod]
    [TestCategory("DataManagement_Binding")]
    public void TestMethod1()
    {
      Binding<int> _nb = new Binding<int>(x => { });
      Assert.IsNotNull(_nb);
    }
    [TestMethod]
    [TestCategory("DataManagement_Binding")]
    public void DataRepositoryTestMethod()
    {
      DataRepository _repo = new DataRepository();
      IBinding _nb = _repo.GetBinding(DataRepository.name);
      Assert.IsNotNull(_nb);
      Assert.IsNotNull(_nb.TargetType);
      _nb.Converter = null;
      Assert.AreSame(typeof(string), _nb.TargetType);
      string _testValue = "123wjkqjwkqjwjqjwqwjwqkwqjw";
      _nb.Assign2Repository(_testValue);
      Assert.AreEqual<string>(_testValue, _repo.Buffer);
    }
    [TestMethod]
    [TestCategory("DataManagement_Binding")]
    public void RecordingRepositoryTestMethod()
    {
      RecordingRepository _rpo = new RecordingRepository();
      IBinding _nb = _rpo.GetBinding(DataRepository.name);
      Assert.IsNotNull(_nb);
      Assert.IsNotNull(_nb.TargetType);
      _nb.Converter = new DateFormatter();
      Assert.AreSame(typeof(string), _nb.TargetType);
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
      public IBinding GetBinding(string variableName)
      {
        if (variableName != name)
          throw new ArgumentOutOfRangeException();
        return new Binding<string>(x => Buffer = x);
      }
      public string Buffer = null;
    }
    private class RecordingRepository : IBindingFactory
    {
      public const string name = "variableName";
      public IBinding GetBinding(string variableName)
      {
        if (variableName != name)
          throw new ArgumentOutOfRangeException();
        return new Binding<string>(x => Buffer = x);
      }
      public string Buffer = null;
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
