using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using UAOOI.SemanticData.UANetworking.Configuration.Properties;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serializers
{
  /// <summary>
  /// Class DataContractSerializers- helper function to serialize or deserialize an object of the specified type using <see cref="DataContractSerializer"/>.
  /// </summary>
  public static class XmlDataContractSerializers
  {

    /// <summary>
    /// Reads the XML stream from <paramref name="fileToRead"/> with an <see cref="DataContractSerializer"/> and returns the deserialized object
    /// </summary>
    /// <typeparam name="type">The type of the deserialized object.</typeparam>
    /// <param name="fileToRead">The <see cref="FileInfo"/> used to open the XML stream.</param>
    /// <param name="trace">Used to write a trace event to the trace listeners using the <see cref="TraceEventType.Verbose"/> event type, event identifier, and message.</param>
    /// <returns>The deserialized object.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static type Load<type>(FileInfo fileToRead, Action<TraceEventType, int, string> trace)
      where type : class, new()
    {
      if (!fileToRead.Exists)
        throw new ArgumentOutOfRangeException(nameof(fileToRead));
      using (XmlTextReader reader = new XmlTextReader(fileToRead.Open(FileMode.Open, FileAccess.Read)))
      {
        DataContractSerializer _serializer = new DataContractSerializer(typeof(type));
        type _graph = _serializer.ReadObject(reader, false) as type;
        trace(TraceEventType.Verbose, 52, String.Format(Resources.InformationFileOpened, fileToRead.FullName));
        return _graph;
      }
    }
    /// <summary>
    /// Serializes the <paramref name="graph"/> object of the specified type and saves it in the specified by the <paramref name="fileToWrite"/> file.
    /// </summary>
    /// <typeparam name="type">The type of the <paramref name="graph"/> to be serialized.</typeparam>
    /// <param name="fileToWrite">The file to write all the object data (starting XML element, content, and closing element) with an System.Xml.XmlWriter..</param>
    /// <param name="graph">The object that contains the data to write to the stream.</param>
    /// <param name="trace">Used to write a trace event to the trace listeners using the <see cref="TraceEventType.Verbose"/> event type, event identifier, and message.</param>
    /// <exception cref="ArgumentNullException">
    /// </exception>
    public static void Save<type>(FileInfo fileToWrite, type graph, Action<TraceEventType, int, string> trace)
    {
      if (fileToWrite == null)
        throw new ArgumentNullException(nameof(fileToWrite));
      if (graph == null)
        throw new ArgumentNullException(nameof(graph));
      DataContractSerializer _deserializer = new DataContractSerializer(typeof(type));
      Formatting _formatting = new Formatting() { };
      using (XmlTextWriter _writer = new XmlTextWriter(fileToWrite.Open(FileMode.Create, FileAccess.Write), Encoding.UTF8) { Formatting = Formatting.Indented, Indentation = 2 })
        _deserializer.WriteObject(_writer, graph);
      trace(TraceEventType.Verbose, 52, String.Format(Resources.InformationFileSaved, fileToWrite.FullName));
    }

  }
}
