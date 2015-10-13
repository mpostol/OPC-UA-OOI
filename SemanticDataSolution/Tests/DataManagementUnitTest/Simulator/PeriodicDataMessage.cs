
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{

  /// <summary>
  /// Class PeriodicDataMessage - very simple encodingless example of how to implement <see cref="IPeriodicDataMessage"/>.
  /// </summary>
  internal class PeriodicDataMessage : IPeriodicDataMessage
  {

    #region creators
    /// <summary>
    /// Initializes a new instance of the <see cref="PeriodicDataMessage"/> class for the tests purpose only.
    /// </summary>
    /// <param name="messageContent">Content of the message.</param>
    internal PeriodicDataMessage(object[] messageContent)
      : this(messageContent, Guid.NewGuid())
    { }
    /// <summary>
    /// Initializes a new instance of the <see cref="PeriodicDataMessage"/> class.
    /// </summary>
    /// <param name="messageContent">Content of the message.</param>
    /// <param name="guid">The unique identifier of the recipient DatSet .</param>
    internal PeriodicDataMessage(object[] messageContent, Guid guid)
    {
      m_MessageContent = messageContent;
      m_Guid = guid;
    }
    #endregion

    #region IPeriodicDataMessage
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
    #endregion

    #region private
    private UInt64 m_filter = UInt64.MaxValue;
    private Guid m_Guid;
    private object[] m_MessageContent;
    #endregion

  }

}
