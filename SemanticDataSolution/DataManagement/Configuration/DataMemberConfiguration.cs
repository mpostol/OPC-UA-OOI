
using System;

namespace UAOOI.SemanticData.DataManagement.Configuration
{

  /// <summary>
  /// Class DataMemberConfiguration - provides all necessary information to create binding between a local process value (PLC register, element on the screen, etc) with a Variable node exposed by an 
  /// OPC UA Server. Adding both to the configuration make the same configuration reusable by the OPC UA Server, OPC UA Client and UA application. 
  /// </summary>
  [Serializable]
  public partial class DataMemberConfiguration
  {

    /// <summary>
    /// Gets or sets the symbolic name representing the OPC UA Variable.
    /// </summary>
    /// <value>The symbolic name of the OPC UA Variable.</value>
    public string SymbolicName { get; set; }
    /// <summary>
    /// Gets or sets the name of the local process value. It must be unique name that can be used to find the Variable to be binded to the OPC UA node.
    /// </summary>
    /// <value>The name of the process value.</value>
    public string ProcessValueName { get; set; }
    /// <summary>
    /// Gets or sets the the Message encoding identifier used by the binding to convert value between message representation and local type.
    /// </summary>
    /// <value>The source encoding.</value>
    public string SourceEncoding { get; set; }

  }
}
