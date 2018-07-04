
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using UAOOI.Configuration.Core;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.DataBindings
{

  /// <summary>
  /// Delegate TraceAction - encapsulates operation writing a trace event message to the trace using the specified event type, event identifier, and message.
  /// </summary>
  /// <param name="eventType">One of the enumeration values <see cref="TraceEventType"/> that specifies the event type of the trace data.</param>
  /// <param name="id">A numeric identifier for the event.</param>
  /// <param name="data">The trace message to write.</param>
  public delegate void TraceEvent(TraceEventType eventType, int id, string data);
  /// <summary>
  /// Interface IInstanceConfigurationFactory - object implementing this interface should provide the user interface 
  /// </summary>
  public interface IInstanceConfigurationFactory
  {

    /// <summary>
    /// Gets an object providing <see cref="IInstanceConfiguration" /> interface which is to be displayed in the main editor window.
    /// </summary>
    /// <param name="dataSet">The object <see cref="DataSetConfiguration"/> to be edited.</param>
    /// <param name="availableHandlers">The available handlers that can be associated with the <paramref name="dataSet"/>.</param>
    /// <param name="trace">The delegate encapsulating the trace operation.</param>
    /// <param name="onModification">The delegate encapsulating operation used to notify the caller about data modification.</param>
    /// <returns>IInstanceConfiguration.</returns>
    IInstanceConfiguration GetIInstanceConfiguration(DataSetConfiguration dataSet, ObservableCollection<MessageHandlerConfiguration> availableHandlers, TraceEvent trace, Action onModification );
    
  }

}
