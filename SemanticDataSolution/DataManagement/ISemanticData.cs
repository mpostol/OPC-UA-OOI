
using System;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Interface ISemanticData - represents a data item conforming to the UA Semantic Data paradigm. 
  /// </summary>
  /// <remarks>
  /// <para>
  /// more about the UA Semantic Data concept cab be found in the:
  /// <a href="https://github.com/mpostol/OPC-UA-OOI/blob/master/SemanticDataSolution/README.MD">OPC UA Data Processing Outside the Server</a>
  /// </para>
  /// </remarks>
  public interface ISemanticData : IDataUniqueIdentifier
  {

    /// <summary>
    /// Gets the global unique identifier of the Information Model providing definition of the 
    /// Semantic Data as an object representation a uniform resource identifier <see cref="Uri"/>.
    /// </summary>
    ///<remarks>
    /// Using the URI as a standard for global identifiers allows for a worldwide reference for any data defined by the OPC UA Information Model. 
    /// This means that we can tell when any two applications anywhere in the world are referring to the same data.
    /// Using URI, therefore, we can introduce the notion of the global data identity. The data identity allows creation of variety of dictionaries collecting 
    /// supplementary information independently and outside of the server Address Space context.
    ///</remarks>
    /// <value>An object of <see cref="Uri"/> capturing the global unique identifier of this data instance.</value>
    Uri Identifier { get; }
    /// <summary>
    /// Gets the symbolic name of the instance node that is root of hierarchy of nodes creating the Semantic Data.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The symbolic name of each node is its <c>BrowseName</c>, or, when it is part of another node, the <c>BrowseName</c> of the other node, a "_", and the <c>BrowseName</c> of itself. 
    /// "Part of” means that the whole has a <c>HasProperty</c> or <c>HasComponent</c> reference to its part. Since all nodes not being part of another node have a unique name, the symbolic name is unique.
    /// </para>
    /// <note>
    /// Root element must not be an instance declaration.
    /// </note>
    /// </remarks>
    /// <value>The <see cref="string"/> representing the symbolic name of the root instance node.</value>
    string SymbolicName { get; }
    /// <summary>
    /// Stores an identifier for a node in a server's address space.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Please refer to OPC Specifications</b>:
    /// <list type="bullet">
    /// <item><b>Address Space Model</b> section <b>8.2</b></item>
    /// <item><b>Address Space Model</b> section <b>5.2.2</b></item>
    /// </list>
    /// </para>
    /// <para>
    /// Stores the id of a Node, which resides within the server's address space.
    /// <br/></para>
    /// <para>
    /// The NodeId can be either:
    /// <list type="bullet">
    /// <item><see cref="uint"/></item>
    /// <item><see cref="Guid"/></item>
    /// <item><see cref="string"/></item>
    /// <item><see cref="byte"/>[]</item>
    /// </list>
    /// <br/></para>
    /// <note>
    /// <b>Important:</b> Keep in mind that the NodeId should be unique such that no two nodes within an address-space share the same value.
    /// </note>
    /// <para>
    /// In the Address Space of the server the NodeId can be assigned to a particular namespace index. The assumption is
    /// that the host of this object will manage that directly using the <see cref="ISemanticData.Identifier"/>.
    /// <br/></para>
    /// </remarks>
    IComparable NodeId { get; }

  }
}
