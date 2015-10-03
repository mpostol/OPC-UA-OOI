
using System;
using System.Windows.Data;

namespace UAOOI.SemanticData.DataManagement
{
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
