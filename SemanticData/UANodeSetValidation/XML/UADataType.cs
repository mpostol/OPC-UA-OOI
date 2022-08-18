//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UADataType : IUADataType
  {
    #region IUADataType

    public override NodeClassEnum NodeClass => NodeClassEnum.UADataType;

    UAOOI.SemanticData.InformationModelFactory.DataTypePurpose IUADataType.Purpose
    {
      get
      {
        UAOOI.SemanticData.InformationModelFactory.DataTypePurpose _status = default(UAOOI.SemanticData.InformationModelFactory.DataTypePurpose);
        switch (this.Purpose)
        {
          case DataTypePurpose.Normal:
            _status = UAOOI.SemanticData.InformationModelFactory.DataTypePurpose.Normal;
            break;

          case DataTypePurpose.CodeGenerator:
            _status = UAOOI.SemanticData.InformationModelFactory.DataTypePurpose.CodeGenerator;
            break;

          case DataTypePurpose.ServicesOnly:
            _status = UAOOI.SemanticData.InformationModelFactory.DataTypePurpose.ServicesOnly;
            break;
        }
        return _status;
      }
    }

    IDataTypeDefinition IUADataTypeNodeClass.Definition
    {
      get { return this.Definition; }
      //TODO Define independent Address Space API #645 - must be implemented
      //set { this.Definition = new DataTypeDefinition(value); }
    }

    #endregion IUADataType

    /// <summary>
    /// Get the clone from the types derived from this one.
    /// </summary>
    /// <returns>An instance of <see cref="UANode" />.</returns>
    protected override UANode ParentClone()
    {
      UADataType _ret = new UADataType()
      {
        Definition = this.Definition,
        Purpose = this.Purpose
      };
      base.CloneUAType(_ret);
      return _ret;
    }

    internal override void RecalculateNodeIds(IUAModelContext modelContext, Action<TraceMessage> trace)
    {
      base.RecalculateNodeIds(modelContext, trace);
      if (this.Definition is null)
        return;
      this.Definition.RecalculateNodeIds(x => modelContext.ImportNodeId(x, trace));
    }
  }
}