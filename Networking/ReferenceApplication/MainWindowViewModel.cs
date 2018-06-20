
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.ReferenceApplication.Consumer;
using UAOOI.Networking.ReferenceApplication.Controls;
using UAOOI.Networking.ReferenceApplication.Producer;
using UAOOI.Networking.SemanticData.DataRepository;

namespace UAOOI.Networking.ReferenceApplication
{

  /// <summary>
  /// Class MainWindowViewModel - this class demonstrates how to create bindings to the properties that are holders of OPC UA values in the 
  /// Model View ViewModel pattern.
  /// </summary>
  [Export()]
  [Export(typeof(IConsumerViewModel))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  internal class MainWindowViewModel : ViewModelBase, IConsumerViewModel
  {

    #region constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {

      b_ConsumerLog = new ObservableCollection<string>();
      //Menu Files
      b_ConfigurationFolder = new ConfigurationFolderCommand();
      b_HelpDocumentation = new WebDocumentationCommand(Properties.Resources.HelpDocumentationUrl);
      //Menu Actions
      b_OpenConsumerConfiguration = new ConfigurationEditorOpenCommand(Properties.Resources.ConfigurationDataConsumerFileName, SaveResponse);
      b_OpenProducerConfiguration = new ConfigurationEditorOpenCommand(Properties.Resources.ConfigurationDataProducerFileName, SaveResponse);
      //Menu Help
      b_ReadMe = new OpenFileCommand(Properties.Resources.ReadMeFileName);
      b_TermsOfService = new WebDocumentationCommand(Properties.Resources.TermsOfServiceUrl);
      b_ViewLicense = new WebDocumentationCommand(Properties.Resources.ViewLicenseUrl);
      String _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      b_WindowTitle = $"OPC UA Reactive Networking Example Application Rel. {_version} supporting PubSup protocol 1.10";

    }

    private FileInfo SaveResponse(FileInfo arg)
    {
      FileInfo _ret = null;
      SaveFileConfirmation _newFileInfo = new SaveFileConfirmation() { Title = "Save configuration file", FilePath = arg.FullName };
      SaveFileInteractionEvent?.Invoke(this, new InteractionRequestedEventArgs(_newFileInfo, () => _ret = String.IsNullOrEmpty(_newFileInfo.FilePath) ? null : new FileInfo(_newFileInfo.FilePath)));
      return _ret;
    }
    #endregion

    #region Window
    public string WindowTitle
    {
      get
      {
        return b_WindowTitle;
      }
      set
      {
        b_WindowTitle = value;
        this.RaisePropertyChanged<string>("WindowTitle", b_WindowTitle, value);
      }
    }
    internal event EventHandler<Controls.InteractionRequestedEventArgs> SaveFileInteractionEvent;
    private string b_WindowTitle;
    #endregion

    #region menu
    public ICommand OpenConsumerConfiguration
    {
      get
      {
        return b_OpenConsumerConfiguration;
      }
      set
      {
        b_OpenConsumerConfiguration = value;
        RaisePropertyChanged<ICommand>("OpenConsumerConfiguration", b_OpenConsumerConfiguration, value);
      }
    }
    public ICommand OpenProducerConfiguration
    {
      get
      {
        return b_OpenProducerConfiguration;
      }
      set
      {
        b_OpenProducerConfiguration = value;
        RaisePropertyChanged<ICommand>("OpenProducerConfiguration", b_OpenProducerConfiguration, value);
      }
    }
    public ICommand HelpDocumentation
    {
      get
      {
        return b_HelpDocumentation;
      }
      set
      {
        b_HelpDocumentation = value;
        RaisePropertyChanged<ICommand>("HelpDocumentation", b_HelpDocumentation, value);
      }
    }
    public ICommand ConfigurationFolder
    {
      get
      {
        return b_ConfigurationFolder;
      }
      set
      {
        b_ConfigurationFolder = value;
        RaisePropertyChanged<ICommand>("ConfigurationFolder", b_ConfigurationFolder, value);
      }
    }
    public ICommand ReadMe
    {
      get
      {
        return b_ReadMe;
      }
      set
      {
        b_ReadMe = value;
        RaisePropertyChanged<ICommand>("ReadMe", b_ReadMe, value);
      }
    }
    public ICommand ViewLicense
    {
      get
      {
        return b_ViewLicense;
      }
      set
      {
        b_ViewLicense = value;
        RaisePropertyChanged<ICommand>("ViewLicense", b_ViewLicense, value);
      }
    }
    public ICommand TermsOfService
    {
      get
      {
        return b_TermsOfService;
      }
      set
      {
        b_TermsOfService = value;
        RaisePropertyChanged<ICommand>("TermsOfService", b_TermsOfService, value);
      }
    }
    //private
    private ICommand b_TermsOfService;
    private ICommand b_ViewLicense;
    private ICommand b_ReadMe;
    private ICommand b_OpenProducerConfiguration;
    private ICommand b_OpenConsumerConfiguration;
    private ICommand b_ConfigurationFolder;
    private ICommand b_HelpDocumentation;
    #endregion

    #region IConsumerViewModel 
    /// <summary>
    /// Gets or sets the consumer received bytes.
    /// </summary>
    /// <value>The consumer received bytes.</value>
    public int ConsumerReceivedBytes
    {
      get
      {
        return b_ConsumerBytesReceived;
      }
      set
      {
        b_ConsumerBytesReceived = value;
        RaisePropertyChanged<int>("ConsumerReceivedBytes", b_ConsumerBytesReceived, value);
      }
    }
    /// <summary>
    /// Gets or sets the number of consumer received frames .
    /// </summary>
    /// <value>The consumer frames received.</value>
    public int ConsumerFramesReceived
    {
      get
      {
        return b_ConsumerFramesReceived;
      }
      set
      {
        b_ConsumerFramesReceived = value;
        RaisePropertyChanged<int>("ConsumerFramesReceived", b_ConsumerFramesReceived, value);
      }
    }
    /// <summary>
    /// Gets or sets the consumer update configuration command.
    /// </summary>
    /// <value>The consumer update configuration <see cref="ICommand" />.</value>
    public ICommand ConsumerUpdateConfiguration //TODO Remove reference of ConsumerDataManagementSetup System.Windows  #239
    {
      get
      {
        return b_ConsumerUpdateConfiguration;
      }
      set
      {
        b_ConsumerUpdateConfiguration = value;
        RaisePropertyChanged<ICommand>("ConsumerUpdateConfiguration", b_ConsumerUpdateConfiguration, value);
      }
    }
    /// <summary>
    /// Gets or sets the last consumer error message.
    /// </summary>
    /// <value>The consumer error message.</value>
    public string ConsumerErrorMessage
    {
      get
      {
        return b_ConsumerErrorMessage;
      }
      set
      {
        b_ConsumerErrorMessage = value;
        RaisePropertyChanged<string>("ConsumerErrorMessage", b_ConsumerErrorMessage, value);
      }
    }
    /// <summary>
    /// Add the message to the <see cref="MainWindowViewModel.ConsumerLog"/>.
    /// </summary>
    /// <param name="message">The message to be added to the log <see cref="MainWindowViewModel.ConsumerLog"/>.</param>
    public void Trace(string message)
    {
      GalaSoft.MvvmLight.Threading.DispatcherHelper.RunAsync((() => ConsumerLog.Insert(0, message)));
    }
    /// <summary>
    /// Helper method that creates the consumer binding.
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <param name="typeInfo">The encoding.</param>
    /// <returns>IConsumerBinding.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">variableName</exception>
    public IConsumerBinding GetConsumerBinding(string variableName, UATypeInfo typeInfo)
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
    #endregion

    #region Consumer ViewModel implementation
    public ObservableCollection<string> ConsumerLog
    {
      get
      {
        return b_ConsumerLog;
      }
      set
      {
        b_ConsumerLog = value;
        RaisePropertyChanged<ObservableCollection<string>>("ConsumerLog", b_ConsumerLog, value);
      }
    }
    #endregion

    #region ProducerViewModel
    /// <summary>
    /// Gets or sets the producer view model.
    /// </summary>
    /// <value>The producer view model.</value>
    [Import(ProducerCompositionSettings.ProducerViewModelContract)]
    public object ProducerViewModel { get; set; }
    public int BytesSent
    {
      get
      {
        return b_BytesSent;
      }
      set
      {
        b_BytesSent = value;
        RaisePropertyChanged<int>("BytesSent", b_BytesSent, value);
      }
    }
    public int PackagesSent
    {
      get
      {
        return b_PackagesSent;
      }
      set
      {
        b_PackagesSent = value;
        RaisePropertyChanged<int>("PackagesSent", b_PackagesSent, value);
      }
    }
    #endregion

    #region private
    //types
    private class WebDocumentationCommand : ICommand
    {

      public WebDocumentationCommand(string url)
      {
        m_URL = url;
      }
      public event EventHandler CanExecuteChanged;
      public bool CanExecute(object parameter)
      {
        return true;
      }
      public void Execute(object parameter)
      {
        try
        {
          using (Process process = Process.Start(m_URL)) { }
        }
        catch (Exception _ex)
        {
          MessageBox.Show($"An error occurs during opening the web page at: {_ex}", "Problem with the website!", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }
      }
      private readonly string m_URL;

    }
    private class ConfigurationFolderCommand : ICommand
    {
      public event EventHandler CanExecuteChanged;
      public bool CanExecute(object parameter)
      {
        return true;
      }
      public void Execute(object parameter)
      {
        string path = string.Empty;
        try
        {
          path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          using (Process process = Process.Start(@path)) { }
        }
        catch (Win32Exception)
        {
          MessageBox.Show($"No configuration folder exists at: {path}.", "No Log folder !", MessageBoxButton.OK, MessageBoxImage.Stop);
          return;
        }
        catch (Exception _ex)
        {
          MessageBox.Show($"An error occurs during opening the folder {_ex}", "Problem with log folder !", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }
      }
    }
    //vars
    //Consumer private part
    private ObservableCollection<string> b_ConsumerLog;
    private string b_ConsumerErrorMessage;
    private ICommand b_ConsumerUpdateConfiguration;
    private int b_ConsumerFramesReceived;
    private int b_ConsumerBytesReceived;
    //producer private part
    private int b_BytesSent;
    private int b_PackagesSent;
    //methods
    private IConsumerBinding AddBinding<type>(string variableName, UATypeInfo typeInfo)
    {
      ConsumerBindingMonitoredValue<type> _return = new ConsumerBindingMonitoredValue<type>(typeInfo);
      _return.PropertyChanged += (x, y) => Trace($"{DateTime.Now.ToLongTimeString()}:{DateTime.Now.Millisecond} {variableName} = {((ConsumerBindingMonitoredValue<type>)x).ToString()}");
      return _return;
    }
    #endregion

  }

}
