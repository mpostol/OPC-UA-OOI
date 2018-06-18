
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Windows.Data;
using UAOOI.Networking.SemanticData.DataRepository;
using UAOOI.Networking.SemanticData.Encoding;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.UnitTest
{
  [TestClass]
  public class EncodingFactoryUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_IEncodingFactory")]
    public void UpdateValueConverterTestMethod2()
    {
      EncodingFactory _ief = new EncodingFactory();
      Assert.IsNotNull(_ief);
      MyBinding _ibf = new MyBinding();
      _ief.UpdateValueConverter(_ibf, String.Empty, new UATypeInfo(BuiltInType.Null));
      _ibf.Assign2Repository(null);
    }
    //private
    private class EncodingFactory : IEncodingFactory
    {
      public void UpdateValueConverter(IBinding binding, string repositoryGroup, UATypeInfo sourceEncoding)
      {
        Assert.IsNotNull(binding);
        binding.Parameter = "Conversion parameter";
        binding.Converter = new IVC();
        binding.Culture = CultureInfo.InvariantCulture;
        binding.FallbackValue = null;
      }
      public IUADecoder UADecoder
      {
        get
        {
          return m_UADecoder;
        }
      }
      public IUAEncoder UAEncoder
      {
        get
        {
          throw new NotImplementedException();
        }
      }
      private readonly IUADecoder m_UADecoder = new Helpers.UABinaryDecoderImplementation();
    }
    private class IVC : IValueConverter
    {

      /// <summary>
      /// Converts the specified value.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <param name="targetType">Type of the target.</param>
      /// <param name="parameter">The parameter.</param>
      /// <param name="culture">The culture.</param>
      /// <returns>System.Object.</returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        throw new NotImplementedException();
      }

      public object Convert(object value, Type targetType, object fallBack, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }
    private class MyBinding : IBinding
    {
      public IValueConverter Converter
      {
        set;
        private get;
      }
      public UATypeInfo Encoding
      {
        get { throw new NotImplementedException(); }
      }
      public object Parameter
      {
        set;
        get;
      }
      public System.Globalization.CultureInfo Culture
      {
        set;
        private get;
      }
      public object FallbackValue { get; set; }

      public void Assign2Repository(object value)
      {
        Assert.IsNotNull(Converter);
        Assert.IsNotNull(Parameter);
        Assert.IsNotNull(Culture);
        Assert.IsNull(FallbackValue);
      }
      public void OnEnabling()
      {
        throw new NotImplementedException();
      }
      public void OnDisabling()
      {
        throw new NotImplementedException();
      }
    }

  }
}
