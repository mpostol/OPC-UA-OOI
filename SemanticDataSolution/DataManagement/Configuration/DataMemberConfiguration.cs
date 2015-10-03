
using System;

namespace UAOOI.SemanticData.DataManagement.Configuration
{
  [Serializable]
  public class DataMemberConfiguration
  {
    internal IBinding DataMemberFactory(DataSetConfiguration parent, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      IBinding _binding = bindingFactory.GetBinding(parent.RepositoryGroup, this.ProcessValueName);
      encodingFactory.UpdateValueConverter(_binding, parent.RepositoryGroup, this.SourceEncoding);
      return _binding;
    }
    public string SymbolicName { get; set; }
    public string ProcessValueName { get; set; }
    public string SourceEncoding { get; set; }

  }
}
