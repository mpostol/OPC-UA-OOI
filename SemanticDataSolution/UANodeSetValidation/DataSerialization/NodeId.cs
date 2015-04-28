using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public class NodeId
  {

    /// <summary>
    /// Initializes the object with default values.
    /// </summary>
    /// <remarks>
    /// Creates a new instance of the class which will have the default values. The actual
    /// Node Id will need to be defined as this constructor does not specify the id.
    /// </remarks>
    internal NodeId()
    {
      Initialize();
    }
    /// <summary>
    /// Initializes a numeric node identifier.
    /// </summary>
    /// <remarks>
    /// Creates a new NodeId that will have a numeric (unsigned-int) id
    /// </remarks>
    /// <param name="value">The numeric value of the id</param>
    public NodeId(uint value)
    {
      m_namespaceIndex = 0;
      m_identifierType = IdType.Numeric;
      m_identifier = value;
    }
    /// <summary>
    /// Initializes a guid node identifier with a namespace index.
    /// </summary>
    /// <remarks>
    /// Creates a new NodeId that will use a numeric (unsigned int) for its Id, but also
    /// specifies which namespace this node should belong to.
    /// </remarks>
    /// <param name="value">The new (numeric) Id for the node being created</param>
    /// <param name="namespaceIndex">The index of the namespace that this node should belong to</param>
    /// <seealso cref="SetNamespaceIndex"/>
    public NodeId(uint value, ushort namespaceIndex)
    {
      m_namespaceIndex = namespaceIndex;
      m_identifierType = IdType.Numeric;
      m_identifier = value;
    }
    /// <summary>
    /// Initializes a string node identifier with a namespace index.
    /// </summary>
    /// <remarks>
    /// Creates a new NodeId that will use a string for its Id, but also
    /// specifies if the Id is a URI, and which namespace this node belongs to.
    /// </remarks>
    /// <param name="value">The new (string) Id for the node being created</param>
    /// <param name="namespaceIndex">The index of the namespace that this node belongs to</param>
    public NodeId(string value, ushort namespaceIndex)
    {
      m_namespaceIndex = namespaceIndex;
      m_identifierType = IdType.String;
      m_identifier = value;
    }
    /// <summary>
    /// Initializes a guid node identifier.
    /// </summary>
    /// <remarks>
    /// Creates a new node whose Id will be a <see cref="Guid"/>.
    /// </remarks>
    /// <param name="value">The new Guid value of this nodes Id.</param>
    public NodeId(Guid value)
    {
      m_namespaceIndex = 0;
      m_identifierType = IdType.Guid;
      m_identifier = value;
    }
    /// <summary>
    /// Initializes a guid node identifier.
    /// </summary>
    /// <remarks>
    /// Creates a new node whose Id will be a <see cref="Guid"/>.
    /// </remarks>
    /// <param name="value">The new Guid value of this nodes Id.</param>
    /// <param name="namespaceIndex">The index of the namespace that this node belongs to</param>
    public NodeId(Guid value, ushort namespaceIndex)
    {
      m_namespaceIndex = namespaceIndex;
      m_identifierType = IdType.Guid;
      m_identifier = value;
    }
    /// <summary>
    /// Initializes a guid node identifier.
    /// </summary>
    /// <remarks>
    /// Creates a new node whose Id will be a series of <see cref="Byte"/>.
    /// </remarks>
    /// <param name="value">An array of <see cref="Byte"/> that will become this Node's ID</param>
    public NodeId(byte[] value)
    {
      m_namespaceIndex = 0;
      m_identifierType = IdType.Opaque;
      m_identifier = null;
      if (value != null)
      {
        byte[] copy = new byte[value.Length];
        Array.Copy(value, copy, value.Length);
        m_identifier = copy;
      }
    }
    /// <summary>
    /// Initializes an opaque node identifier with a namespace index.
    /// </summary>
    /// <remarks>
    /// Creates a new node whose Id will be a series of <see cref="Byte"/>, while specifying
    /// the index of the namespace that this node belongs to.
    /// </remarks>
    /// <param name="value">An array of <see cref="Byte"/> that will become this Node's ID</param>
    /// <param name="namespaceIndex">The index of the namespace that this node belongs to</param>
    public NodeId(byte[] value, ushort namespaceIndex)
    {
      m_namespaceIndex = namespaceIndex;
      m_identifierType = IdType.Opaque;
      m_identifier = null;
      if (value != null)
      {
        byte[] copy = new byte[value.Length];
        Array.Copy(value, copy, value.Length);
        m_identifier = copy;
      }
    }
    /// <summary>
    /// Initializes a node id by parsing a node id string.
    /// </summary>
    /// <remarks>
    /// Creates a new node with a String id.
    /// </remarks>
    /// <param name="text">The string id of this new node</param>
    public NodeId(string text)
    {
      NodeId nodeId = NodeId.Parse(text);
      m_namespaceIndex = nodeId.NamespaceIndex;
      m_identifierType = nodeId.IdType;
      m_identifier = nodeId.Identifier;
    }
    /// <summary>
    /// Initializes a node identifier with a namespace index.
    /// </summary>
    /// <remarks>
    /// Throws an exception if the identifier type is not supported.
    /// </remarks>
    /// <param name="value">The identifier</param>
    /// <param name="namespaceIndex">The index of the namespace that qualifies the node</param>
    public NodeId(object value, ushort namespaceIndex)
    {
      m_namespaceIndex = namespaceIndex;

      if (value is uint)
      {
        SetIdentifier(IdType.Numeric, value);
        return;
      }

      if (value == null || value is string)
      {
        SetIdentifier(IdType.String, value);
        return;
      }

      if (value is Guid)
      {
        SetIdentifier(IdType.Guid, value);
        return;
      }

      if (value is byte[])
      {
        SetIdentifier(IdType.Opaque, value);
        return;
      }
    }
    /// <summary>
    /// Returns an instance of a null NodeId.
    /// </summary>
    public static NodeId Null
    {
      get { return s_Null; }
    }
    /// <summary>
    /// Parses a node id string and returns a node id object.
    /// </summary>
    /// <remarks>
    /// Parses a NodeId String and returns a NodeId object
    /// </remarks>
    /// <param name="text">The NodeId value as a string.</param>
    /// <exception cref="ServiceResultException">Thrown under a variety of circumstances, each time with a specific message.</exception>
    public static NodeId Parse(string text)
    {
      try
      {
        if (String.IsNullOrEmpty(text))
          return NodeId.Null;
        ushort namespaceIndex = 0;
        // parse the namespace index if present.
        if (text.StartsWith("ns=", StringComparison.Ordinal))
        {
          int index = text.IndexOf(';');
          if (index == -1)
            throw new ServiceResultException
              (TraceMessage.BuildErrorTraceMessage(BuildError.BadNodeIdInvalid, String.Format("Cannot parse node id text: '{0}'", text)), "BuildError_BadNodeIdInvalid");
          namespaceIndex = Convert.ToUInt16(text.Substring(3, index - 3), CultureInfo.InvariantCulture);
          text = text.Substring(index + 1);
        }
        // parse numeric node identifier.
        if (text.StartsWith("i=", StringComparison.Ordinal))
          return new NodeId(Convert.ToUInt32(text.Substring(2), CultureInfo.InvariantCulture), namespaceIndex);
        // parse string node identifier.
        if (text.StartsWith("s=", StringComparison.Ordinal))
          return new NodeId(text.Substring(2), namespaceIndex);
        // parse guid node identifier.
        if (text.StartsWith("g=", StringComparison.Ordinal))
          return new NodeId(new Guid(text.Substring(2)), namespaceIndex);
        // parse opaque node identifier.
        if (text.StartsWith("b=", StringComparison.Ordinal))
          return new NodeId(Convert.FromBase64String(text.Substring(2)), namespaceIndex);
        // treat as a string identifier if a namespace was specified.
        if (namespaceIndex != 0)
          return new NodeId(text, namespaceIndex);
        // treat as URI identifier.
        return new NodeId(text, 0);
      }
      catch (Exception e)
      {
        throw new ServiceResultException
          (TraceMessage.BuildErrorTraceMessage(BuildError.BadNodeIdInvalid, String.Format("Cannot parse node id text: '{0}'", text)), "BuildError_BadNodeIdInvalid", e);
      }
    }
    /// <summary>
    /// Initializes the object during deserialization.
    /// </summary>
    private void Initialize()
    {
      m_namespaceIndex = 0;
      m_identifierType = IdType.Numeric;
      m_identifier = null;
    }
    /// <summary>
    /// Updates the identifier.
    /// </summary>
    internal void SetIdentifier(IdType idType, object value)
    {
      m_identifierType = idType;
      switch (idType)
      {
        case IdType.Opaque:
          throw new NotImplementedException(" m_identifier = Utils.Clone(value);");
        default:
          m_identifier = value;
          break;
      }
    }
    /// <summary>
    /// Updates the identifier.
    /// </summary>
    internal void SetIdentifier(string value, IdType idType)
    {
      m_identifierType = idType;
      SetIdentifier(IdType.String, value);
    }
    /// The index of the namespace URI in the server's namespace array.
    /// </summary>
    /// <remarks>
    /// The index of the namespace URI in the server's namespace array.
    /// </remarks>
    public ushort NamespaceIndex
    {
      get { return m_namespaceIndex; }
    }
    /// <summary>
    /// The type of node identifier used.
    /// </summary>
    /// <remarks>
    /// Returns the type of Id, whether it is:
    /// <list type="bullet">
    /// <item><see cref="Guid"/></item>
    /// <item><see cref="string"/></item>
    /// <item><see cref="byte"/>[]</item>
    /// </list>
    /// </remarks>
    /// <seealso cref="IdType"/>
    public IdType IdType
    {
      get { return m_identifierType; }
    }
    /// <summary>
    /// The node identifier.
    /// </summary>
    /// <remarks>
    /// Returns the Id in its native format, i.e. UInt, GUID, String etc.
    /// </remarks>
    public object Identifier
    {
      get
      {
        if (m_identifier == null)
        {
          switch (m_identifierType)
          {
            case IdType.Numeric: { return (uint)0; }
            case IdType.Guid: { return Guid.Empty; }
          }
        }
        return m_identifier;
      }
    }
    //private
    private ushort m_namespaceIndex;
    private IdType m_identifierType;
    private object m_identifier;
    private static NodeId s_Null = new NodeId();

  }
  public enum IdType
  {
    /// <summary>
    /// The identifier is a numeric value. 0 is a null value.
    /// </summary>
    Numeric = 0,
    /// <summary>
    /// The identifier is a string value. An empty string is a null value.
    /// </summary>
    String = 1,
    /// <summary>
    /// The identifier is a 16 byte structure. 16 zero bytes is a null value.
    /// </summary>
    Guid = 2,
    /// <summary>
    /// The identifier is an array of bytes. A zero length array is a null value.
    /// </summary>
    Opaque = 3,
  }

}
