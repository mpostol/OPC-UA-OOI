
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{
  internal class AssociationsCollection : Dictionary<string, Association>
  {
    internal static AssociationsCollection CreateAssociations(AssociationConfiguration[] configuration, IBindingFactory bindingFactory, IEncodingFactory encodingFactory)
    {
      AssociationsCollection _collection = new AssociationsCollection();
      Association _newAssociation = null;
      foreach (AssociationConfiguration _ax in configuration)
      {
        if (_collection.ContainsKey(_ax.Alias))
          throw new ArgumentOutOfRangeException("Alias", "Alias of any Association must be unique");
        SemanticData _newSemanticData = new SemanticData( new Uri(_ax.InformationModelURI), _ax.DataSymbolicName, null  );
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
    internal void AddMessageHandler(string alias, IMessageHandler messageHandler)
    {
      Association _ass = this[alias];
      _ass.AddMessageHandler(messageHandler);
    }
    /// <summary>
    /// Class SemanticData - private implementation of the <see cref="ISemanticData"/>
    /// </summary>
    //TODO consider intenal implementation
    private class SemanticData : ISemanticData
    {
      public SemanticData(Uri identifier, string symbolicName, IComparable nodeId)
      {
        Identifier = identifier;
        SymbolicName = symbolicName;
        NodeId = NodeId;
      }
      public Uri Identifier
      {
        get; private set;
      }
      public string SymbolicName
      {
        get; private set;
      }
      public IComparable NodeId
      {
        get; private set;
      }
    }
    private AssociationsCollection() { }

  }
}
