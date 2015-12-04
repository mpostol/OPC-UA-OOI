
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class AssociationsCollection - a dictionary containing all <see cref="Association"/> instances
  /// </summary>
  /// <Note>
  /// Current implementation does not support deletion of the <see cref="Association"/>. If the association is not longer needed call Disable on the <see cref="Association.State"/> object.
  /// </Note>
  /// <Note>
  /// Current implementation does not support handling of the configuration changes.
  /// </Note>
  internal class AssociationsCollection : Dictionary<string, Association>
  {

    #region interna API
    /// <summary>
    /// Creates the associations and populates the new dictionary with the associations instances created using the configuration <paramref name="configuration"/>.
    /// </summary>
    /// <param name="configuration">The configuration used to populate the collection.</param>
    /// <param name="bindingFactory">The binding factory responsible to create and return <see cref="IBinding"/> instance for each association.</param>
    /// <param name="encodingFactory">The encoding factory responsible to updated the created <see cref="IBinding"/> by provisioning all information necessary for encoding/decoding including <see cref="IValueConverter"/>.</param>
    /// <returns>New dictionary of type <see cref="AssociationsCollection"/>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Alias; Alias of any <see cref="Association"/> must be unique.</exception>
    internal static AssociationsCollection CreateAssociations(DataSetConfiguration[] configuration, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      AssociationsCollection _collection = new AssociationsCollection();
      Association _newAssociation = null;
      foreach (DataSetConfiguration _dataSet in configuration)
      {
        if (_collection.ContainsKey(_dataSet.AssociationName))
          throw new ArgumentOutOfRangeException("Alias", "Alias of any Association must be unique");
        SemanticData _newSemanticData = new SemanticData(new Uri(_dataSet.InformationModelURI), _dataSet.DataSymbolicName, null, _dataSet.Id);
        switch (_dataSet.AssociationRole)
        {
          case AssociationRole.Consumer:
            _newAssociation = new ConsumerAssociation(_newSemanticData, _dataSet, bindingFactory, encodingFactory);
            break;
          case AssociationRole.Producer:
            _newAssociation = new ProducerAssociation(_newSemanticData, _dataSet.AssociationName, _dataSet, bindingFactory, encodingFactory);
            break;
          default:
            break;
        }
        _collection.Add(_dataSet.AssociationName, _newAssociation);
      }
      return _collection;
    }
    /// <summary>
    /// Adds the message handler to the selected by the <paramref name="associationName" />.
    /// </summary>
    /// <remarks>
    /// If this dictionary does not contain the <paramref name="associationName"/> key the request is skipped - no action is undertaken.
    /// </remarks>
    /// <param name="associationName">The association alias.</param>
    /// <param name="messageHandler">The message handler to be associated.</param>
    internal void AddMessageHandler(string associationName, IMessageHandler messageHandler)
    {
      if (!this.ContainsKey(associationName))
        return;
      Association _ass = this[associationName];
      _ass.AddMessageHandler(messageHandler);
    }
    /// <summary>
    /// Handles the configuration modifications.
    /// </summary>
    /// <param name="sender">The sender of the modification.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    /// <remarks>
    /// Limitation: It is intentionally not implemented - it is placeholder for further development in case there need be.
    /// This handler could be called after recognition of of any modification over the out of bound communication, e.g. OPC UA Client/Server session or configuration file.
    /// </remarks>
    /// <exception cref="System.NotImplementedException">It is intentionally not implemented - it is placeholder for further development in case there need be.</exception>
    internal static void OnConfigurationChangeHandler(object sender, EventArgs e)
    {
      throw new NotImplementedException("It is intentionally not implemented");
    }
    /// <summary>
    /// Initializes this instance - initializes and enables all the object in this collection. 
    /// </summary>
    internal void Initialize()
    {
      foreach (Association _ax in this.Values)
      {
        try
        {
          _ax.Initialize();
          _ax.State.Enable();
        }
        catch (Exception)
        {
          //TODO tracking add exception handling to provide feedback to the user, e.g. message in the log file - intensionally not implemented. 
        }
      };
    }
    #endregion

    #region private
    /// <summary>
    /// Class SemanticData - private implementation of the <see cref="ISemanticData"/>
    /// </summary>
    //TODO consider internal implementation
    private class SemanticData : ISemanticData
    {
      public SemanticData(Uri identifier, string symbolicName, IComparable nodeId, Guid guid)
      {
        Identifier = identifier;
        SymbolicName = symbolicName;
        NodeId = NodeId;
        Guid = guid;
      }
      public Uri Identifier
      {
        get;
        private set;
      }
      public string SymbolicName
      {
        get;
        private set;
      }
      public IComparable NodeId
      {
        get;
        private set;
      }
      public Guid Guid
      {
        get;
        private set;
      }
    }
    private AssociationsCollection() { }
    #endregion

  }

}
