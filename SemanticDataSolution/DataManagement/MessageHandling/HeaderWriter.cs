
using System;
using System.IO;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class HeaderWriter - helper class supporting writing headers content.
  /// </summary>
  internal class HeaderWriter
  {

    #region API
    /// <summary>
    /// Initializes a new instance of the <see cref="HeaderWriter"/> class.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="headerLength">Length of the packet.</param>
    internal HeaderWriter(IBinaryHeaderEncoder writer, ushort headerLength)
    {
      m_Length = headerLength;
      m_Writer = writer;
      m_BeginPosition = CurrentPosition();
      writer.Seek(m_Length, SeekOrigin.Current);
    }
    /// <summary>
    /// Writes the header.
    /// </summary>
    /// <param name="writeHeader">The write header delegate encapsulating functionality used to update the header content.</param>
    internal void WriteHeader(Action<IBinaryHeaderEncoder, ushort> writeHeader)
    {
      long m_CurrentPosition = SetPosition(Convert.ToInt32(m_BeginPosition));
      writeHeader(m_Writer, DataLength(m_CurrentPosition));
      RestorePosition();
    }
    #endregion

    #region private
    //vars
    private IBinaryHeaderEncoder m_Writer;
    private ushort m_Length;
    private long m_BeginPosition;
    //methods
    /// <summary>
    /// The length of the message content.
    /// </summary>
    /// <returns>Calculated message data length.</returns>
    private ushort DataLength(long currentPosition)
    {
      return Convert.ToUInt16(currentPosition - m_BeginPosition - m_Length);
    }
    private long SetPosition(int offset)
    {
      long _ret = m_Writer.Seek(0, SeekOrigin.Current);
      m_Writer.Seek(offset, SeekOrigin.Begin);
      return _ret;
    }
    private long RestorePosition()
    {
      return m_Writer.Seek(0, SeekOrigin.End);
    }
    private long CurrentPosition()
    {
      return m_Writer.Seek(0, SeekOrigin.Current);
    }
    #endregion

  }
}
