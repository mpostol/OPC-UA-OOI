using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.DataManagement
{
  public class DataSet
  {
    public DataMember[] Members;
  }
  public class DataMember
  {
    public string SymbolicName;
    public string ProcessValueName;
  }
}
