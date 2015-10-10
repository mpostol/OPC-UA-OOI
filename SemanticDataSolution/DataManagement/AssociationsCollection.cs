
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.Configuration;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.DataManagement.Encoding;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class AssociationsCollection - a dictionary containing all <see cref="Association"/>
  /// </summary>
  /// <Note>
  /// Current implementation does not support deletion of the <see cref="Association"/>. If the association is not longer needed call Disable on the <see cref="Association.State"/> object.
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
    internal static AssociationsCollection CreateAssociations(AssociationConfiguration[] configuration, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      AssociationsCollection _collection = new AssociationsCollection();
      Association _newAssociation = null;
      foreach (AssociationConfiguration _ax in configuration)
      {
        if (_collection.ContainsKey(_ax.Alias))
          throw new ArgumentOutOfRangeException("Alias", "Alias of any Association must be unique");
        SemanticData _newSemanticData = new SemanticData(new Uri(_ax.InformationModelURI), _ax.DataSymbolicName, null, _ax.Id);
        switch (_ax.AssociationRole)
        {
          case AssociationRole.Consumer:
            _newAssociation = new ConsumerAssociation(_newSemanticData, _ax.Alias, _ax.DataSet, bindingFactory, encodingFactory);
            break;
          case AssociationRole.Producer:
            _newAssociation = new ProducerAssociation(_newSemanticData, _ax.Alias, _ax.DataSet, bindingFactory, encodingFactory);
            break;
          default:
            break;
        }
        _collection.Add(_ax.Alias, _newAssociation);
      }
      return _collection;
    }
    /// <summary>
    /// Adds the message handler to the selected by the <paramref name="associationAlias" />.
    /// </summary>
    /// <remarks>
    /// If this dictionary does not contain the <paramref name="associationAlias"/> key the request is skipped - no action is undertaken.
    /// </remarks>
    /// <param name="associationAlias">The association alias.</param>
    /// <param name="messageHandler">The message handler to be associated.</param>
    internal void AddMessageHandler(string associationAlias, IMessageHandler messageHandler)
    {
      if (!this.ContainsKey(associationAlias))
        return;
      Association _ass = this[associationAlias];
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
          //TODO add error handler to provide feedback to the user, e.g. message in the log file - intensionally not implemented. 
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
      public SemanticData(Uri identifier, string symbolicName, IComparable nodeId, Guid Guid)
      {
        Identifier = identifier;
        SymbolicName = symbolicName;
        NodeId = NodeId;
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
