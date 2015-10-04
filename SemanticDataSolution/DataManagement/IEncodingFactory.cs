
using System;
using System.Windows.Data;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Interface IEncodingFactory - provides functionality to lookup a dictionary containing value converters.
  /// It is expected that the encoding/decoding functionality is provided outside of this library
  /// The interface is used for late binding to inject dependency on the external library. 
  /// </summary>
  public interface IEncodingFactory
  {

    /// <summary>
    /// Updates the value converter.
    /// </summary>
    /// <param name="converter">The converter.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="sourceEncoding">The source encoding.</param>
    void UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding);

  }
}
