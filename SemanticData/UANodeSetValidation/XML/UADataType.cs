//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UADataType
  {
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
  }
}
