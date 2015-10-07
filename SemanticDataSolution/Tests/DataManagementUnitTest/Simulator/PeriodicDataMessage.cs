
using System;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{
  /// <summary>
  /// Class PeriodicDataMessage - very simple example how to implement <see cref="IPeriodicDataMessage"/>
  /// </summary>
  internal class PeriodicDataMessage: IPeriodicDataMessage
  {
    public PeriodicDataMessage(object[] messageContent)
    {
      m_MessageContent = messageContent;
    }
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
    void IPeriodicDataMessage.UpdateMyValues(Func<int, IBinding> update)
    {
      throw new NotImplementedException();
    }
    public bool IAmDestination(ISemanticData dataId)
    {
      throw new NotImplementedException();
    }
  }
}
