
using System;
using System.Xml;

namespace UAOOI.SemanticData.InformationModelFactory
{

  /// <summary>
  /// Interface IMethodInstanceFactory representing a Method in the Information Model. Methods are lightweight functions, whose scope is bounded by an owning object, 
  /// similar to the methods of a class in object-oriented programming or an owning object type, similar to static methods of a class.
  /// </summary>
  /// <remarks>
  /// This interface may be specified for a Method node that is a target of a HasComponent reference from a single TypeObject or Object node.
  /// </remarks>
  public interface IMethodInstanceFactory : IInstanceFactory
  {

    /// <summary>
    /// Sets a value indicating whether the Method node is executable (“False” means not executable, “True” means executable), not taking user access rights into account. 
    /// If the server cannot get the executable information from the underlying system, it should state that it is executable. If a Method is called, the server should transfer 
    /// this request and return the corresponding StatusCode if such a request is rejected.
    /// </summary>
    /// <value><c>true</c> if executable; otherwise, <c>false</c>. Default value is <c>true</c></value>
    bool? Executable
    {
      set;
    }
    /// <summary>
    /// Sets a value indicating whether the Method is currently executable taking user access rights into account (“False” means not executable, “True” means executable).
    /// </summary>
    /// <value><c>true</c> if executable by current user; otherwise, <c>false</c>. Default value is <c>true</c></value>
    bool? UserExecutable
    {
      set;
    }
    /// <summary>
    /// Adds the input arguments. The InputArgument specify the input argument of the Method. The Method contains an array of the Argument data type. 
    /// An empty array indicates that there are no input arguments for the Method.
    /// </summary>
    /// <param name="argument">Encapsulates a method used to convert Argument represented as <see cref="XMLElement"/>.</param>
    void AddInputArguments(Func<XmlElement, Parameter[]> argument);
    /// <summary>
    /// Adds the output argument. The OutputArgument specifies the output argument of the Method. The Method contains an array of the Argument data type. 
    /// An empty array indicates that there are no output arguments for the Method.
    /// </summary>
    /// <param name="argument">Encapsulates a method used to convert Argument represented as <see cref="XMLElement"/>.</param>
    void AddOutputArguments(Func<XmlElement, Parameter[]> argument);

  }
}
