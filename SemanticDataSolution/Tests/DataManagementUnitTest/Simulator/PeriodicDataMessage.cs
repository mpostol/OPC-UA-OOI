
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

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
    public PeriodicDataMessage(object[] messageContent, Guid guid): this (messageContent)
    {
      m_Guid = guid;
    }
    public void UpdateMyValues(Func<int, IConsumerBinding> update)
    {
      UInt64 _mask = 0x1;
      int _associationIndex = 0;
      for (int i = 0; i < m_MessageContent.Length; i++)
      {
        if ((m_filter & _mask) > 0)
          update(_associationIndex).Assign2Repository(m_MessageContent[i]);
        _associationIndex++;
        _mask = _mask << 1;
      }
    }
    public bool IAmDestination(ISemanticData dataId)
    {
      return dataId.Guid == m_Guid;
    }
    private UInt64 m_filter = UInt64.MaxValue;
    private Guid m_Guid;
    private object[] m_MessageContent;

  }
}
