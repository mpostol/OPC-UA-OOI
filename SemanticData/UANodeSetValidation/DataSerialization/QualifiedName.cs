//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization
{
  /// <summary>
  /// A name qualified with a namespace.
  /// </summary>
  /// <remarks>
  /// <para>
  /// The QualifiedName is defined in <b>Part 3 - Address Space Model, Section 7.3</b>, titled
  /// <b>Qualified Name</b>.
  /// <br/></para>
  /// <para>
  /// The QualifiedName is a simple wrapper class that is used to generate a fully-qualified name
  /// for any type that has a name.
  /// <br/></para>
  /// <para>
  /// A <i>Fully Qualified</i> name is one that consists of a name, and an index of which namespace
  /// (within a namespace table) this name belongs to.
  /// For example<br/>
  /// <b>Namespace Index</b> = 1<br/>
  /// <b>Name</b> = MyName<br/>
  /// becomes:<br/>
  /// <i>1:MyName</i>
  /// <br/></para>
  /// </remarks>

  public partial class QualifiedName : IFormattable, ICloneable, IComparable
  {
    #region Constructors

    /// <summary>
    /// Initializes the object with default values.
    /// </summary>
    /// <remarks>
    /// Initializes the object with default values.
    /// </remarks>
    internal QualifiedName()
    {
      NamespaceIndex = 0;
      Name = null;
    }

    /// <summary>
    /// Creates a deep copy of the value.
    /// </summary>
    /// <remarks>
    /// Creates a deep copy of the value.
    /// </remarks>
    /// <param name="value">The qualified name to copy</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided value is null</exception>
    public QualifiedName(QualifiedName value)
    {
      if (value == null) throw new ArgumentNullException("value");
      Name = value.Name;
      NamespaceIndex = value.NamespaceIndex;
      NamespaceIndexSpecified = value.NamespaceIndexSpecified;
    }

    /// <summary>
    /// Initializes the object with a name.
    /// </summary>
    /// <remarks>
    /// Initializes the object with a name.
    /// </remarks>
    /// <param name="name">The name-portion to store as part of the fully qualified name</param>
    public QualifiedName(string name)
    {
      NamespaceIndex = 0;
      Name = name;
      NamespaceIndexSpecified = false;
    }

    /// <summary>
    /// Initializes the object with a name and a namespace index.
    /// </summary>
    /// <remarks>
    /// Initializes the object with a name and a namespace index.
    /// </remarks>
    /// <param name="name">The name-portion of the fully qualified name</param>
    /// <param name="namespaceIndex">The index of the namespace within the namespace-table</param>
    public QualifiedName(string name, ushort namespaceIndex)
    {
      NamespaceIndex = namespaceIndex;
      Name = name;
      NamespaceIndexSpecified = namespaceIndex != 0;
    }

    #endregion Constructors

    #region Public Properties

    /// <summary cref="QualifiedName.NamespaceIndex" />
    internal ushort XmlEncodedNamespaceIndex
    {
      get => NamespaceIndex;
      set => NamespaceIndex = value;
    }

    #endregion Public Properties

    #region IComparable Members

    /// <summary>
    /// Compares two QualifiedNames.
    /// </summary>
    /// <param name="obj">The object to compare to.</param>
    /// <returns>
    /// Less than zero if the instance is less than the object.
    /// Zero if the instance is equal to the object.
    /// Greater than zero if the instance is greater than the object.
    /// </returns>
    public int CompareTo(object obj)
    {
      if (Object.ReferenceEquals(obj, null))
        return -1;
      if (Object.ReferenceEquals(this, obj))
        return 0;
      QualifiedName _qualifiedName = obj as QualifiedName;
      if (_qualifiedName == null)
        return typeof(QualifiedName).GUID.CompareTo(obj.GetType().GUID);
      if (_qualifiedName.NamespaceIndex != NamespaceIndex)
        return NamespaceIndex.CompareTo(_qualifiedName.NamespaceIndex);
      if (Name != null)
        return String.CompareOrdinal(Name, _qualifiedName.Name);
      return -1;
    }

    /// <summary>
    /// Implements the operator &gt;.
    /// </summary>
    /// <param name="value1">The value1.</param>
    /// <param name="value2">The value2.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator >(QualifiedName value1, QualifiedName value2)
    {
      if (!Object.ReferenceEquals(value1, null))
        return value1.CompareTo(value2) > 0;
      return false;
    }

    /// <summary>
    /// Implements the operator &lt;.
    /// </summary>
    /// <param name="value1">The value1.</param>
    /// <param name="value2">The value2.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator <(QualifiedName value1, QualifiedName value2)
    {
      if (!Object.ReferenceEquals(value1, null))
        return value1.CompareTo(value2) < 0;
      return true;
    }

    #endregion IComparable Members

    #region Overridden Methods

    /// <summary>
    /// Returns a suitable hash value for the instance.
    /// </summary>
    public override int GetHashCode()
    {
      if (Name != null)
        return Name.GetHashCode();
      return 0;
    }

    /// <summary>
    /// Returns true if the objects are equal.
    /// </summary>
    /// <remarks>
    /// Returns true if the objects are equal.
    /// </remarks>
    /// <param name="obj">The object to compare to this/me</param>
    public override bool Equals(object obj)
    {
      if (Object.ReferenceEquals(obj, null))
        return false;
      if (Object.ReferenceEquals(this, obj))
        return true;
      QualifiedName _qualifiedName = obj as QualifiedName;
      if (_qualifiedName == null)
        return false;
      if (_qualifiedName.NamespaceIndex != NamespaceIndex)
        return false;
      return _qualifiedName.Name == Name;
    }

    /// <summary>
    /// Returns true if the objects are equal.
    /// </summary>
    /// <remarks>
    /// Returns true if the objects are equal.
    /// </remarks>
    /// <param name="value1">The first value to compare</param>
    /// <param name="value2">The second value to compare</param>
    public static bool operator ==(QualifiedName value1, QualifiedName value2)
    {
      if (Object.ReferenceEquals(value1, null))
        return Object.ReferenceEquals(value2, null);
      return value1.Equals(value2);
    }

    /// <summary>
    /// Returns true if the objects are not equal.
    /// </summary>
    /// <remarks>
    /// Returns true if the objects are equal.
    /// </remarks>
    /// <param name="value1">The first value to compare</param>
    /// <param name="value2">The second value to compare</param>
    public static bool operator !=(QualifiedName value1, QualifiedName value2)
    {
      if (Object.ReferenceEquals(value1, null))
        return !Object.ReferenceEquals(value2, null);
      return !value1.Equals(value2);
    }

    /// <summary>
    /// Returns the string representation of the object.
    /// </summary>
    /// <remarks>
    /// Returns the string representation of the object.
    /// </remarks>
    public override string ToString()
    {
      return ToString(null, null);
    }

    #endregion Overridden Methods

    #region IFormattable Members

    /// <summary>
    /// Returns the string representation of the object.
    /// </summary>
    /// <remarks>
    /// Returns the string representation of the object.
    /// </remarks>
    /// <param name="format">(Unused) Always pass null</param>
    /// <param name="formatProvider">(Unused) Always pass null</param>
    /// <exception cref="FormatException">Thrown if non-null parameters are passed</exception>
    public string ToString(string format, IFormatProvider formatProvider)
    {
      if (format == null)
      {
        int capacity = (Name != null) ? Name.Length : 0;
        StringBuilder builder = new StringBuilder(capacity + 10);
        if (this.NamespaceIndex == 0)
        {
          // prepend the namespace index if the name contains a colon.
          if (Name != null)
          {
            int index = Name.IndexOf(':');
            if (index != -1)
              builder.Append("0:");
          }
        }
        else
        {
          builder.Append(NamespaceIndex);
          builder.Append(':');
        }
        if (Name != null)
          builder.Append(Name);
        return builder.ToString();
      }
      throw new FormatException(String.Format("Invalid format string: '{0}'.", format));
    }

    #endregion IFormattable Members

    #region ICloneable Members

    /// <summary>
    /// Makes a deep copy of the object.
    /// </summary>
    /// <remarks>
    /// Makes a deep copy of the object.
    /// </remarks>
    public object Clone()
    {
      // this object cannot be altered after it is created so no new allocation is necessary.
      return this;
    }

    #endregion ICloneable Members

    #region Static Methods

    /// <summary>
    /// Converts an expanded node id to a node id using a namespace table.
    /// </summary>
    internal static QualifiedName Create(string name, string namespaceUri, INamespaceTable namespaceTable)
    {
      // check for null.
      if (String.IsNullOrEmpty(name))
        return QualifiedName.Null;
      // return a name using the default namespace.
      if (String.IsNullOrEmpty(namespaceUri))
        return new QualifiedName(name);
      // find the namespace index.
      int namespaceIndex = -1;
      if (namespaceTable != null)
        namespaceIndex = namespaceTable.GetURIIndex(new Uri(namespaceUri));
      // oops - not found.
      if (namespaceIndex < 0)
        throw new ServiceResultException
          (TraceMessage.BuildErrorTraceMessage(BuildError.QualifiedNameInvalidSyntax, String.Format("NamespaceUri ({0}) is not in the NamespaceTable.", namespaceUri)), "Cannot create the QualifiedName because NamespaceUri is not in the NamespaceTable.");
      // return the name.
      return new QualifiedName(name, (ushort)namespaceIndex);
    }

    /// <summary>
    /// Returns true if the QualifiedName is valid.
    /// </summary>
    /// <param name="value">The name to be validated.</param>
    /// <param name="namespaceUris">The table namespaces known to the server.</param>
    /// <returns>True if the name is value.</returns>
    internal static bool IsValid(QualifiedName value, INamespaceTable namespaceUris)
    {
      if (value == null || String.IsNullOrEmpty(value.Name))
        return false;
      if (namespaceUris != null && namespaceUris.GetModelTableEntry(value.NamespaceIndex) == null)
        return false;
      return true;
    }

    /// <summary>
    /// Parses a string containing a <see cref="QualifiedName"/> with the syntax <code>n:qname</code>
    /// </summary>
    /// <param name="text">The QualifiedName value as a string.</param>
    internal static QualifiedName Parse(string text)
    {
      ushort defaultNamespaceIndex = 0;
      string pattern = @"\b((\d{1,}):)?(.+)";
      RegexOptions options = RegexOptions.Singleline;
      MatchCollection parseResult = Regex.Matches(text, pattern, options);
      if (parseResult.Count == 0)
        throw new ArgumentOutOfRangeException(nameof(text), $"The entry text {text} cannot be resolved to create a valid instance of the {nameof(QualifiedName)}.");
      else if (parseResult.Count > 1)
        throw new ArgumentOutOfRangeException(nameof(text), $"Ambiguous entry - {text} contains {parseResult.Count} parts that match the {nameof(QualifiedName)} syntax.");
      if (!parseResult[0].Groups[3].Success)
        throw new ArgumentOutOfRangeException(nameof(text), $"The entry text {text} doesn't contain a required {nameof(QualifiedName.Name)} field.");
      if (ushort.TryParse(parseResult[0].Groups[2].Value, out ushort index))
        defaultNamespaceIndex = index;
      return new QualifiedName() { Name = parseResult[0].Groups[3].Value, namespaceIndexFieldSpecified = true, NamespaceIndex = defaultNamespaceIndex };
    }

    /// <summary>
    /// Returns true if the value is null.
    /// </summary>
    /// <param name="value">The qualified name to check</param>
    public static bool IsNull(QualifiedName value)
    {
      if (value != null)
      {
        if (value.NamespaceIndex != 0 || !String.IsNullOrEmpty(value.Name))
        {
          return false;
        }
      }
      return true;
    }

    /// <summary>
    /// Converts a string to a qualified name.
    /// </summary>
    /// <remarks>
    /// Converts a string to a qualified name.
    /// </remarks>
    /// <param name="value">The string to turn into a fully qualified name</param>
    public static QualifiedName ToQualifiedName(string value)
    {
      return new QualifiedName(value);
    }

    /// <summary>
    /// Converts a string to a qualified name.
    /// </summary>
    /// <remarks>
    /// Converts a string to a qualified name.
    /// </remarks>
    /// <param name="value">The string to turn into a fully qualified name</param>
    public static implicit operator QualifiedName(string value)
    {
      return new QualifiedName(value);
    }

    /// <summary>
    /// Returns an instance of a null QualifiedName.
    /// </summary>
    public static QualifiedName Null => s_Null;

    private static readonly QualifiedName s_Null = new QualifiedName();

    #endregion Static Methods
  }

  /// <summary>
  /// A collection of QualifiedName objects.
  /// </summary>
  /// <remarks>
  /// A strongly-typed collection of QualifiedName objects.
  /// </remarks>
  public partial class QualifiedNameCollection : List<QualifiedName>, ICloneable
  {
    /// <summary>
    /// Initializes an empty collection.
    /// </summary>
    /// <remarks>
    /// Initializes an empty collection.
    /// </remarks>
    public QualifiedNameCollection()
    { }

    /// <summary>
    /// Initializes the collection from another collection.
    /// </summary>
    /// <remarks>
    /// Initializes the collection from another collection.
    /// </remarks>
    /// <param name="collection">The enumerated collection of qualified names to add to this new collection</param>
    public QualifiedNameCollection(IEnumerable<QualifiedName> collection) : base(collection) { }

    /// <summary>
    /// Initializes the collection with the specified capacity.
    /// </summary>
    /// <remarks>
    /// Initializes the collection with the specified capacity.
    /// </remarks>
    /// <param name="capacity">Max capacity of this collection</param>
    public QualifiedNameCollection(int capacity) : base(capacity) { }

    /// <summary>
    /// Converts an array to a collection.
    /// </summary>
    /// <remarks>
    /// Converts an array to a collection.
    /// </remarks>
    /// <param name="values">The array to turn into a collection</param>
    public static QualifiedNameCollection ToQualifiedNameCollection(QualifiedName[] values)
    {
      if (values != null)
        return new QualifiedNameCollection(values);
      return new QualifiedNameCollection();
    }

    /// <summary>
    /// Converts an array to a collection.
    /// </summary>
    /// <remarks>
    /// Converts an array to a collection.
    /// </remarks>
    /// <param name="values">The array to turn into a collection</param>
    public static implicit operator QualifiedNameCollection(QualifiedName[] values)
    {
      return ToQualifiedNameCollection(values);
    }

    #region ICloneable Methods

    /// <summary>
    /// Creates a deep copy of the collection.
    /// </summary>
    /// <remarks>
    /// Creates a deep copy of the collection.
    /// </remarks>
    public object Clone()
    {
      QualifiedNameCollection _clonedCollection = new QualifiedNameCollection(this.Count);
      foreach (QualifiedName _item in this)
        _clonedCollection.Add((QualifiedName)_item.Clone());
      return _clonedCollection;
    }

    #endregion ICloneable Methods
  }//QualifiedNameCollection
}//namespace