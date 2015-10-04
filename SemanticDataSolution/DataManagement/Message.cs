using System;

namespace UAOOI.SemanticData.DataManagement
{
  internal class Message
  {
    internal void UpdateMyValues(Func<int, IBinding> update)
    {
      UInt64 _mask = 0x1;
      int _associationIndex = 0;
      for (int i = 0; i < m_MessageContent.Length; i++)
      {
        if ((m_filter & _mask) == 1)
          update(_associationIndex).Assign2Repository(m_MessageContent[i]);
        _associationIndex++;
        _mask = _mask << 1;
      }
    }
    private UInt64 m_filter = UInt64.MaxValue;
    private object[] m_MessageContent;

  }
}
