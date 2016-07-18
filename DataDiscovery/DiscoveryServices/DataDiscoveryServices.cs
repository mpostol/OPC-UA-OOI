
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
  public class DataDiscoveryServices : IDisposable
  {
    #region public API
    /// <summary>
    /// Resolves address and reads the <see cref="DomainModel"/> record as an asynchronous operation.
    /// </summary>
    /// <param name="modelUri">The model URI.</param>
    /// <param name="rootZoneUrl">The root zone URL where the resolving process shall start.</param>
    /// <param name="log"><see cref="Action{T1, T2, T3}"/> encapsulating tracing functionality .</param>
    /// <returns>Task{DomainModel}.</returns>
    /// <exception cref="InvalidOperationException">Too many iteration in the resolving process.</exception>
    public async Task<DomainModel> ResolveDomainModelAsync(Uri modelUri, Uri rootZoneUrl, Action<string, TraceEventType, Priority> log)
    {
      log($"Starting resolving address of the domain model descriptor for the model Uri {modelUri}", TraceEventType.Verbose, Priority.Low);
      DomainDescriptor _lastDomainDescriptor = new DomainDescriptor() { NextStepRecordType = RecordType.DomainDescriptor };
      Uri _nextUri = rootZoneUrl;
      int _iteration = 0;
      do
      {
        _iteration++;
        log($"Resolving address iteration {_iteration} address: {_nextUri}", TraceEventType.Verbose, Priority.Low);
        if (_iteration > 16)
          throw new InvalidOperationException("Too many iteration in the resolving process.");
        _lastDomainDescriptor = await GetHTTPResponseAsync<DomainDescriptor>(_nextUri, log);
        _nextUri = _lastDomainDescriptor.ResolveUri(modelUri);
      } while (_lastDomainDescriptor.NextStepRecordType == RecordType.DomainDescriptor);
      log($"Reading DomainModel at: {_nextUri}", TraceEventType.Verbose, Priority.Low);
      Task<DomainModel> _DomainModelTask = GetHTTPResponseAsync<DomainModel>(_nextUri, log);
      DomainModel _model = await _DomainModelTask;
      _model.UniversalDiscoveryServiceLocator = _nextUri.ToString();
      log($"Successfuly received and decoded the requested DomainModel record: {_nextUri}", TraceEventType.Verbose, Priority.Low);
      return _model;
    }
    #endregion

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    private HttpClient m_Client = new HttpClient() { MaxResponseContentBufferSize = Int32.MaxValue };
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
          m_Client.Dispose();
        m_Client = null;
        disposedValue = true;
      }
    }
    // This code added to correctly implement the disposable pattern.
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    #region private
    /// <summary>
    /// Resolve domain description as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="address">The address of the discovery service.</param>
    /// <param name="log">Encapsulates the log operation.</param>
    /// <returns>Task{TResult}</returns>
    private async Task<TResult> GetHTTPResponseAsync<TResult>(Uri address, Action<string, TraceEventType, Priority> log)
      where TResult : class, new()
    {
      m_Client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
      //_client.DefaultRequestHeaders.Accept.Clear();
      //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("opc/application/json"));
      int _retryCount = 0;
      do
      {
        try
        {
          using (HttpResponseMessage _Message = await m_Client.GetAsync(address))
          {
            _Message.EnsureSuccessStatusCode();
            using (Task<Stream> _descriptionStream = _Message.Content.ReadAsStreamAsync())
            {
              XmlSerializer _serializer = new XmlSerializer(typeof(TResult));
              Stream _description = await _descriptionStream;
              TResult _newDescription = (TResult)_serializer.Deserialize(_description);
              return _newDescription;
            }
          };
        }
        catch (Exception _ex)
        {
          log($"Error for {address} in {nameof(GetHTTPResponseAsync)} retry ={_retryCount}: {_ex.Message} ", TraceEventType.Error, Priority.Medium);
          if (_retryCount < 3)
            _retryCount++;
          else
            throw;
        }
      } while (true);
    }
    //UnitTest instrumentation
    [System.Diagnostics.Conditional("DEBUG")]
    internal void GetHTTPResponse<T>(Uri address, Action<string, TraceEventType, Priority> debugLog, Action<T> getResult)
            where T : class, new()
    {
      Task<T> _task = GetHTTPResponseAsync<T>(address, debugLog);
      _task.Wait();
      getResult(_task.Result);
    }
    #endregion

  }
}
