//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.IO;
using System.Runtime.Serialization;

namespace UAOOI.SemanticData.SolutionConfiguration.Serialization
{
  /// <summary>
  /// Class UAModelDesignerSolution.
  /// </summary>
  public partial class UAModelDesignerSolution
  {
    /// <summary>
    /// Creates an empty solution model.
    /// </summary>
    /// <returns>UAModelDesignerSolution.</returns>
    internal static UAModelDesignerSolution CreateEmptyModel(string solutionName)
    {
      return new UAModelDesignerSolution()
      {
        Name = solutionName,
        Projects = new UAModelDesignerProject[] { },
        ServerDetails = UAModelDesignerSolutionServerDetails.CreateEmptyInstance()
      };
    }

    [OnDeserialized()]
    public void OnDeserialized(StreamingContext context)
    {
      ServerDetails = ServerDetails ?? UAModelDesignerSolutionServerDetails.CreateEmptyInstance();
    }
  }

  /// <summary>
  /// Class UAModelDesignerSolutionServerDetails - encapsulates details about the associated server configuration
  /// </summary>
  public partial class UAModelDesignerSolutionServerDetails
  {
    internal static UAModelDesignerSolutionServerDetails CreateEmptyInstance()
    {
      return new UAModelDesignerSolutionServerDetails() { codebase = string.Empty, configuration = string.Empty };
    }
  }

  internal enum ResourceEntries { Token_ProjectFileName, DefaultCSVFileName, Project_FileDialogDefaultExt };

  public partial class UAModelDesignerProject
  {
    internal static UAModelDesignerProject CreateEmpty(string name, Func<ResourceEntries, string> resource)
    {
      return new UAModelDesignerProject()
      {
        buildOutputDirectoryNameField = resource(ResourceEntries.Token_ProjectFileName),
        cSVFileNameField = resource(ResourceEntries.DefaultCSVFileName),
        fileNameField = Path.ChangeExtension(resource(ResourceEntries.Token_ProjectFileName), resource(ResourceEntries.Project_FileDialogDefaultExt)),
        nameField = name,
        ProjectIdentifier = Guid.NewGuid().ToString(),
      };
    }
  }
}