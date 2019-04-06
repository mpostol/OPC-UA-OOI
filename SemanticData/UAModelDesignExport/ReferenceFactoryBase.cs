//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  internal class ReferenceFactoryBase : IReferenceFactory
  {

    #region IReferenceFactory
    /// <summary>
    /// Sets the type of the reference.
    /// </summary>
    /// <value>The type of the reference.</value>
    public XmlQualifiedName ReferenceType
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the target identifier.
    /// </summary>
    /// <value>The target identifier.</value>
    public XmlQualifiedName TargetId
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets a value indicating whether this instance is inverse.
    /// </summary>
    /// <value><c>true</c> if this instance is inverse; otherwise, <c>false</c>.</value>
    public bool IsInverse
    {
      set;
      private get;
    }
    #endregion

    #region internal API
    internal Reference Export()
    {
      return new Reference()
      {
        IsInverse = this.IsInverse,
        IsOneWay = false, //Not supported
        ReferenceType = this.ReferenceType,
        TargetId = TargetId
      };
    }
    #endregion

  }
}
