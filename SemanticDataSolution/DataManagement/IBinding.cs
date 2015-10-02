using System;
using System.Windows.Data;

namespace UAOOI.SemanticData.DataManagement
{

  public interface IBinding
  {
    IValueConverter Converter { set; }
    Type RepositoryType { get; }
    void Assign2Repository(object value);
  }

}
