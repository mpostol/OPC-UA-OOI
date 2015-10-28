
using System;
using System.Diagnostics;
using System.Threading;
using UAOOI.SemanticData.DataManagement;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Producer
{
  internal class CustomNodeManager : IBindingFactory, IEncodingFactory
  {
    public CustomNodeManager()
    {
      Value1 = new ProducerBindingMonitoredValue<DateTime>(m_Variable1Name);
      Value2 = new ProducerBindingMonitoredValue<double>(m_Variable2Name);
      m_Timer = new Timer(TimerCallback, null, 1000, 500);
    }

    #region IBindingFactory
    /// <summary>
    /// Gets the binding captured by an instance of the <see cref="IConsumerBinding" /> type used by the consumer to save the data in the data repository.
    /// </summary>
    /// <param name="repositoryGroup">It is the name of a repository group profiling the configuration behavior, e.g. encoders selection.
    /// The configuration of the repositories belong to the same group are handled according to the same profile.</param>
    /// <param name="variableName">The name of a variable that is the ultimate destination of the values recovered from messages. Must be unique in the context of the repositories group.
    /// is updated periodically by a data produced - user of the <see cref="IBinding" /> object.</param>
    /// <returns>Returns an object implementing the <see cref="IBinding" /> interface that can be used to update selected variable on the factory side.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Gets the producer binding.
    /// </summary>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="variableName">Name of the variable.</param>
    /// <returns>IProducerBinding.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
    {
      if (repositoryGroup != m_RepositoryGroup)
        throw new ArgumentNullException("repositoryGroup");
      if (variableName == m_Variable1Name)
      {
        return Value1;
      }
      else if (variableName == m_Variable2Name)
      {
        return Value2;
      }
      else
        throw new ArgumentOutOfRangeException("variableName");
    }
    #endregion

    #region IEncodingFactory
    /// <summary>
    /// Updates the value converter.
    /// </summary>
    /// <param name="converter">The converter.</param>
    /// <param name="repositoryGroup">The repository group.</param>
    /// <param name="sourceEncoding">The source encoding.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">repositoryGroup</exception>
    void IEncodingFactory.UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding)
    {
      if (repositoryGroup != m_RepositoryGroup)
        throw new ArgumentOutOfRangeException("repositoryGroup");
      Debug.Assert(sourceEncoding.CompareTo(converter.TargetType.ToString()) == 0);
    }
    #endregion

    private ProducerBindingMonitoredValue<DateTime> Value1 { get; set; }
    private ProducerBindingMonitoredValue<double> Value2 { get; set; }
    private const string m_Variable1Name = "Value1";
    private const string m_Variable2Name = "Value2";
    private const string m_RepositoryGroup = "repositoryGroup";
    private Timer m_Timer;
    private Random m_Random = new Random();
    private void TimerCallback(object state)
    {
      if (Value1.HandlerState == HandlerState.Operational)
        Value1.MonitoredValue = DateTime.Now;
      //if (Value2.HandlerState == HandlerState.Operational)
      //  Value2.MonitoredValue = m_Random.NextDouble();
    }
  }

}
