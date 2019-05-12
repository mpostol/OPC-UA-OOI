//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{

  /// <summary>
  /// Interface IVariableTypeFactory
  /// </summary>
  public interface IVariableTypeFactory : ITypeFactory, IDataDescriptor
  {

    /// <summary>
    /// Sets the default value. The value of the Variable node that the server assigns while instantiating the node. Its data type is defined by the <see cref="IDataDescriptor.DataType"/>.
    /// </summary>
    /// <value>The default value.</value>
    XmlElement DefaultValue
    {
      set;
    }

  }
}
