
using UAOOI.Common.Infrastructure.Diagnostic;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{
  public class PrepareTests
  {
    /// <summary>
    /// This method that contains code to be used before all tests in the assembly have run and to allocate resources obtained by the assembly. 
    /// </summary>
    /// <remarks>
    /// It is used top make sure tha assemblies containing dependency injection exports are copied to the working space used by the testing infrastructure.
    /// </remarks>
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
    [Microsoft.VisualStudio.TestTools.UnitTesting.AssemblyInitialize]
    public void Initialize()
    {
      //Makes sure UAOOI.Common.Infrastructure is in the test target folder.
      TraceSourceBase _newOne = new TraceSourceBase();
    }
  }
}
