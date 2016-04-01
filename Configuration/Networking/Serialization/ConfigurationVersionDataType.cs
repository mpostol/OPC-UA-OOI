using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAOOI.Configuration.Networking.Serialization
{
  public partial class ConfigurationVersionDataType
  {
    public override string ToString()
    {
      return $"{MajorVersion}.{MinorVersion}";
    }
  }
}
