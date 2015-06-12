
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
  }
}
