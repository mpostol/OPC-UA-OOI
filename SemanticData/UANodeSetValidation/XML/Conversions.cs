//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using OOIReleaseStatus = UAOOI.SemanticData.InformationModelFactory.ReleaseStatus;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  internal static class Conversions
  {
    internal static DataSerialization.LocalizedText[] GetLocalizedTextArray(this LocalizedText[] value)
    {
      if (value == null || value.Length == 0)
        return null;
      List<DataSerialization.LocalizedText> ret = new List<DataSerialization.LocalizedText>();
      foreach (LocalizedText item in value)
        ret.Add(new DataSerialization.LocalizedText() { Locale = item.Locale, Text = item.Value });
      return ret.ToArray();
    }

    internal static AccessRestrictions GetAccessRestrictions(this byte accessRestrictions, NodeClassEnum typeName, Action<TraceMessage> buildErrorsHandling)
    {
      if (accessRestrictions > 7)
      {
        buildErrorsHandling(TraceMessage.BuildErrorTraceMessage(BuildError.WrongAccessLevel, $"The current value is {accessRestrictions} of the node type {typeName}. Assigned max value"));
        return AccessRestrictions.EncryptionRequired & AccessRestrictions.SessionRequired & AccessRestrictions.SigningRequired;
      }
      return (AccessRestrictions)accessRestrictions;
    }

    internal static AttributeWriteMask GetAttributeWriteMask(this UInt32 value)
    {
      return (AttributeWriteMask)value;
    }

    internal static OOIReleaseStatus GetReleaseStatus(this ReleaseStatus value)
    {
      OOIReleaseStatus retValue = default(OOIReleaseStatus);
      switch (value)
      {
        case ReleaseStatus.Released:
          retValue = OOIReleaseStatus.Released;
          break;

        case ReleaseStatus.Draft:
          retValue = OOIReleaseStatus.Draft;
          break;

        case ReleaseStatus.Deprecated:
          retValue = OOIReleaseStatus.Deprecated;
          break;

        default:
          break;
      }
      return retValue;
    }
  }
}