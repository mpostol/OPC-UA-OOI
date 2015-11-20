using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UAOOI.SemanticData.DataManagement.Encoding
{
  internal interface IBinaryDecoder
  {
    byte ReadByte();
    int ReadInt32();
    bool ReadBoolean();
    sbyte ReadSByte();
    short ReadInt16();
    ushort ReadUInt16();
    uint ReadUInt32();
    long ReadInt64();
    ulong ReadUInt64();
    float ReadFloat();
    double ReadDouble();
    string ReadString();
    DateTime ReadDateTime();
    Guid ReadGuid();
    byte[] ReadByteString();
    XmlElement ReadXmlElement();
    NodeId ReadNodeId();
    ExpandedNodeId ReadExpandedNodeId();
    StatusCode ReadStatusCode();
    QualifiedName ReadQualifiedName();
    LocalizedText ReadLocalizedText();
    ExtensionObject ReadExtensionObject();
    DataValue ReadDataValue();
    Variant ReadVariant();
    List<int> ReadInt32Array();

  }
}
