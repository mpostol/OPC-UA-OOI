//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________


namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  public partial class UAObjectType
  {
    /// <summary>
    /// Get the clone from the types derived from this one.
    /// </summary>
    /// <returns>An instance of <see cref="T:UAOOI.SemanticData.UANodeSetValidation.XML.UANode" />.</returns>
    protected override UANode ParentClone()
    {
      UAObjectType _ret = new UAObjectType();
      base.CloneUAType(_ret);
      return _ret;
    }
  }
}
