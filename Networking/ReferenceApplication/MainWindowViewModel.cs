
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
using UAOOI.Networking.ReferenceApplication.Properties;
using UAOOI.Networking.SemanticData.DataRepository;

namespace UAOOI.Networking.ReferenceApplication
{

  /// <summary>
  /// Class MainWindowViewModel - this class demonstrates how to create bindings to the properties that are holders of OPC UA values in the 
  /// Model View ViewModel pattern.
  /// </summary>
  [Export()]
  [PartCreationPolicy(CreationPolicy.Shared)]
  internal class MainWindowViewModel : ViewModelBase
  {

    #region constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {

      //Menu Files
      b_ConfigurationFolder = new RelayCommand(ProcessOpenFileInExecutingAssemblyLocation);
      b_HelpDocumentation = new RelayCommand(() => ProcessStart(Resources.HelpDocumentationUrl));
      //Menu Actions
      b_OpenConsumerConfiguration = new ConfigurationEditorOpenCommand(Properties.Resources.ConfigurationDataConsumerFileName, SaveResponse);
      b_OpenProducerConfiguration = new ConfigurationEditorOpenCommand(Properties.Resources.ConfigurationDataProducerFileName, SaveResponse);
      //Menu Help
      b_ReadMe = new RelayCommand(() => ProcessStart(Resources.ReadMeFileName));
      b_TermsOfService = new RelayCommand(() => ProcessStart(Resources.TermsOfServiceUrl));
      b_ViewLicense = new RelayCommand(() => ProcessStart(Resources.ViewLicenseUrl));
      String _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      b_WindowTitle = $"OPC UA Reactive Networking Example Application Rel. {_version} supporting PubSup protocol 1.10";

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

    #region Consumer ViewModel
    /// <summary>
    /// Gets or sets the producer view model.
    /// </summary>
    /// <value>The producer view model.</value>
    [Import(ConsumerCompositionSettings.ViewModelContract)]
    public object ConsumerViewModel { get; set; }
    #endregion

    #region Producer ViewModel
    /// <summary>
    /// Gets or sets the producer view model.
    /// </summary>
    /// <value>The producer view model.</value>
    [Import(typeof(SimulatorViewModel))]
    public SimulatorViewModel ProducerViewModel { get; set; }
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
    private int b_BytesSent;
    private int b_PackagesSent;
    private void ProcessStart(string parameter)
    {
      try
      {
        using (Process process = Process.Start(parameter)) { }
      }
      catch (Exception _ex)
      {
        MessageBox.Show($"An error occurs during opening the web page at: {_ex}", "Problem with the website!", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
    }
    private void ProcessOpenFileInExecutingAssemblyLocation()
    {
      string path = string.Empty;
      try
      {
        path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        using (Process process = Process.Start(@path)) { }
      }
      catch (Win32Exception)
      {
        MessageBox.Show($"No folder exists at: {path}.", "Folder error !", MessageBoxButton.OK, MessageBoxImage.Stop);
        return;
      }
      catch (Exception _ex)
      {
        MessageBox.Show($"An error occurs during opening the file {_ex}", "Problem with the file !", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
    }
    private FileInfo SaveResponse(FileInfo arg)
    {
      FileInfo _ret = null;
      SaveFileConfirmation _newFileInfo = new SaveFileConfirmation() { Title = "Save configuration file", FilePath = arg.FullName };
      SaveFileInteractionEvent?.Invoke(this, new InteractionRequestedEventArgs(_newFileInfo, () => _ret = String.IsNullOrEmpty(_newFileInfo.FilePath) ? null : new FileInfo(_newFileInfo.FilePath)));
      return _ret;
    }
    #endregion

  }

}
