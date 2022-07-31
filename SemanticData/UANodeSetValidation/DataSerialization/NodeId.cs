//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Globalization;
using System.Text;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization
{
  /// <summary>
  /// Stores an identifier for a node in a server's address space.
  /// </summary>
  /// <remarks>
  /// <para>
  /// <b>Please refer to OPC Specifications</b>:
  /// <list type="bullet">
  /// <item><b>Address Space Model</b> section <b>8.2</b></item>
  /// <item><b>Address Space Model</b> section <b>5.2.2</b></item>
  /// </list>
  /// </para>
  /// <para>
  /// Stores the id of a Node, which resides within the server's address space.
  /// <br/></para>
  /// <para>
  /// The NodeId can be either:
  /// <list type="bullet">
  /// <item><see cref="uint"/></item>
  /// <item><see cref="Guid"/></item>
  /// <item><see cref="string"/></item>
  /// <item><see cref="byte"/>[]</item>
  /// </list>
  /// <br/></para>
  /// <note>
  /// <b>Important:</b> Keep in mind that the actual ID's of nodes should be unique such that no two
  /// nodes within an address-space share the same ID's.
  /// </note>
  /// <para>
  /// The NodeId can be assigned to a particular namespace index. This index is merely just a number and does
  /// not represent some index within a collection that this node has any knowledge of. The assumption is
  /// that the host of this object will manage that directly.
  /// <br/></para>
  /// </remarks>
  public partial class NodeId : IFormattable, IEquatable<NodeId>, IComparable
  {
    #region constructors

    /// <summary>
    /// Initializes the object with default values.
    /// </summary>
    /// <remarks>
    /// Creates a new instance of the class which will have the default values. The actual
    /// NodeId will need to be defined as this constructor does not specify the id.
    /// </remarks>
    internal NodeId()
    {
      m_namespaceIndex = 0;
      m_identifierType = IdType.Numeric_0;
      m_identifierPart = null;
      m_GlobalHashCode++;
    }

    /// <summary>
    /// Creates a deep copy of the value.
    /// </summary>
    /// <remarks>
    /// Creates a new NodeId by copying the properties of the node specified in the parameter.
    /// </remarks>
    /// <param name="value">The NodeId object whose properties will be copied.</param>
    /// <exception cref="ArgumentNullException">Thrown when <i>value</i> is null</exception>
    public NodeId(NodeId value)
    {
      if (value == null) throw new ArgumentNullException("value");
      m_namespaceIndex = value.m_namespaceIndex;
      m_identifierType = value.m_identifierType;
      m_identifierPart = value.MemberwiseClone();
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
      m_identifierType = IdType.Numeric_0;
      m_identifierPart = value;
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
      m_identifierType = IdType.Numeric_0;
      m_identifierPart = value;
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
      m_identifierType = IdType.String_1;
      m_identifierPart = value;
    }

    /// <summary>
    /// Initializes a guid node identifier.
    /// </summary>
    /// <remarks>
    /// Creates a new node whose Id will be a <see cref="Guid"/>.
    /// </remarks>
    /// <param name="value">The new Guid value of this nodes Id.</param>
    public NodeId(System.Guid value)
    {
      m_namespaceIndex = 0;
      m_identifierType = IdType.Guid_2;
      m_identifierPart = value;
    }

    /// <summary>
    /// Initializes a guid node identifier.
    /// </summary>
    /// <remarks>
    /// Creates a new node whose Id will be a <see cref="Guid"/>.
    /// </remarks>
    /// <param name="value">The new Guid value of this nodes Id.</param>
    /// <param name="namespaceIndex">The index of the namespace that this node belongs to</param>
    public NodeId(global::System.Guid value, ushort namespaceIndex)
    {
      m_namespaceIndex = namespaceIndex;
      m_identifierType = IdType.Guid_2;
      m_identifierPart = value;
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
      m_identifierType = IdType.Opaque_3;
      m_identifierPart = null;
      if (value != null)
      {
        byte[] copy = new byte[value.Length];
        Array.Copy(value, copy, value.Length);
        m_identifierPart = copy;
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
      m_identifierType = IdType.Opaque_3;
      m_identifierPart = null;
      if (value != null)
      {
        byte[] copy = new byte[value.Length];
        Array.Copy(value, copy, value.Length);
        m_identifierPart = copy;
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
      m_identifierPart = nodeId.IdentifierPart;
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
        SetIdentifier(IdType.Numeric_0, value);
        return;
      }
      if (value == null || value is string)
      {
        SetIdentifier(IdType.String_1, value);
        return;
      }
      if (value is System.Guid)
      {
        SetIdentifier(IdType.Guid_2, value);
        return;
      }
      if (value is byte[])
      {
        SetIdentifier(IdType.Opaque_3, value);
        return;
      }
    }

    #endregion constructors

    #region public

    /// <summary>
    /// Converts an integer to a numeric node identifier.
    /// </summary>
    /// <param name="value">The <see cref="uint" /> to compare this node to.</param>
    /// <returns>The <see cref="NodeId"/> object as the result of the conversion.</returns>
    /// <remarks>Converts an integer to a numeric node identifier for comparisons.</remarks>
    public static implicit operator NodeId(uint value)
    {
      return new NodeId(value);
    }

    /// <summary>
    /// Returns an instance of a null NodeId.
    /// </summary>
    /// <summary>
    /// Checks if the node id represents a 'Null' node id.
    /// </summary>
    /// <remarks>
    /// Returns a true/false value to indicate if the specified NodeId is null.
    /// </remarks>
    /// <param name="nodeId">The NodeId to validate</param>
    public static bool IsNull(NodeId nodeId)
    {
      if (nodeId == null)
        return true;
      return nodeId.IsNullNodeId;
    }

    /// <summary>
    /// Gets the <see cref="NodeId"/> representing <b>null</b>.
    /// </summary>
    /// <value>The null.</value>
    public static NodeId Null => s_Null;

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
              (TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdInvalidSyntax, String.Format("Cannot parse node id text: '{0}'", text)), "BuildError_BadNodeIdInvalid");
          namespaceIndex = Convert.ToUInt16(text.Substring(3, index - 3), CultureInfo.InvariantCulture);
          text = text.Substring(index + 1);
        }
        // parse numeric node identifier.
        if (text.StartsWith("i=", StringComparison.Ordinal))
          return new NodeId(Convert.ToUInt32(text.Substring(2), CultureInfo.InvariantCulture), namespaceIndex);
        // parse string node identifier.
        if (text.StartsWith("s=", StringComparison.Ordinal))
          return new NodeId(text.Substring(2), namespaceIndex);
        // parse GUID node identifier.
        if (text.StartsWith("g=", StringComparison.Ordinal))
          return new NodeId(new System.Guid(text.Substring(2)), namespaceIndex);
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
          (TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdInvalidSyntax, String.Format("Cannot parse node id text: '{0}'", text)), "BuildError_BadNodeIdInvalid", e);
      }
    }

    /// <summary>
    /// Updates the namespace index.
    /// </summary>
    internal void SetNamespaceIndex(ushort value)
    {
      m_namespaceIndex = value;
    }

    /// <summary>
    /// Updates the identifier.
    /// </summary>
    internal void SetIdentifier(IdType idType, object value)
    {
      m_identifierType = idType;
      switch (idType)
      {
        case IdType.Opaque_3:
          throw new NotImplementedException(" m_identifier = Utils.Clone(value);");
        default:
          m_identifierPart = value;
          break;
      }
    }

    /// <summary>
    /// Updates the identifier.
    /// </summary>
    internal void SetIdentifier(string value, IdType idType)
    {
      m_identifierType = idType;
      SetIdentifier(IdType.String_1, value);
    }

    ///<summary>
    /// The index of the namespace URI in the server's namespace array.
    /// </summary>
    /// <remarks>
    /// The index of the namespace URI in the server's namespace array.
    /// </remarks>
    public ushort NamespaceIndex => m_namespaceIndex;

    /// <summary>
    /// The type of node identifier used.
    /// </summary>
    /// <remarks>
    /// Returns the type of Id, whether it is:
    /// <list type="bullet">
    /// <item><see cref="uint"/></item>
    /// <item><see cref="Guid"/></item>
    /// <item><see cref="string"/></item>
    /// <item><see cref="byte"/>[]</item>
    /// </list>
    /// </remarks>
    /// <seealso cref="IdType"/>
    public IdType IdType => m_identifierType;

    /// <summary>
    /// The node identifier.
    /// </summary>
    /// <remarks>
    /// Returns the Id in its native format, i.e. UInt, GUID, String etc.
    /// </remarks>
    public object IdentifierPart
    {
      get
      {
        if (m_identifierPart == null)
        {
          switch (m_identifierType)
          {
            case IdType.Numeric_0: { return (uint)0; }
            case IdType.Guid_2: { return global::System.Guid.Empty; }
          }
        }
        return m_identifierPart;
      }
    }

    /// <summary>
    /// Whether the object represents a Null NodeId.
    /// </summary>
    /// <remarks>
    /// Whether the NodeId represents a Null NodeId.
    /// </remarks>
    public bool IsNullNodeId
    {
      get
      {
        // non-zero namespace means it can't be null.
        if (m_namespaceIndex != 0)
          return false;
        // the definition of a null identifier depends on the identifier type.
        if (IdentifierPart == null)
          return true;
        bool _ret = true;
        switch (m_identifierType)
        {
          case IdType.Numeric_0:
            _ret = !!IdentifierPart.Equals((uint)0);
            break;

          case IdType.String_1:
            _ret = String.IsNullOrEmpty((string)IdentifierPart);
            break;

          case IdType.Guid_2:
            _ret = IdentifierPart.Equals(System.Guid.Empty);
            break;

          case IdType.Opaque_3:
            _ret = !(IdentifierPart != null && ((byte[])IdentifierPart).Length > 0);
            break;
        }
        // must be null.
        return _ret;
      }
    }

    /// <summary>
    /// Returns true if the objects are equal.
    /// </summary>
    /// <remarks>
    /// Returns true if the objects are equal.
    /// </remarks>
    public static bool operator ==(NodeId value1, object value2)
    {
      if (Object.ReferenceEquals(value1, null))
        return Object.ReferenceEquals(value2, null);
      return (value1.CompareTo(value2) == 0);
    }

    /// <summary>
    /// Returns true if the objects are not equal.
    /// </summary>
    /// <remarks>
    /// Returns true if the objects are not equal.
    /// </remarks>
    public static bool operator !=(NodeId value1, object value2)
    {
      if (Object.ReferenceEquals(value1, null))
        return !Object.ReferenceEquals(value2, null);
      return (value1.CompareTo(value2) != 0);
    }

    /// <summary>
    /// Converts an identifier and a namespaceUri to a local NodeId using the namespaceTable.
    /// </summary>
    /// <param name="identifier">The identifier for the node.</param>
    /// <param name="namespaceUri">The URI to look up.</param>
    /// <param name="namespaceTable">The table to use for the URI lookup.</param>
    /// <returns>A local NodeId</returns>
    /// <exception cref="ServiceResultException">Thrown when the namespace cannot be found</exception>
    public static NodeId Create(object identifier, string namespaceUri, INamespaceTable namespaceTable)
    {
      int index = -1;
      if (namespaceTable != null)
        index = namespaceTable.GetURIIndex(new Uri(namespaceUri));
      if (index < 0)
        throw new ServiceResultException(TraceMessage.BuildErrorTraceMessage(BuildError.NodeIdNotDefined, $"NamespaceUri ({namespaceUri}) is not in the namespace table."), "BuildError_BadNodeIdInvalid");
      return new NodeId(identifier, (ushort)index);
    }

    #region Format()

    /// <summary>
    /// Formats a node id as a string.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Formats a NodeId as a string.
    /// <br/></para>
    /// <para>
    /// An example of this would be:
    /// <br/></para>
    /// <para>
    /// NodeId = "hello123"<br/>
    /// NamespaceId = 1;<br/>
    /// <br/> This would translate into:<br/>
    /// ns=1;s=hello123
    /// <br/></para>
    /// </remarks>
    public string Format()
    {
      StringBuilder buffer = new StringBuilder();
      Format(buffer);
      return buffer.ToString();
    }

    /// <summary>
    /// Formats the NodeId as a string and appends it to the buffer.
    /// </summary>
    public void Format(StringBuilder buffer)
    {
      Format(buffer, IdentifierPart, m_identifierType, m_namespaceIndex);
    }

    /// <summary>
    /// Formats the NodeId as a string and appends it to the buffer.
    /// </summary>
    public static void Format(StringBuilder buffer, object identifier, IdType identifierType, ushort namespaceIndex)
    {
      if (namespaceIndex != 0)
        buffer.AppendFormat(CultureInfo.InvariantCulture, "ns={0};", namespaceIndex);
      // add identifier type prefix.
      switch (identifierType)
      {
        case IdType.Numeric_0:
          buffer.Append("i=");
          break;

        case IdType.String_1:
          buffer.Append("s=");
          break;

        case IdType.Guid_2:
          buffer.Append("g=");
          break;

        case IdType.Opaque_3:
          buffer.Append("b=");
          break;
      }
      // add identifier.
      FormatIdentifier(buffer, identifier, identifierType);
    }

    #endregion Format()

    #endregion public

    #region IComparable

    /// <summary>
    /// Compares the current instance to the object.
    /// </summary>
    /// <remarks>
    /// Enables this object type to be compared to other types of object.
    /// </remarks>
    public int CompareTo(object obj)
    {
      // check for null.
      if (Object.ReferenceEquals(obj, null))
        return -1;
      // check for reference comparisons.
      if (Object.ReferenceEquals(this, obj))
        return 0;
      ushort namespaceIndex = this.m_namespaceIndex;
      IdType idType = this.m_identifierType;
      object id = null;
      // check for expanded node ids.
      NodeId nodeId = obj as NodeId;
      if (nodeId != null)
      {
        namespaceIndex = nodeId.NamespaceIndex;
        idType = nodeId.IdType;
        id = nodeId.IdentifierPart;
      }
      else
      {
        UInt32? uid = obj as UInt32?;
        // check for numeric contains.
        if (uid != null)
        {
          if (namespaceIndex != 0 || idType != IdType.Numeric_0)
            return -1;
          uint id1 = (uint)m_identifierPart;
          uint id2 = uid.Value;
          if (id1 == id2)
            return 0;
          return (id1 < id2) ? -1 : +1;
        }
        ExpandedNodeId expandedId = obj as ExpandedNodeId;
        if (!Object.ReferenceEquals(expandedId, null))
        {
          if (expandedId.IsAbsolute)
            return -1;
          namespaceIndex = expandedId.NamespaceIndex;
          idType = expandedId.IdType;
          id = expandedId.IdentifierPart;
        }
      }
      // check for different namespace.
      if (namespaceIndex != m_namespaceIndex)
        return (m_namespaceIndex < namespaceIndex) ? -1 : +1;
      // check for different id type.
      if (idType != m_identifierType)
        return (m_identifierType < idType) ? -1 : +1;
      // check for two nulls.
      if (IdentifierPart == null && id == null)
        return 0;
      // check for a single null.
      if (IdentifierPart == null && id != null)
      {
        switch (idType)
        {
          case IdType.String_1:
            string stringId = id as string;
            if (stringId.Length == 0)
              return 0;
            break;

          case IdType.Opaque_3:
            byte[] opaqueId = id as byte[];
            if (opaqueId.Length == 0)
              return 0;
            break;

          case IdType.Numeric_0:
            uint? numericId = id as uint?;
            if (numericId.Value == 0)
              return 0;
            break;
        }
        return -1;
      }
      else if (IdentifierPart != null && id == null) // check for a single null.
      {
        switch (idType)
        {
          case IdType.String_1:
            string stringId = IdentifierPart as string;
            if (stringId.Length == 0)
              return 0;
            break;

          case IdType.Opaque_3:
            byte[] opaqueId = IdentifierPart as byte[];
            if (opaqueId.Length == 0)
              return 0;
            break;

          case IdType.Numeric_0:
            uint? numericId = IdentifierPart as uint?;
            if (numericId.Value == 0)
              return 0;
            break;
        }
        return +1;
      }
      // compare ids.
      switch (idType)
      {
        case IdType.Numeric_0:
          {
            uint id1 = (uint)IdentifierPart;
            uint id2 = (uint)id;
            if (id1 == id2)
              return 0;
            return (id1 < id2) ? -1 : +1;
          }
        case IdType.String_1:
          {
            string id1 = (string)IdentifierPart;
            string id2 = (string)id;
            return String.CompareOrdinal(id1, id2);
          }
        case IdType.Guid_2:
          {
            System.Guid id1 = (System.Guid)IdentifierPart;
            return id1.CompareTo(id);
          }
        case IdType.Opaque_3:
          {
            byte[] id1 = (byte[])IdentifierPart;
            byte[] id2 = (byte[])id;
            if (id1.Length == id2.Length)
            {
              for (int ii = 0; ii < id1.Length; ii++)
                if (id1[ii] != id2[ii])
                  return (id1[ii] < id2[ii]) ? -1 : +1;
              return 0;
            }
            return (id1.Length < id2.Length) ? -1 : +1;
          }
      }
      // invalid id type - should never get here.
      return +1;
    }

    #endregion IComparable

    #region IFormattable

    /// <summary>
    /// Returns the string representation of a NodeId.
    /// </summary>
    /// <remarks>
    /// Returns the string representation of a NodeId. This is the same as calling
    /// <see cref="Format()"/>.
    /// </remarks>
    /// <exception cref="FormatException">Thrown when the format is not null</exception>
    public string ToString(string format, IFormatProvider formatProvider)
    {
      if (format == null)
        return String.Format(formatProvider, "{0}", Format());
      throw new FormatException(String.Format("Invalid format string: '{0}'.", format));
    }

    #endregion IFormattable

    #region object

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public override int GetHashCode()
    {
      return m_HashCode;
    }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return Format();
    }

    #endregion object

    #region private

    /// <summary>
    /// Formats a node id as a string.
    /// </summary>
    private static void FormatIdentifier(StringBuilder buffer, object identifier, IdType identifierType)
    {
      switch (identifierType)
      {
        case IdType.Numeric_0:
          if (identifier == null)
          {
            buffer.Append('0');
            break;
          }
          buffer.AppendFormat(CultureInfo.InvariantCulture, "{0}", identifier);
          break;

        case IdType.String_1:
          buffer.AppendFormat(CultureInfo.InvariantCulture, "{0}", identifier);
          break;

        case IdType.Guid_2:
          if (identifier == null)
          {
            buffer.Append(System.Guid.Empty);
            break;
          }
          buffer.AppendFormat(CultureInfo.InvariantCulture, "{0}", identifier);
          break;

        case IdType.Opaque_3:
          if (identifier != null)
            buffer.AppendFormat(CultureInfo.InvariantCulture, "{0}", Convert.ToBase64String((byte[])identifier));
          break;
      }
    }

    private ushort m_namespaceIndex;
    private IdType m_identifierType;
    private object m_identifierPart;
    private static NodeId s_Null = new NodeId();
    private static int m_GlobalHashCode = 0;
    private int m_HashCode = m_GlobalHashCode;

    #endregion private

    #region IEquatable<NodeId>

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
    public bool Equals(NodeId other)
    {
      return this.CompareTo(other) == 0;
    }

    #endregion IEquatable<NodeId>
  }
}