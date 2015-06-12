
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  /// <summary>
  /// Class MethodInstanceFactoryBase.
  /// </summary>
  internal class MethodInstanceFactoryBase: InstanceFactoryBase, IMethodInstanceFactory
  {

    /// <summary>
    /// Sets a value indicating whether the Method node is executable (“False” means not executable, “True” means executable), not taking user access rights into account.
    /// If the server cannot get the executable information from the underlying system, it should state that it is executable. If a Method is called, the server should transfer
    /// this request and return the corresponding StatusCode if such a request is rejected.
    /// </summary>
    /// <value><c>true</c> if executable; otherwise, <c>false</c>. Default value is <c>true</c></value>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool? Executable
    {
      set { throw new System.NotImplementedException(); }
    }

    /// <summary>
    /// Sets a value indicating whether the Method is currently executable taking user access rights into account (“False” means not executable, “True” means executable).
    /// </summary>
    /// <value><c>true</c> if executable by current user; otherwise, <c>false</c>. Default value is <c>true</c></value>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool? UserExecutable
    {
      set { throw new System.NotImplementedException(); }
    }
    /// <summary>
    /// Adds the input argument. The InputArgument specify the input argument of the Method. The Method contains an array of the Argument data type.
    /// An empty array indicates that there are no input arguments for the Method.
    /// </summary>
    /// <param name="argument">The input argument.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void AddInputArgument(IParameter argument)
    {
      throw new System.NotImplementedException();
    }
    /// <summary>
    /// Adds the output argument. The OutputArgument specifies the output argument of the Method. The Method contains an array of the Argument data type.
    /// An empty array indicates that there are no output arguments for the Method.
    /// </summary>
    /// <param name="argument">The output argument.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void AddOutputArguments(IParameter argument)
    {
      throw new System.NotImplementedException();
    }
  }
}
