
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UAOOI.DataDiscovery.DiscoveryServices.Models;

namespace UAOOI.DataDiscovery.DiscoveryServices
{
  /// <summary>
  /// Class DataDiscoveryServices - provides functionality supporting Global Data Discovery resolution.
  /// </summary>
  public class DataDiscoveryServices
  {
    /// <summary>
    /// Resolve domain description as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="address">The address of the discovery service.</param>
    /// <param name="log">Encapsulates the log operation.</param>
    /// <returns>Task{TResult}</returns>
    public static async Task<TResult> GetHTTPResponseAsync<TResult>(Uri address, Action<string, TraceEventType, Priority> log)
      where TResult : class, new()
    {
      try
      {
        using (HttpClient _client = new HttpClient())
        {
          //_client.BaseAddress = address;
          _client.MaxResponseContentBufferSize = Int32.MaxValue;
          _client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
          //_client.DefaultRequestHeaders.Accept.Clear();
          //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("opc/application/json"));
          using (HttpResponseMessage _Message = await _client.GetAsync(address))
          {
            _Message.EnsureSuccessStatusCode();
            using (Task<Stream> _descriptionStream = _Message.Content.ReadAsStreamAsync())
            {
              XmlSerializer _serializer = new XmlSerializer(typeof(TResult));
              Stream _description = await _descriptionStream;
              TResult _newDescription = (TResult)_serializer.Deserialize(_description);
              return _newDescription;
            }
          }
        }
      }
      catch (Exception _ex)
      {
        log($"Error for {nameof(address)} in {nameof(GetHTTPResponseAsync)}: {_ex.Message} ", TraceEventType.Error, Priority.Medium);
        throw;
      }
    }
    /// <summary>
    /// Resolves domain model as an asynchronous operation.
    /// </summary>
    /// <param name="modelUri">The model URI.</param>
    /// <param name="log">The log.</param>
    /// <returns>Task&lt;DomainModel&gt;.</returns>
    /// <exception cref="InvalidOperationException">Too many iteration in the resolve process.</exception>
    public static async Task<DomainModel> ResolveDomainModelAsync(Uri modelUri, Action<string, TraceEventType, Priority> log)
    {
      log($"Starting resolving address of the domain model descriptor for the model Uri {modelUri}", TraceEventType.Verbose, Priority.Low);
      DomainDescriptor _lastDomainDescriptor = new DomainDescriptor() { NextStepRecordType = RecordType.DomainDescriptor };
      Uri _nextUri = new Uri(Properties.Settings.Default.DataDiscoveryRootServiceUrl);
      int _iteration = 0;
      do
      {
        _iteration++;
        log($"Resolving address iteration {_iteration} address: {_nextUri}", TraceEventType.Verbose, Priority.Low);
        if (_iteration > 16)
          throw new InvalidOperationException("Too many iteration in the resolve process.");
        Task<DomainDescriptor> _DomainDescriptorTask = GetHTTPResponseAsync<DomainDescriptor>(_nextUri, log);
        _lastDomainDescriptor = await _DomainDescriptorTask;
        _nextUri = _lastDomainDescriptor.ResolveUri(modelUri);
      } while (_lastDomainDescriptor.NextStepRecordType == RecordType.DomainDescriptor);
      log($"Reading DomainModel at: {_nextUri}", TraceEventType.Verbose, Priority.Low);
      Task<DomainModel> _DomainModelTask = GetHTTPResponseAsync<DomainModel>(_nextUri, log);
      DomainModel _model = await _DomainModelTask;
      _model.UniversalDiscoveryServiceLocator = _nextUri.ToString();
      log($"Successfuly received and decoded the requested DomainModel record: {_nextUri}", TraceEventType.Verbose, Priority.Low);
      return _model;
    }

  }
}
