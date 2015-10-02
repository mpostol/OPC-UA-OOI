
using System;
using System.Windows.Data;

namespace UAOOI.SemanticData.DataManagement
{
  public class Binding<type> : IBinding
  {
    public Binding(Action<type> assign)
    {
      RepositoryType = typeof(type);
      m_Assign = assign;
    }
    public IValueConverter Converter { private get; set; }
    public Type RepositoryType { get; private set; }
    public void Assign2Repository(object value)
    {
      if (Converter == null)
        m_Assign((type)value);
      else
        m_Assign((type)Converter.Convert(value, RepositoryType, null, null));
    }

    private Action<type> m_Assign;
  }



}
