//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UAOOI.Common.Infrastructure.Serializers;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Delegate LocalizedTextFactory - encapsulates a method that must be used to create localized text using <paramref name="localeField"/> and <paramref name="valueField"/>
  /// </summary>
  /// <param name="localeField">The locale field. This argument is specified as a <see cref="string"/>  that is composed of a language component and a country/region
  /// component as specified by RFC 3066. The country/region component is always preceded by a hyphen. The format of the LocaleId string is shown below:
  /// <c>
  /// &lt;language&gt;[-&lt;country/region&gt;],
  /// where:
  ///   &lt;language&gt; is the two letter ISO 639 code for a language,
  ///   &lt;country/region&gt; is the two letter ISO 3166 code for the country/region.
  /// </c>
  /// </param>
  /// <param name="valueField">The value field.</param>
  internal delegate void LocalizedTextFactory(string localeField, string valueField);

  /// <summary>
  /// Class Extensions - provides helper functions for this namespace
  /// </summary>
  internal static class Extensions
  {
    //string
    internal static string SymbolicName(this List<string> path)
    {
      return string.Join("_", path.ToArray());
    }

    /// <summary>
    /// Exports the string and filter out the default value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>Returns <paramref name="value"/> if not equal to <paramref name="defaultValue"/>, otherwise it returns <see cref="string.Empty"/>.</returns>
    internal static string ExportString(this string value, string defaultValue)
    {
      if (string.IsNullOrEmpty(value))
        return null;
      return string.Compare(value, defaultValue) == 0 ? null : value;
    }

    internal static bool? Export(this bool value, bool defaultValue)
    {
      return !value.Equals(defaultValue) ? value : new Nullable<bool>();
    }

    internal static int? Export(this double value, double defaultValue)
    {
      return !value.Equals(defaultValue) ? Convert.ToInt32(value) : new Nullable<int>();
    }

    internal static AttributeWriteMask Validate(this AttributeWriteMask value, AttributeWriteMask maxValue, Action<AttributeWriteMask> reportError)
    {
      if (value.CompareTo(maxValue) >= 0)
        reportError(value);
      return value & maxValue - 1;
    }

    //TODO IsValidLanguageIndependentIdentifier is not supported by the .NET standard #340
    /// <summary>
    /// Gets a value indicating whether the specified value is a valid language independent identifier.
    /// </summary>
    /// <remarks>
    /// it is implemented using
    /// https://raw.githubusercontent.com/Microsoft/referencesource/3b1eaf5203992df69de44c783a3eda37d3d4cd10/System/compmod/system/codedom/compiler/CodeGenerator.cs as the starting point.
    /// </remarks>
    private static bool IsValidLanguageIndependentIdentifier(this string value)
    {
      if (value.Length == 0)
        return false;
      // each char must be Lu, Ll, Lt, Lm, Lo, Nd, Mn, Mc, Pc
      for (int i = 0; i < value.Length; i++)
      {
        char ch = value[i];
        UnicodeCategory uc = char.GetUnicodeCategory(ch);
        switch (uc)
        {
          case UnicodeCategory.UppercaseLetter:        // Lu
          case UnicodeCategory.LowercaseLetter:        // Ll
          case UnicodeCategory.TitlecaseLetter:        // Lt
          case UnicodeCategory.ModifierLetter:         // Lm
          case UnicodeCategory.LetterNumber:           // Lm
          case UnicodeCategory.OtherLetter:            // Lo
            break;

          case UnicodeCategory.NonSpacingMark:         // Mn
          case UnicodeCategory.SpacingCombiningMark:   // Mc
          case UnicodeCategory.ConnectorPunctuation:   // Pc
          case UnicodeCategory.DecimalDigitNumber:     // Nd
                                                       // Underscore is a valid starting character, even though it is a ConnectorPunctuation.
                                                       //if (nextMustBeStartChar && ch != '_')
                                                       //  return false;
            break;

          default:
            return false;
        }
      }
      return true;
    }

    internal static string ValidateIdentifier(this string name, Action<TraceMessage> reportError)
    {
      if (!name.IsValidLanguageIndependentIdentifier())
        reportError(TraceMessage.BuildErrorTraceMessage(BuildError.WrongSymbolicName, string.Format("SymbolicName: '{0}'.", name)));
      return name;
    }

    internal static string NodeIdentifier(this IUANode node)
    {
      if (node.BrowseName == null)
        return node.SymbolicName;
      return node.BrowseName.ToString();
    }

    internal static string ConvertToString(this LocalizedText localizedText)
    {
      if (localizedText == null)
        return "Empty LocalizedText";
      return string.Format("{0}:{1}", localizedText.Locale, localizedText.Text);
    }

    /// <summary>
    /// Converts the ArrayDimensions represented as the array of <seealso cref="uint"/> to string.
    /// </summary>
    /// <remarks>
    /// The maximum length of an array.
    /// This value is a comma separated list of unsigned integer values.The list has a number of elements equal to the ValueRank.
    /// The value is 0 if the maximum is not known for a dimension.
    /// This field is not specified if the ValueRank less or equal 0.
    /// This field is not specified for subtypes of Enumeration or for DataTypes with the OptionSetValues Property.
    /// </remarks>
    /// <param name="arrayDimensions">The array dimensions represented as the string.</param>
    /// <returns>System.String.</returns>
    internal static string ArrayDimensionsToString(this uint[] arrayDimensions)
    {
      return string.Join(", ", arrayDimensions);
    }

    internal static void GetParameters(this IDataTypeDefinition dataTypeDefinition,
                                            IDataTypeDefinitionFactory dataTypeDefinitionFactory,
                                            IAddressSpaceBuildContext nodeContext,
                                            Action<TraceMessage> traceEvent)
    {
      if (dataTypeDefinition is null)
        return;
      //xsd comment  < !--BaseType is obsolete and no longer used.Left in for backwards compatibility. -->
      //definition.BaseType = modelContext.ExportBrowseName(dataTypeDefinition.BaseType, DataTypes.BaseDataType);
      dataTypeDefinitionFactory.IsOptionSet = dataTypeDefinition.IsOptionSet;
      dataTypeDefinitionFactory.IsUnion = dataTypeDefinition.IsUnion;
      dataTypeDefinitionFactory.Name = null; //TODO UADataType.Definition.Name wrong value #341 modelContext.ExportBrowseName( dataTypeDefinition.Name, DataTypes.BaseDataType);
      dataTypeDefinitionFactory.SymbolicName = dataTypeDefinition.SymbolicName;
      if (dataTypeDefinition.Field == null || dataTypeDefinition.Field.Length == 0)
        return;
      foreach (IDataTypeField _item in dataTypeDefinition.Field)
      {
        IDataTypeFieldFactory _nP = dataTypeDefinitionFactory.NewField();
        _nP.Name = _item.Name;
        _nP.SymbolicName = _item.SymbolicName;
        _item.DisplayName.ExportLocalizedTextArray(_nP.AddDisplayName);
        _nP.DataType = nodeContext.ExportBrowseName(_item.DataTypeNodeId, DataTypes.BaseDataType);
        _nP.ValueRank = _item.ValueRank.ParseValueRank(traceEvent);
        _nP.ArrayDimensions = _item.ArrayDimensions;
        _nP.MaxStringLength = _item.MaxStringLength;
        _item.Description.ExportLocalizedTextArray(_nP.AddDescription);
        _nP.Value = _item.Value;
        _nP.IsOptional = _item.IsOptional;
      }
    }

    //TODO Define independent Address Space API #645 - move it to the InformationModelFactory
    internal static void ExportLocalizedTextArray(this LocalizedText[] text, LocalizedTextFactory createLocalizedText)
    {
      if (text == null || text.Length == 0)
        return;
      foreach (LocalizedText item in text)
        createLocalizedText(item.Locale, item.Text);
    }

    internal static LocalizedText[] Truncate(this LocalizedText[] localizedText, int maxLength, Action<TraceMessage> reportError)
    {
      if (localizedText == null || localizedText.Length == 0)
        return null;
      List<LocalizedText> _ret = new List<LocalizedText>();
      foreach (LocalizedText _item in localizedText)
      {
        if (_item.Text.Length > maxLength)
        {
          reportError(TraceMessage.BuildErrorTraceMessage(BuildError.WrongDisplayNameLength, string.Format
            ("The localized text starting with '{0}:{1}' of length {2} is too long.", _item.Locale, _item.Text.Substring(0, 20), _item.Text.Length)));
          LocalizedText _localizedText = new LocalizedText()
          {
            Locale = _item.Locale,
            Text = _item.Text.Substring(0, maxLength)
          };
          _ret.Add(_localizedText);
        }
      }
      return _ret.ToArray();
    }

    internal static List<DataSerialization.Argument> GetParameters(this XmlElement xmlElement)
    {
      //TODO UANodeSetValidation.Extensions.GetObject - object reference not set #624
      ListOfExtensionObject _wrapper = xmlElement.GetObject<ListOfExtensionObject>();
      Debug.Assert(_wrapper != null);
      if (_wrapper.ExtensionObject.AsEnumerable<ExtensionObject>().Where<ExtensionObject>(x => !((string)x.TypeId.Identifier).Equals("i=297")).Any())
        throw new ArgumentOutOfRangeException("ExtensionObject.TypeId.Identifier");
      List<DataSerialization.Argument> _ret = new List<DataSerialization.Argument>();
      foreach (ExtensionObject item in _wrapper.ExtensionObject)
        _ret.Add(item.Body.GetObject<DataSerialization.Argument>());
      return _ret;
    }

    internal static bool LocalizedTextArraysEqual(this LocalizedText[] first, LocalizedText[] second)
    {
      if (Object.ReferenceEquals(first, null))
        return Object.ReferenceEquals(second, null);
      if (Object.ReferenceEquals(second, null))
        return false;
      if (first.Length != second.Length)
        return false;
      try
      {
        Dictionary<string, LocalizedText> _dictionaryForFirst = first.ToDictionary(x => ConvertToString(x));
        foreach (LocalizedText _text in second)
          if (!_dictionaryForFirst.ContainsKey(ConvertToString(_text)))
            return false;
      }
      catch (Exception)
      {
        return false;
      }
      return true;
    }

    internal static bool RolePermissionsEquals(this IRolePermission[] first, IRolePermission[] second)
    {
      if (Object.ReferenceEquals(first, null))
        return Object.ReferenceEquals(second, null);
      if (first.Length != second.Length)
        return false;
      Dictionary<uint, IRolePermission> _dictionaryForFirst = first.ToDictionary<IRolePermission, uint>(x => x.Permissions);
      foreach (IRolePermission _permission in second)
      {
        if (!_dictionaryForFirst.ContainsKey(_permission.Permissions))
          return false;
        if (_dictionaryForFirst[_permission.Permissions].Value != _permission.Value)
          return false;
      }
      return true;
    }

    internal static bool ReferencesEquals(this IReference[] first, IReference[] second)
    {
      return true;
    }

    internal static bool AreEqual(this string first, string second)
    {
      if (String.IsNullOrEmpty(first))
        return String.IsNullOrEmpty(second);
      return String.Compare(first, second) == 0;
    }

    #region private

    //private static string ConvertToString(LocalizedText x)
    //{
    //  return $"{x.Locale}:{x.Text}";
    //}

    /// <summary>
    /// Deserialize <see cref="XmlElement" /> object using <see cref="XmlSerializer" />
    /// </summary>
    /// <typeparam name="type">The type of the type.</typeparam>
    /// <param name="xmlElement">The object to be deserialized.</param>
    /// <returns>Deserialized object</returns>
    private static type GetObject<type>(this XmlElement xmlElement)
    {
      using (MemoryStream _memoryBuffer = new MemoryStream(1000))
      {
        XmlWriterSettings _settings = new XmlWriterSettings() { ConformanceLevel = ConformanceLevel.Fragment };
        using (XmlWriter wrt = XmlWriter.Create(_memoryBuffer, _settings))
          //TODO UANodeSetValidation.Extensions.GetObject - object reference not set #624
          xmlElement.WriteTo(wrt);
        _memoryBuffer.Flush();
        _memoryBuffer.Position = 0;
        return XmlFile.ReadXmlFile<type>(_memoryBuffer);
      }
    }

    #endregion private
  }
}