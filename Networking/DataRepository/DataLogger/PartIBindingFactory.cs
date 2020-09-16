//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.DataRepository.DataLogger.Diagnostic;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;

namespace UAOOI.Networking.DataRepository.DataLogger
{
  /// <summary>
  /// Class <see cref="PartIBindingFactory"/> - it is a consumer of the data send over the wire using the UAOOI.Networking.SemanticData framework.
  /// It is expected that the data is generated according to the requirements defined by the OPCF to proceed interoperability testing.
  /// </summary>
  internal class PartIBindingFactory : IBindingFactory
  {
    #region composition

    /// <summary>
    /// Initializes a new instance of the <see cref="PartIBindingFactory"/> class.
    /// </summary>
    /// <param name="viewModel">The view model used to log data received over wire.</param>
    internal PartIBindingFactory(ConsumerViewModel viewModel)
    {
      _logger.EnteringMethodBinding();
      _ViewModel = viewModel;
    }

    #endregion composition

    #region IBindingFactory

    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="IConsumerBinding" /> type used by the consumer to save the data in the data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belonging to the same group are handled according to the same profile.</param>
    /// <param name="processValueName">The name of a variable that is the ultimate destination of the values recovered from messages.
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup" />.</param>
    /// <param name="fieldTypeInfo">The field metadata definition represented as an object of <see cref="UATypeInfo" />.</param>
    /// <returns>Returns an object implementing the <see cref="IConsumerBinding" /> interface that can be used to update selected variable on the factory side.</returns>
    IConsumerBinding IBindingFactory.GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      _logger.EnteringMethodBinding();
      return GetConsumerBinding(processValueName, fieldTypeInfo);
    }

    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="IProducerBinding" /> type used by the producer to read from the local data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belonging to the same group are handled according to the same profile.</param>
    /// <param name="processValueName">The name of a variable that is the source of the values forwarded by a message over the network.
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup" /></param>
    /// <param name="fieldTypeInfo">The <see cref="BuiltInType" />of the message field encoding.</param>
    /// <returns>Returns an object implementing the <see cref="IProducerBinding" /> interface that can be used to create message and populate it with the data.</returns>
    IProducerBinding IBindingFactory.GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      _logger.EnteringMethodBinding();
      NotImplementedException ex = new NotImplementedException($"intentionally the method {nameof(IBindingFactory.GetProducerBinding)} is not implemented.");
      _logger.LogException(nameof(PartIBindingFactory), ex);
      throw ex;
    }

    #endregion IBindingFactory

    #region private

    private ConsumerViewModel _ViewModel;
    private DataLoggerEventSource _logger = DataLoggerEventSource.Log();

    /// <summary>
    /// Helper method that creates the consumer binding.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="typeInfo">The encoding.</param>
    /// <returns>IConsumerBinding.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">variableName</exception>
    private IConsumerBinding GetConsumerBinding(string variableName, UATypeInfo typeInfo)
    {
      _logger.EnteringMethodBinding();
      IConsumerBinding returnValue = null;
      if (typeInfo.ValueRank == 0 || typeInfo.ValueRank > 1)
      {
        ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(nameof(typeInfo.ValueRank));
        _logger.LogException(nameof(PartIBindingFactory), ex);
        throw ex;
      }
      switch (typeInfo.BuiltInType)
      {
        case BuiltInType.Boolean:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<bool>(variableName, typeInfo);
          else
            returnValue = AddBinding<bool[]>(variableName, typeInfo);
          break;

        case BuiltInType.SByte:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<sbyte>(variableName, typeInfo);
          else
            returnValue = AddBinding<sbyte[]>(variableName, typeInfo);
          break;

        case BuiltInType.Byte:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<byte>(variableName, typeInfo);
          else
            returnValue = AddBinding<byte[]>(variableName, typeInfo);
          break;

        case BuiltInType.Int16:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<short>(variableName, typeInfo);
          else
            returnValue = AddBinding<short[]>(variableName, typeInfo);
          break;

        case BuiltInType.UInt16:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<ushort>(variableName, typeInfo);
          else
            returnValue = AddBinding<ushort[]>(variableName, typeInfo);
          break;

        case BuiltInType.Int32:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<int>(variableName, typeInfo);
          else
            returnValue = AddBinding<int[]>(variableName, typeInfo);
          break;

        case BuiltInType.UInt32:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<uint>(variableName, typeInfo);
          else
            returnValue = AddBinding<uint[]>(variableName, typeInfo);
          break;

        case BuiltInType.Int64:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<long>(variableName, typeInfo);
          else
            returnValue = AddBinding<long[]>(variableName, typeInfo);
          break;

        case BuiltInType.UInt64:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<ulong>(variableName, typeInfo);
          else
            returnValue = AddBinding<ulong[]>(variableName, typeInfo);
          break;

        case BuiltInType.Float:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<float>(variableName, typeInfo);
          else
            returnValue = AddBinding<float[]>(variableName, typeInfo);
          break;

        case BuiltInType.Double:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<double>(variableName, typeInfo);
          else
            returnValue = AddBinding<double[]>(variableName, typeInfo);
          break;

        case BuiltInType.String:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<string>(variableName, typeInfo);
          else
            returnValue = AddBinding<string[]>(variableName, typeInfo);
          break;

        case BuiltInType.DateTime:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<DateTime>(variableName, typeInfo);
          else
            returnValue = AddBinding<DateTime[]>(variableName, typeInfo);
          break;

        case BuiltInType.Guid:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<Guid>(variableName, typeInfo);
          else
            returnValue = AddBinding<Guid[]>(variableName, typeInfo);
          break;

        case BuiltInType.ByteString:
          if (typeInfo.ValueRank < 0)
            returnValue = AddBinding<byte[]>(variableName, typeInfo);
          else
            returnValue = AddBinding<byte[][]>(variableName, typeInfo);
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
      return returnValue;
    }

    private IConsumerBinding AddBinding<type>(string variableName, UATypeInfo typeInfo)
    {
      _logger.EnteringMethodBinding();
      ConsumerBindingMonitoredValue<type> returnValue = new ConsumerBindingMonitoredValue<type>(typeInfo);
      returnValue.PropertyChanged += (x, y) => _ViewModel.Trace($"{DateTime.Now.ToLongTimeString()}:{DateTime.Now.Millisecond} {variableName} = {((ConsumerBindingMonitoredValue<type>)x).ToString()}");
      return returnValue;
    }

    #endregion private
  }
}