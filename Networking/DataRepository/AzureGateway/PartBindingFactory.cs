//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;

namespace UAOOI.Networking.DataRepository.AzureGateway
{
  internal class PartBindingFactory : IBindingFactory
  {
    #region IBindingFactory

    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="T:UAOOI.Networking.SemanticData.DataRepository.IConsumerBinding" /> type used by the consumer to save the data in the data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belonging to the same group are handled according to the same profile.</param>
    /// <param name="processValueName">The name of a variable that is the ultimate destination of the values recovered from messages.
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup" />.</param>
    /// <param name="fieldTypeInfo">The field metadata definition represented as an object of <see cref="T:UAOOI.Configuration.Networking.Serialization.UATypeInfo" />.</param>
    /// <returns>Returns an object implementing the <see cref="T:UAOOI.Networking.SemanticData.DataRepository.IConsumerBinding" /> interface that can be used to update selected variable on the factory side.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      IConsumerBinding _return = null;
      if (fieldTypeInfo.ValueRank == 0 || fieldTypeInfo.ValueRank > 1)
        throw new ArgumentOutOfRangeException(nameof(fieldTypeInfo.ValueRank));
      switch (fieldTypeInfo.BuiltInType)
      {
        case BuiltInType.Boolean:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<bool>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<bool[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.SByte:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<sbyte>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<sbyte[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.Byte:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<byte>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<byte[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.Int16:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<short>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<short[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.UInt16:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<ushort>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<ushort[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.Int32:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<int>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<int[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.UInt32:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<uint>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<uint[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.Int64:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<long>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<long[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.UInt64:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<ulong>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<ulong[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.Float:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<float>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<float[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.Double:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<double>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<double[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.String:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<string>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<string[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.DateTime:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<DateTime>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<DateTime[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.Guid:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<Guid>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<Guid[]>(repositoryGroup, processValueName, fieldTypeInfo);
          break;

        case BuiltInType.ByteString:
          if (fieldTypeInfo.ValueRank < 0)
            _return = AddBinding<byte[]>(repositoryGroup, processValueName, fieldTypeInfo);
          else
            _return = AddBinding<byte[][]>(repositoryGroup, processValueName, fieldTypeInfo);
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

    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="T:UAOOI.Networking.SemanticData.DataRepository.IProducerBinding" /> type used by the producer to read from the local data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belonging to the same group are handled according to the same profile.</param>
    /// <param name="processValueName">The name of a variable that is the source of the values forwarded by a message over the network.
    /// Must be unique in the context of the group named by <paramref name="repositoryGroup" /></param>
    /// <param name="fieldTypeInfo">The <see cref="T:UAOOI.Configuration.Networking.Serialization.BuiltInType" />of the message field encoding.</param>
    /// <returns>Returns an object implementing the <see cref="T:UAOOI.Networking.SemanticData.DataRepository.IProducerBinding" /> interface that can be used to create message and populate it with the data.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
    {
      throw new NotImplementedException();
    }

    #endregion IBindingFactory

    #region API

    internal dynamic GetDTO(string repositoryGroup)
    {
      return _processReplica[repositoryGroup];
    }

    #endregion API

    #region private

    private readonly Dictionary<string, RepositoryGroup> _processReplica = new Dictionary<string, RepositoryGroup>();

    private IConsumerBinding AddBinding<type>(string repositoryGroup, string variableName, UATypeInfo typeInfo)
    {
      ConsumerBindingMonitoredValue<type> _return = new ConsumerBindingMonitoredValue<type>(typeInfo);
      if (!_processReplica.ContainsKey(repositoryGroup))
        _processReplica.Add(repositoryGroup, new RepositoryGroup());
      Action<type> _updater = _processReplica[repositoryGroup].AddProperty<type>(variableName);
      _return.PropertyChanged += (x, y) => _updater(((ConsumerBindingMonitoredValue<type>)x).Value);
      return _return;
    }

    #endregion private
  }
}