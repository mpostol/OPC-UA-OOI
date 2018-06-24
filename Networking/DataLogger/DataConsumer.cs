
using System;
using System.ComponentModel.Composition;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;

namespace UAOOI.Networking.DataLogger
{

  /// <summary>
  /// Class ConsumerViewModel - it is a consumer of the data send over the wire using the UAOOI.Networking.SemanticData framework.
  /// It is expected that the data is generated according to the requirements defined by OPCF to proceed interoperability testing.
  /// </summary>
  [Export(ConsumerCompositionSettings.BindingFactoryContract, typeof(IBindingFactory))]
  [PartCreationPolicy(CreationPolicy.NonShared)]
  internal class DataConsumer : IBindingFactory
  {

    #region composition
    [Import(ConsumerCompositionSettings.ViewModelContract, typeof(ConsumerViewModel))]
    internal ConsumerViewModel ViewModel
    {
      get; set;
    }
    #endregion

    #region API
    internal ConsumerViewModel ViewModelBindingFactory { get; set; }
    #endregion

    #region IBindingFactory
    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="UAOOI.Networking.SemanticData.DataRepository.IConsumerBinding" /> type used by the consumer to save the data in the data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belonging to the same group are handled according to the same profile.</param>
    /// <param name="processValueName">The name of a variable that is the ultimate destination of the values recovered from messages.
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup" />.</param>
    /// <param name="fieldTypeInfo">The field metadata definition represented as an object of <see cref="UAOOI.Configuration.Networking.Serialization.UATypeInfo" />.</param>
    /// <returns>Returns an object implementing the <see cref="UAOOI.Networking.SemanticData.DataRepository.IConsumerBinding" /> interface that can be used to update selected variable on the factory side.</returns>
    /// <exception cref="System.ArgumentNullException">repositoryGroup</exception>
    IConsumerBinding IBindingFactory.GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      if (repositoryGroup != Encoding.EncodingCompositionSettings.ConfigurationRepositoryGroup)
        throw new ArgumentNullException("repositoryGroup");
      return GetConsumerBinding(processValueName, fieldTypeInfo);
    }
    /// <summary>
    /// Gets the producer binding.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="processValueName">The name of a variable that is the source of the values forwarded by a message over the network.
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup" /></param>
    /// <param name="fieldTypeInfo">The <see cref="T:UAOOI.Configuration.Networking.Serialization.BuiltInType" />of the message field encoding.</param>
    /// <returns>An instance implementing the <see cref="IProducerBinding" /> interface.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    /// <remarks>It is intentionally not implemented.</remarks>
    IProducerBinding IBindingFactory.GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region private
    /// <summary>
    /// Helper method that creates the consumer binding.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="typeInfo">The encoding.</param>
    /// <returns>IConsumerBinding.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">variableName</exception>
    private IConsumerBinding GetConsumerBinding(string variableName, UATypeInfo typeInfo)
    {
      IConsumerBinding _return = null;
      if (typeInfo.ValueRank == 0 || typeInfo.ValueRank > 1)
        throw new ArgumentOutOfRangeException(nameof(typeInfo.ValueRank));
      switch (typeInfo.BuiltInType)
      {
        case BuiltInType.Boolean:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<Boolean>(variableName, typeInfo);
          else
            _return = AddBinding<Boolean[]>(variableName, typeInfo);
          break;
        case BuiltInType.SByte:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<SByte>(variableName, typeInfo);
          else
            _return = AddBinding<SByte[]>(variableName, typeInfo);
          break;
        case BuiltInType.Byte:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<Byte>(variableName, typeInfo);
          else
            _return = AddBinding<Byte[]>(variableName, typeInfo);
          break;
        case BuiltInType.Int16:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<Int16>(variableName, typeInfo);
          else
            _return = AddBinding<Int16[]>(variableName, typeInfo);
          break;
        case BuiltInType.UInt16:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<UInt16>(variableName, typeInfo);
          else
            _return = AddBinding<UInt16[]>(variableName, typeInfo);
          break;
        case BuiltInType.Int32:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<Int32>(variableName, typeInfo);
          else
            _return = AddBinding<Int32[]>(variableName, typeInfo);
          break;
        case BuiltInType.UInt32:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<UInt32>(variableName, typeInfo);
          else
            _return = AddBinding<UInt32[]>(variableName, typeInfo);
          break;
        case BuiltInType.Int64:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<Int64>(variableName, typeInfo);
          else
            _return = AddBinding<Int64[]>(variableName, typeInfo);
          break;
        case BuiltInType.UInt64:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<UInt64>(variableName, typeInfo);
          else
            _return = AddBinding<UInt64[]>(variableName, typeInfo);
          break;
        case BuiltInType.Float:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<float>(variableName, typeInfo);
          else
            _return = AddBinding<float[]>(variableName, typeInfo);
          break;
        case BuiltInType.Double:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<Double>(variableName, typeInfo);
          else
            _return = AddBinding<Double[]>(variableName, typeInfo);
          break;
        case BuiltInType.String:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<String>(variableName, typeInfo);
          else
            _return = AddBinding<String[]>(variableName, typeInfo);
          break;
        case BuiltInType.DateTime:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<DateTime>(variableName, typeInfo);
          else
            _return = AddBinding<DateTime[]>(variableName, typeInfo);
          break;
        case BuiltInType.Guid:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<Guid>(variableName, typeInfo);
          else
            _return = AddBinding<Guid[]>(variableName, typeInfo);
          break;
        case BuiltInType.ByteString:
          if (typeInfo.ValueRank < 0)
            _return = AddBinding<byte[]>(variableName, typeInfo);
          else
            _return = AddBinding<byte[][]>(variableName, typeInfo);
          break;
        case BuiltInType.Null:
        case BuiltInType.XmlElement:
        case BuiltInType.NodeId:
        case BuiltInType.ExpandedNodeId:
        case BuiltInType.StatusCode:
        case BuiltInType.QualifiedName:
        case BuiltInType.LocalizedText:
        case BuiltInType.ExtensionObject:
        case BuiltInType.DataValue:
        case BuiltInType.Variant:
        case BuiltInType.DiagnosticInfo:
        case BuiltInType.Enumeration:
        default:
          throw new ArgumentOutOfRangeException("encoding");
      }
      return _return;
    }
    private IConsumerBinding AddBinding<type>(string variableName, UATypeInfo typeInfo)
    {
      ConsumerBindingMonitoredValue<type> _return = new ConsumerBindingMonitoredValue<type>(typeInfo);
      _return.PropertyChanged += (x, y) => ViewModel.Trace($"{DateTime.Now.ToLongTimeString()}:{DateTime.Now.Millisecond} {variableName} = {((ConsumerBindingMonitoredValue<type>)x).ToString()}");
      return _return;
    }
    #endregion

  }

}
