using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.DotNet.ProjectModel.Workspaces;
using Xunit;

namespace CodeGround.Roslyn
{
   
   public class WorkspaceTests
    {

      
      [Fact(DisplayName = "dot net core projects does not work with MSBuildWorkspace at the moment. See https://github.com/dotnet/roslyn/issues/11342")]
       public void GetDocument()
       {
         string projectPath = @"F:\temp\WebAPIApp\WebAPIApp\WebAPIApp.csproj"; //TODO EB 20161019: Fix!
         //string projectPath = @"F:\Development\CodeGround\GodeGround\CodeGround.WebCore\CodeGround.WebCore.xproj"; //TODO EB 20161019: Fix!
         var msWorkspace = MSBuildWorkspace.Create();

          var project = msWorkspace.OpenProjectAsync(projectPath).Result;

          var doc = project.Documents.First(d => d.Name.Contains("Values")); //TODO EB 20161019: get document into document service
         
         Assert.True(doc.Name == "ValuesController.cs");
       }

      [Fact]
      public void GetDocumentProjectJson()
      {
         string basePath = @"F:\temp\WebCoreAPIApp\src\WebCoreAPIApp"; //TODO EB 20161019: Fix!
         string projectPath = @"F:\temp\WebCoreAPIApp\src\WebCoreAPIApp\project.json"; //TODO EB 20161019: Fix!
         //string projectPath = @"F:\Development\CodeGround\GodeGround\CodeGround.WebCore\CodeGround.WebCore.xproj"; //TODO EB 20161019: Fix!
         var workspace = new ProjectJsonWorkspace(projectPath);
         var thisDocument = workspace.CurrentSolution.GetDocumentIdsWithFilePath(Path.Combine(basePath, "project.json")).First();
         var project = workspace.CurrentSolution.GetProject(thisDocument.ProjectId);
         
         //var project = msWorkspace.OpenProjectAsync(projectPath).Result;


         var doc = project.Documents.First(d => d.Name.Contains("AS2")); //TODO EB 20161019: get document into document service

         Assert.True(doc.Name == "AS2Controller");
      }
   }
}
